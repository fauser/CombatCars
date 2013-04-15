using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using CombatCarsAPI.Models;

namespace CombatCarsAPI.Security
{
    internal class TokenHandler
    {
        internal static string ReadTokenStringFromRequest(HttpRequestMessage request)
        {
            return request.Headers.GetValues("Authorization-Token").First();
        }

        internal static Token GetTokenSpecifiedInRequest(CombatCarsAPIModelDataContext repository, HttpRequestMessage request)
        {
            string tokenFromHeader = request.Headers.GetValues("Authorization-Token").First();

            Token token = (from t in repository.Tokens
                           where t.TokenString == tokenFromHeader
                           select t).FirstOrDefault();
            return token;
        }

        internal static void SetNewLeaseTime(CombatCarsAPIModelDataContext repository, Token token)
        {
            token.Issued = DateTime.Now;
            token.ValidTo = token.Issued.AddMinutes(5);
            repository.SubmitChanges();
        }


        internal static Token CreateNewToken(CombatCarsAPIModelDataContext repository, User user)
        {
            Token token = new Token() { UserId = user.UserId, TokenString = Guid.NewGuid().ToString(), Issued = DateTime.Now, ValidTo = DateTime.Now.AddMinutes(5) };
            repository.Tokens.InsertOnSubmit(token);
            repository.SubmitChanges();

            return token;
        }
    }
}