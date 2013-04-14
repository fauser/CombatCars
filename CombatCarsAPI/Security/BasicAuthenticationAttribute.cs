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
            CombatCarsAPIModelDataContext repository = new CombatCarsAPIModelDataContext(@"Data Source=msdb6.surftown.se;Initial Catalog=fauser7_combatcars;Persist Security Info=True;User ID=fauser7_combatcars;Password=combat1234");

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

                //IPasswordTransform transform = DependencyResolver.Current.GetService<IPasswordTransform>();
                //IRepository<User> userRepository = DependencyResolver.Current.GetService<IRepository<User>>();

                User user = (from u in repository.Users
                             where u.Username == username && u.Password == password
                             select u).FirstOrDefault();

                Token token = (from t in repository.Tokens
                               where t.UserId == user.UserId
                               select t).FirstOrDefault();

                //70,6,141,10,127,131,125,236,203,83,197,168,119,175,109,214,179,139,212,237,201,117,106,114,218,26,122,7,37,166,121,110,109,109,156,22,37,67,125,2,176,170,51,4,14,118,210,150,54,187,159,157,83,186,110,150,145,56,24,215,71,174,175,230,202,207,15,124,145,235,251,103,126,237,111,216,132,42,0,107,14,245,90,249,71,3,3,105,168,58,106,184,51,215,210,30,130,34,3,110,29,43,108,120,86,48,130,123,54,34,59,122,253,198,163,140,208,116,201,240,244,196,195,213,10,0,216,244



                if (user != null)
                {
                    if (token != null)
                    {
                        token.Issued = DateTime.Now;
                        token.ValidTo = DateTime.Now.AddMinutes(5);
                        repository.SubmitChanges();
                    }
                    else
                    {
                        token = new Token() { UserId = 1, TokenString = "dsdsdsds", Issued = DateTime.Now, ValidTo = DateTime.Now.AddMinutes(5) };
                        repository.Tokens.InsertOnSubmit(token);
                        repository.SubmitChanges();
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