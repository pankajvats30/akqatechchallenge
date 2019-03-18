using System;
using AKQA.Web.Services;
using AKQA.Web.Services.Controllers;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AKQA.Web.Services.Tests.Controllers
{
    [TestClass]
    public class NumberConverterControllerTest
    {
        [TestMethod]
        public void Convert()
        {
            string input = "123.45";
          
            string output = "One hundred twenty-three  and Forty-five  cents";
            // Arrange
            NumberConverterController controller = new NumberConverterController();

            // Act
            string result = controller.Convert(input);

            // Assert
            Assert.AreEqual(output, result);
        }
    }
}
