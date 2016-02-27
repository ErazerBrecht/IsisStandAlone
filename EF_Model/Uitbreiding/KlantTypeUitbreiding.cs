using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EF_Model
{
    public partial class KlantType : IDataErrorInfo
    {
        public bool CanSave
        {
            get
            {
                foreach (string property in ValidatedProperties)
                {
                    if (GetValidationError(property) != null)   // there is an error
                        return false;
                }

                return true;
            }
        }

        #region Data Validation
        static readonly string[] ValidatedProperties =
        {
            //"Type",
            "Naam",
            "SnelheidsCoëfficiënt",
            "Euro"
        };

        public string GetValidationError(string propertyName)
        {
            string result = null;

            var validationResults = new List<ValidationResult>();

            Validator.TryValidateProperty(
                GetType().GetProperty(propertyName).GetValue(this),
                new ValidationContext(this)
                {
                    MemberName = propertyName
                }, validationResults);

            if (validationResults.Count > 0)
                result = validationResults.First().ErrorMessage;

            else
                switch (propertyName)
                {
                    case "Naam":
                        {
                            if (InvalidKey == true)
                                result = "Dit type is al aangemaakt!";
                            break;
                        }
                }

            return result;
        }
        #endregion

        #region Interface Implementation
        public string Error => "";
        public string this[string columnName] => GetValidationError(columnName);

        #endregion

        //Is used to check if our ID is unique!
        [NotMapped]
        public bool InvalidKey { get; set; }
        public override string ToString() => this.Naam;
    }
}
