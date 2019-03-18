using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AKQATechChallenge;
using AKQATechChallenge.Controllers;
namespace WebService.Tests
{
    [TestClass]
    public class NumberConverterController
    {
        [TestMethod]
        public void Convert()
        {
            string input = "123.45";
            // Arrange
            NumberConverterController controller = new NumberConverterController();

            // Act
            string result = controller.Convert(input);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("One hundred twenty-three and Forty-five cents", result.ElementAt(0));

        }
    }
}
