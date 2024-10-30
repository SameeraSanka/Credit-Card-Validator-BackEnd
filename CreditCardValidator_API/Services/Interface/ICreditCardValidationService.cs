using CreditCardValidator_API.Common;
using CreditCardValidator_API.Model;

namespace CreditCardValidator_API.Services.Interface
{
    public interface ICreditCardValidationService
    {
        Response ValidateCard(CreditCard card);
    }
}
