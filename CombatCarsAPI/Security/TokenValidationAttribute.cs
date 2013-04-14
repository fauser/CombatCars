using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using CombatCarsAPI.Models;

namespace CombatCarsAPI.Security
{
    public class TokenValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            CombatCarsAPIModelDataContext repository = new CombatCarsAPIModelDataContext(@"Data Source=msdb6.surftown.se;Initial Catalog=fauser7_combatcars;Persist Security Info=True;User ID=fauser7_combatcars;Password=combat1234");

            string tokenFromHeader;

            try
            {
                tokenFromHeader = actionContext.Request.Headers.GetValues("Authorization-Token").First();
            }
            catch (Exception)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Missing Authorization-Token")
                };
                return;
            }

            try
            {
                Token token = repository.Tokens.First(t => t.TokenString == tokenFromHeader);

                if (token.ValidTo < DateTime.Now)
                    actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized)
                    {
                        Content = new StringContent("Unauthorized User")
                    };


                base.OnActionExecuting(actionContext);
            }
            catch (Exception)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
                {
                    Content = new StringContent("Unauthorized User")
                };
                return;
            }
        }
    }
}