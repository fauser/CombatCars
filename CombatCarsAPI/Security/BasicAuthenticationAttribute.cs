using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using CombatCarsAPI.Models;

namespace CombatCarsAPI.Security
{
    public class BasicAuthenticationAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
            else
            {
                string authToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(authToken));

                string username = decodedToken.Substring(0, decodedToken.IndexOf(":"));
                string password = decodedToken.Substring(decodedToken.IndexOf(":") + 1);

                using (CombatCarsAPIModelDataContext repository = new CombatCarsAPIModelDataContext(ConnectionStringHandler.ConnectionString()))
                {
                    User user = (from u in repository.Users
                                 where u.Username == username && u.Password == password
                                 select u).FirstOrDefault();

                    Token token = (from t in repository.Tokens
                                   where t.UserId == user.UserId
                                   select t).FirstOrDefault();

                    if (user != null)
                    {
                        if (token != null)
                        {
                            TokenHandler.SetNewLeaseTime(repository, token);
                        }
                        else
                        {
                            token = TokenHandler.CreateNewToken(repository, user);
                        }

                        HttpContext.Current.User = new GenericPrincipal(new ApiIdentity(user, token), new string[] { });
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