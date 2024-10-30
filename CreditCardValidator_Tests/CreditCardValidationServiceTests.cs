using Microsoft.VisualStudio.TestTools.UnitTesting;
using CreditCardValidator_API.Services;
using CreditCardValidator_API.Model;
using CreditCardValidator_API.Common;

namespace CreditCardValidator_API.Tests
{
    [TestClass]
    public class CreditCardValidationServiceTests
    {
        private CreditCardValidationService _service;

        [TestInitialize]
        public void Setup()
        {
            _service = new CreditCardValidationService();
        }

        [TestMethod]
        public void ValidateCard_EmptyCardNumber_ReturnsError()
        {
            // Arrange
            var card = new CreditCard { CardNumber = "" };

            // Act
            var result = _service.ValidateCard(card);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Credit card number is required", result.Message);
            Assert.AreEqual(400, result.Code);
        }

        [TestMethod]
        public void ValidateCard_InvalidProvider_ReturnsError()
        {
            // Arrange
            var card = new CreditCard { CardNumber = "9999999999999999" };

            // Act
            var result = _service.ValidateCard(card);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Credit card number is Invalid", result.Message);
            Assert.AreEqual(400, result.Code);
        }

        [TestMethod]
        public void ValidateCard_ValidVisaCard_ReturnsSuccess()
        {
            // Arrange
            var card = new CreditCard { CardNumber = "4012888888881881" };

            // Act
            var result = _service.ValidateCard(card);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Card is Valid", result.Message);
            Assert.AreEqual(200, result.Code);
            Assert.AreEqual("Visa Card", result.Data);
        }

        [TestMethod]
        public void ValidateCard_ValidMasterCard_ReturnsSuccess()
        {
            // Arrange
            var card = new CreditCard { CardNumber = "5105105105105100" };

            // Act
            var result = _service.ValidateCard(card);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Card is Valid", result.Message);
            Assert.AreEqual(200, result.Code);
            Assert.AreEqual("Master Card", result.Data);
        }

        [TestMethod]
        public void ValidateCard_ValidAmexCard_ReturnsSuccess()
        {
            // Arrange
            var card = new CreditCard { CardNumber = "371449635398431" };

            // Act
            var result = _service.ValidateCard(card);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Card is Valid", result.Message);
            Assert.AreEqual(200, result.Code);
            Assert.AreEqual("AmEx", result.Data);
        }

        [TestMethod]
        public void ValidateCard_ValidDiscoverCard_ReturnsSuccess()
        {
            // Arrange
            var card = new CreditCard { CardNumber = "6011111111111117" };

            // Act
            var result = _service.ValidateCard(card);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual("Card is Valid", result.Message);
            Assert.AreEqual(200, result.Code);
            Assert.AreEqual("Discover", result.Data);
        }

        [TestMethod]
        public void ValidateCard_FailsLuhnCheck_ReturnsError()
        {
            // Arrange
            var card = new CreditCard { CardNumber = "4012888888881882" }; // Invalid Luhn checksum

            // Act
            var result = _service.ValidateCard(card);

            // Assert
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Credit card number is Invalid", result.Message);
            Assert.AreEqual(400, result.Code);
        }
    }
}
