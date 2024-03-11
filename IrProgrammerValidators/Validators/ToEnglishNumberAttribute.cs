using System.ComponentModel.DataAnnotations;

namespace IrProgrammerValidators.Validators;

public class ToEnglishNumberAttribute : ValidationAttribute
{
    private readonly string _sourceParameterName;
    private readonly object _targetValue;

    public ToEnglishNumberAttribute(string sourceParameterName, object targetValue)
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
        var rawValue = sourceProperty?.GetValue(instance)?.ToString();

        if (rawValue != null)
        {
            sourceProperty?.SetValue(instance, ToEnglishNumber(rawValue!));
        }

        return ValidationResult.Success;
    }

    public string ToEnglishNumber(string value)
    {
        Dictionary<string, string> LettersDictionary = new Dictionary<string, string>
        {
            ["۰"] = "0",
            ["۱"] = "1",
            ["۲"] = "2",
            ["۳"] = "3",
            ["۴"] = "4",
            ["۵"] = "5",
            ["۶"] = "6",
            ["۷"] = "7",
            ["۸"] = "8",
            ["۹"] = "9"
        };
        return LettersDictionary.Aggregate(value, (current, item) =>
            current.Replace(item.Key, item.Value));
    }
}