using CreditCardValidator_API.Common;
using CreditCardValidator_API.Model;
using CreditCardValidator_API.Services.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CreditCardValidator_API.Services;

public class CreditCardValidationService : ICreditCardValidationService
{
    public Response ValidateCard(CreditCard card)
    {
        try
        {
            if (string.IsNullOrEmpty(card.CardNumber))
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Credit card number is required",
                    Code = 400,
                    Data = null

                };
            }
            string cardNumber = Regex.Replace(card.CardNumber, @"[\s-]", "");

            string cardName;
            if (Regex.IsMatch(cardNumber, @"^4\d{12}(\d{3})?$"))
                cardName = "Visa Card";
            else if (Regex.IsMatch(cardNumber, @"^5[1-5]\d{14}$"))
                cardName = "Master Card";
            else if (Regex.IsMatch(cardNumber, @"^3[47]\d{13}$"))
                cardName = "AmEx";
            else if (Regex.IsMatch(cardNumber, @"^6(?:011|5\d{2})\d{12}$"))
                cardName = "Discover";
            else
                return new Response
                {
                    IsSuccess = false,
                    Message = "Credit card number is Invalid",
                    Code = 400,
                    Data = null

                };

            bool isLuhnAlgorithmValid = ValidateLuhnAlgorithm(cardNumber);
            if (isLuhnAlgorithmValid)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = "Card is Valid",
                    Code = 200,
                    Data = cardName
                };
            }
            return new Response
            {
                IsSuccess = false,
                Message = "Credit card number is Invalid",
                Code = 400,
                Data = null
            };
        }
        catch 
        {
            return new Response
            {
                IsSuccess = false,
                Message = "Credit card number is Invalid",
                Code = 400,
                Data = null
            };
        }
    }

    private bool ValidateLuhnAlgorithm(string cardNumber)
    {
        int sum = 0;
        bool isSecondDigit = false;

        for (int i = cardNumber.Length - 1; i >= 0; i--)
        {
            int digit = cardNumber[i] - '0';

            if (isSecondDigit)
            {
                digit *= 2;
                if (digit > 9)
                    digit -= 9;
            }

            sum += digit;
            isSecondDigit = !isSecondDigit;
        }

        return sum % 10 == 0;
    }


}
