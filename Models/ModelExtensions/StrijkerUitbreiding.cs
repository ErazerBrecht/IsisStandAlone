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
            "Login",
            "Email",
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
                case "Voornaam":
                    {
                        if (!String.IsNullOrEmpty(Voornaam) && (Voornaam.Length > 50))
                            result = "Voornaam mag maximum uit 50 karakters bestaan!";
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
                        else if (Straat.Length > 50)
                            result = "Straat mag maximum uit 50 karakters bestaan!";
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
                        else if (Gemeente.Length > 50)
                            result = "Gemeente mag maximum uit 50 karakters bestaan!";
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
                        else if (RNSZ.Length > 50)
                            result = "RNSZ nummer mag maximum uit 20 karakters bestaan!";      //TODO: Navragen!
                        break;
                    }
                case "Login":
                    {
                        if (String.IsNullOrWhiteSpace(Login))
                            result = "Login nummer moet verplicht ingevuld worden!";
                        else if (Login.Length > 6)
                            result = "Login nummer mag maximum uit 6 karakters bestaan!";
                        break;
                    }
                case "Email":
                    {
                        if (!String.IsNullOrEmpty(Email) && Email.Length > 50)
                            result = "E-mail mag maximum uit 50 karakters bestaan!";
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

        public override string ToString()
        {
            return ID + "  " + Naam + "  " + Voornaam; ;
        }
    }
}
