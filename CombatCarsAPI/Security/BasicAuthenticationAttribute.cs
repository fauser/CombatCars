using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using CombatCarsAPI.Models;

namespace CombatCarsAPI.Security
{
    public class BasicAuthenticationAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            IEnumerable<string> ds;
            if (actionContext.Request.Headers.TryGetValues(EnumHeader.Authentication.ToString(), out ds) && ds.Count() != 1)
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
            else
            {
                string authToken = ds.First();
                string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));

                string username = decodedToken.Substring(0, decodedToken.IndexOf(":"));
                string password = decodedToken.Substring(decodedToken.IndexOf(":") + 1);

                using (CombatCarsAPIModelDataContext repository = new CombatCarsAPIModelDataContext(ConnectionStringHandler.ConnectionString()))
                {
                    User user = (from u in repository.Users
                                 where u.Username == username && u.Password == password
                                 select u).FirstOrDefault();

                    if (user != null)
                    {
                        Token token = (from t in repository.Tokens
                                       where t.UserId == user.UserId
                                       select t).FirstOrDefault();

                        if (token != null)
                        {
                            TokenHandler.SetNewLeaseTime(repository, token);
                        }
                        else
                        {
                            token = TokenHandler.CreateNewToken(repository, user);
                        }

                        GenericPrincipal gp = new GenericPrincipal(new ApiIdentity(user, token), new string[] { });
                        Thread.CurrentPrincipal = gp;
                        HttpContext.Current.User = gp;
                        base.OnActionExecuting(actionContext);
                    }
                    else
                    {
                        actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                    }
                }
            }
        }

    }
}