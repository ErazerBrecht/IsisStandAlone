using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;

namespace ISIS
{
    partial class Strijker : IDataErrorInfo
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

        #region Data Validiation
        static readonly string[] ValidatedProperties =
{
            "Naam",
            "Voornaam",
            "ID",
            "Straat",
            "Nummer",
            "Gemeente",
            "Postcode",
            "RNSZ",
            "Gebruikersnummer",
            "UrenTewerkstelling",
        };

        public string GetValidationError(string propertyName)
        {
            string result = null;

            switch (propertyName)
            {
                case "Naam":
                    {
                        if (String.IsNullOrWhiteSpace(Naam))
                            result = "Naam moet verplicht ingevuld worden!";
                        else if (Naam.Length > 50)
                            result = "Naam mag maximum uit 50 karakters bestaan!";
                        break;
                    }
                case "ID":
                    {
                        if (CanValidateID == true)
                        {
                            ISIS_DataEntities entities = new ISIS_DataEntities();
                            if ((entities.Strijkers.Any(k => k.ID == ID)))
                            {
                                result = "ID is al in gebruik!";
                            }
                        }
                        break;
                    }
                case "Straat":
                    {
                        if (String.IsNullOrWhiteSpace(Straat))
                            result = "Straat moet verplicht ingevuld worden!";
                        break;
                    }

                case "Nummer":
                    {
                        if (String.IsNullOrWhiteSpace(Convert.ToString(Nummer)))
                            result = "Nummer moet verplicht ingevuld worden!";
                        break;
                    }
                case "Gemeente":
                    {
                        if (String.IsNullOrWhiteSpace(Gemeente))
                            result = "Gemeente moet verplicht ingevuld worden!";
                        break;
                    }
                case "Postcode":
                    {
                        if (String.IsNullOrWhiteSpace(Convert.ToString(Postcode)))
                            result = "Gemeente moet verplicht ingevuld worden!";
                        break;
                    }
                case "RNSZ":
                    {
                        if (String.IsNullOrWhiteSpace(RNSZ))
                            result = "RNSZ nummer moet verplicht ingevuld worden!";
                        break;
                    }
                case "UrenTewerkstelling":
                    {
                        if (String.IsNullOrWhiteSpace(Convert.ToString(UrenTewerkstelling)))
                            result = "Uren tewerkstelling moet verplicht ingevuld worden!";
                        if (UrenTewerkstelling > 38)
                            result = "Uren tewerkstelling mag maximaal 38 zijn!";
                        break;
                    }
            }

            return result;
        }
        #endregion

        #region Interface Implementation
        public string Error
        {
            get
            {
                return "";
            }
        }

        public string this[string columnName]
        {
            get
            {
                return GetValidationError(columnName);
            }
        }
        #endregion

        #region CanValidate ID fullproperty
        private bool _canValidateID;
        public bool CanValidateID
        {
            get { return _canValidateID; }
            set
            {
                _canValidateID = value;
                OnPropertyChanged("CanValidateID");
            }
        }
        #endregion
    }
}
