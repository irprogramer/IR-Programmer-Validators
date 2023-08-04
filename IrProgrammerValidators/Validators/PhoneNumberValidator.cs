using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace IrProgrammerValidators.Validators;

public class PhoneNumberValidator : ValidationAttribute
{
    #region Ctor

    private readonly Country _country;

    public PhoneNumberValidator(Country country = Country.Iran)
    {
        _country = country;

        if (string.IsNullOrEmpty(ErrorMessage))
        {
            ErrorMessage = "لطفا یک شماره تلفن معتبر وارد کنید";
        }
    }

    #endregion

    public override bool IsValid(object? value)
    {
        if (value.GetType() != typeof(string))
        {
            throw new ArgumentException("Value Is Not String, Value: " + value);
        }

        var correctValue = (string) value;
        var validatorRegex = new Regex(@"09[0-9]{9}");
        var pnSize = 11;

        return correctValue.Length == pnSize && validatorRegex.IsMatch(correctValue);
    }
}

public enum Country
{
    Iran
}