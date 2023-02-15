using EmcureCERI.Web.Models.DRFViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmcureCERI.Web.Classes
{
    public class RequiredIfAttribute : ValidationAttribute, IClientModelValidator
    {
        public void AddValidation(ClientModelValidationContext context)
        {            
            MergeAttribute(context.Attributes, "data-val", "true");
            MergeAttribute(context.Attributes, "data-val-required", FormatErrorMessage(context.ModelMetadata.DisplayName));            
        }

        public override string FormatErrorMessage(string name)
        {
            return "The " + name + " field is required.";
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var productType = (DRFInitialization)validationContext.ObjectInstance;
            if (productType.ProductTypeID == 1)
                return ValidationResult.Success;                
            else
            {
                if (value == null)
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
                else
                {
                    return ValidationResult.Success;
                }                
            }           
        }

        private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);
            return true;
        }
    }
}
