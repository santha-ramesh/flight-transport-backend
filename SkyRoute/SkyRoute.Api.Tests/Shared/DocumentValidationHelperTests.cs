using SkyRoute.Api.Shared.Helpers;

namespace SkyRoute.Api.Tests.Shared
{
    [TestClass]
    public class DocumentValidationHelperTests
    {
        [TestMethod]
        public void InternationalPassport_Should_BeValid()
        {
            var result =
                DocumentValidationHelper.IsValidDocument("NY", "CHI", "P1234567");

            Assert.IsTrue(result);
        }
    }
}
