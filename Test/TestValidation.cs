using NUnit.Framework;

namespace VentSystems.Test
{
    [TestFixture]
    public class TestValidation
    {

        [Test]
        public void Test1()
        {
            var login = "asdfsd";
            var password = "123456";
            
            Assert.Equals(login.Length < 7 || password.Length < 7, true);
        }
        
    }
}