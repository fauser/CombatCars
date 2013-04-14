using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using CombatCarsAPI.Models;
using CombatCarsAPI.Security;

namespace CombatCarsAPI.Controllers
{
    [BasicAuthentication]
    public class AuthenticationController : ApiController
    {
        // GET api/authentication
        public Token Get()
        {
            return ((ApiIdentity)HttpContext.Current.User.Identity).Token;
        }
    }
}
