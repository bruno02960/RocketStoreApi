using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace RocketStoreApi.Models.CustomAttributes
{
    /// <summary>
    /// Custom validation attribute for validating VAT numbers.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class VatNumberAttribute : ValidationAttribute
    {    
        /// <summary>
        /// Validates a VAT number based on a specified regular expression pattern.
        /// </summary>
        /// <param name="value">The value to validate as a VAT number.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>
        /// A <see cref="ValidationResult"/> indicating whether the VAT number is valid.
        /// If the VAT number is valid, returns <see cref="ValidationResult.Success"/>; otherwise, returns a validation error message.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            string vatNumber = value.ToString();

            string pattern = "^[0-9]{9}$";

            if (Regex.IsMatch(vatNumber, pattern))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("The field VAT Number must match the regular expression '^[0-9]{9}$'.");
        }
    }
}
