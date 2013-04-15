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
            string tokenFromHeader;
            CombatCarsAPIModelDataContext repository = new CombatCarsAPIModelDataContext(ConnectionStringHandler.ConnectionString());
            try
            {
                tokenFromHeader = TokenHandler.GetTokenSpecifiedInRequest(repository, actionContext.Request).TokenString;
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