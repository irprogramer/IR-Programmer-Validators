using System.ComponentModel.DataAnnotations;

namespace IrProgrammerValidators.Validators;

public class RequireIfTrueAttribute : ValidationAttribute
{

    private readonly string _sourceParameterName;

    public RequireIfTrueAttribute(string sourceParameterName)
    {

        _sourceParameterName = sourceParameterName;

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
                _sourceParameterName);
        }

        if (rawValue is bool == false)
        {
            throw new ArgumentException("Source Property Type Must Be Boolean Or bool");
        }

        var sourcePropertyValue = (bool?) rawValue;

        if (sourcePropertyValue != true || value != null)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(ErrorMessage);
    }
}