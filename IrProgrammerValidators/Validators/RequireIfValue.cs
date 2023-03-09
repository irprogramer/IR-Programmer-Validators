using System.ComponentModel.DataAnnotations;

namespace BetterHomeApi.Domains.Validators;

public class RequireIfValue : ValidationAttribute
{
    private readonly string _sourceParameterName;
    private readonly object _targetValue;

    public RequireIfValue(string sourceParameterName, object targetValue)
    {
        _sourceParameterName = sourceParameterName;
        _targetValue = targetValue;

        if (string.IsNullOrEmpty(ErrorMessage))
        {
            ErrorMessage = $"وارد کردن این بخش در صورت True بودن {sourceParameterName} اجباری است";
        }
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {

        var instance = validationContext.ObjectInstance;
        var sourceProperty = instance.GetType().GetProperty(_sourceParameterName);
        var rawValue = sourceProperty?.GetValue(instance);

        if (sourceProperty == null)
        {
            throw new ArgumentException($"Invalid Source Property, Please Enter Valid Source Property Name",
                "sourceParameterName");
        }

        if (!Equals(rawValue, _targetValue) || value != null)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(ErrorMessage);
    }
}