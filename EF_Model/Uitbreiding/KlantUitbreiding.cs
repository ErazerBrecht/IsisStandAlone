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
    partial class Klant : IDataErrorInfo
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
            "Naam",
            "Voornaam",
            "Id",
            "Straat",
            "Nummer",
            "Gemeente",
            "Postcode",
            "Telefoon",
            "Gsm",
            "Email",
            "AndereNaam",
            "Betalingswijze",
            "Gebruikersnummer",
            "TypeNaam",
            "TypePlaats"
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

            if(validationResults.Count > 0)
                result = validationResults.First().ErrorMessage;

            else
                switch (propertyName)
                {
                    case "Id":
                        {
                            if (InvalidId == true)
                               result = "Klantnummer is al in gebruik!";
                            break;
                        }

                    case "Telefoon":
                        {
                            if (!String.IsNullOrEmpty(Telefoon) && (!Telefoon.All(char.IsDigit) || Telefoon.Length != 9))
                            {
                                result = "Geen geldig telefoonnummer!";
                            }
                            break;
                        }

                    case "Gsm":
                        {
                            if (String.IsNullOrEmpty(Gsm))
                            {
                                if (String.IsNullOrEmpty(Telefoon))
                                    result = "U dient ofwel een telefoon of gsm in te vullen!";
                            }
                            else if (!Gsm.All(char.IsDigit) || Gsm.Length != 10)
                            {
                                result = "Geen geldig gsmnummer!";
                            }
                            break;
                        }
           
                    case "Gebruikersnummer":
                        {
                            if (Betalingswijze == "Elektronisch")
                            {
                                if (String.IsNullOrWhiteSpace(Gebruikersnummer))
                                    result = "U hebt gekozen voor Elektronisch betalen dan is een Gebruikersnummer verplicht";
                                else if (Gebruikersnummer.Length != 13 || Gebruikersnummer[3] != ' ')
                                    result = "Niet geldig! Structuur = 123 123456789";
                            }
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

        //Convert 'Actief' number into text
        public string ActiefString
        {
            get
            {
                switch (Actief)
                {
                    case 0:
                        return "Gedeactiveerd";
                    case 1:
                        return "Actief";
                    case 2:
                        return "Slapend inactief";
                }
                return "";
            }
        }

        public override string ToString()
        {
            return Naam + "  " + Voornaam + "\t\t" + Id;
        }
    }
}
