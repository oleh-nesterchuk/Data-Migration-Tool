using System;
using System.ComponentModel.DataAnnotations;

namespace DataMigrationApi.Core.Entities
{
    public class BirthDateAttribute : ValidationAttribute
    {
        public string GetErrorMessage() =>
            "A person should have a valid birth date.";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var birthDate = (DateTime)value;
            if (birthDate > DateTime.UtcNow || birthDate.Year < 1900)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}
