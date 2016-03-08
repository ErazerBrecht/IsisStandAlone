using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows;

namespace EF_Model
{
    partial class Strijker : IDataErrorInfo
    {
        public bool CanSave => ValidatedProperties.All(property => GetValidationError(property) == null);

        #region Data Validiation
        static readonly string[] ValidatedProperties =
{
            "Naam",
            "Voornaam",
            "Id",
            "Straat",
            "Nummer",
            "Gemeente",
            "Postcode",
            "RNSZ",
            "Login",
            "Email",
            "UrenTewerkstelling",
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
                    case "ID":
                        {
                            if (InvalidId == true)
                                result = "ID is al in gebruik!";

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
        public bool InvalidId { get; set; }

        [NotMapped]
        public bool ActiefBool => IndienstTot == null;

        public override string ToString() => Voornaam;
    }
}
