using System;
using System.Security.Principal;
using CombatCarsAPI.Models;

namespace CombatCarsAPI.Security
{

    public class ApiIdentity : IIdentity
    {
        public User User { get; private set; }
        public Token Token { get; private set; }

        public ApiIdentity(User user, Token token)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            this.User = user;
            this.Token = token;
        }

        public string Name
        {
            get { return this.User.Username; }
        }

        public string AuthenticationType
        {
            get { return "Basic"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }
    }
}