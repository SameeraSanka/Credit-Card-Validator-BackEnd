using Microsoft.VisualStudio.TestTools.UnitTesting;
using CreditCardValidator_API.Services;
using CreditCardValidator_API.Model;
using CreditCardValidator_API.Common;

namespace CreditCardValidator_API.Tests;

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
        var card = new CreditCard { CardNumber = "" };

        var result = _service.ValidateCard(card);

        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual("Credit card number is required", result.Message);
        Assert.AreEqual(400, result.Code);
    }

    [TestMethod]
    public void ValidateCard_InvalidProvider_ReturnsError()
    {
        var card = new CreditCard { CardNumber = "9999999999999999" };

        var result = _service.ValidateCard(card);

        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual("Credit card number is Invalid", result.Message);
        Assert.AreEqual(400, result.Code);
    }

    [TestMethod]
    public void ValidateCard_ValidVisaCard_ReturnsSuccess()
    {
        var card = new CreditCard { CardNumber = "4012888888881881" };

        var result = _service.ValidateCard(card);

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual("Card is Valid", result.Message);
        Assert.AreEqual(200, result.Code);
        Assert.AreEqual("Visa Card", result.Data);
    }

    [TestMethod]
    public void ValidateCard_ValidMasterCard_ReturnsSuccess()
    {
        var card = new CreditCard { CardNumber = "5105105105105100" };

        var result = _service.ValidateCard(card);

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual("Card is Valid", result.Message);
        Assert.AreEqual(200, result.Code);
        Assert.AreEqual("Master Card", result.Data);
    }

    [TestMethod]
    public void ValidateCard_ValidAmexCard_ReturnsSuccess()
    {
        var card = new CreditCard { CardNumber = "371449635398431" };

        var result = _service.ValidateCard(card);

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual("Card is Valid", result.Message);
        Assert.AreEqual(200, result.Code);
        Assert.AreEqual("AmEx", result.Data);
    }

    [TestMethod]
    public void ValidateCard_ValidDiscoverCard_ReturnsSuccess()
    {
        var card = new CreditCard { CardNumber = "6011111111111117" };

        var result = _service.ValidateCard(card);

        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual("Card is Valid", result.Message);
        Assert.AreEqual(200, result.Code);
        Assert.AreEqual("Discover", result.Data);
    }

    [TestMethod]
    public void ValidateCard_FailsLuhnCheck_ReturnsError()
    {
        var card = new CreditCard { CardNumber = "4012888888881882" }; 

        var result = _service.ValidateCard(card);

        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual("Credit card number is Invalid", result.Message);
        Assert.AreEqual(400, result.Code);
    }
}
