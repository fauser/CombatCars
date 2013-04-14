using System;
using CombatCarsAPI.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CombatCarsAPITest
{
    [TestClass]
    public class RSATest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var token = "Daniel";
            var encryptedToken = RSAClass.Encrypt(token);
            var decryptedToken = RSAClass.Decrypt(encryptedToken);

            Assert.AreEqual(token, decryptedToken);
        }
    }
}
