using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;

namespace ISIS
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
            "ID",
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
            "SoortKlant",
            "SoortKlantPlaats"
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
                        if (!String.IsNullOrEmpty(Voornaam) && Voornaam.Length > 50)
                            result = "Voornaam mag maximum uit 50 karakters bestaan!";
                        break;
                    }

                case "ID":
                    {
                        if (CanValidateID == true)
                        {
                            ISIS_DataEntities entities = new ISIS_DataEntities();
                            if ((entities.Klanten.Any(k => k.ID == ID)))
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

                case "Telefoon":
                    {
                        if (!String.IsNullOrEmpty(Telefoon) && (!Telefoon.All(char.IsDigit) || Telefoon.Length != 9))
                        {
                            result = "Geen geldig telefoonnummer!";
                        }
                        if (String.IsNullOrEmpty(Gsm))
                            this.Gsm = null;     //Force validation for GSM

                        break;
                    }

                case "Gsm":
                    {
                        if (String.IsNullOrEmpty(_gsm))
                        {
                            if (String.IsNullOrEmpty(Telefoon))
                                result = "U dient ofwel een telefoon of gsm in te vullen!";
                        }
                        else if (!_gsm.All(char.IsDigit) || _gsm.Length != 10)
                        {
                            result = "Geen geldig gsmnummer!";
                        }
                        break;
                    }
                case "Email":
                    {
                        if (!String.IsNullOrEmpty(Email) && Email.Length > 50)
                            result = "E-mail mag maximum uit 50 karakters bestaan!";
                        break;
                    }
                case "AndereNaam":
                    {
                        if (!String.IsNullOrEmpty(AndereNaam) && AndereNaam.Length > 50)
                            result = "'Andere Naam' mag maximum uit 50 karakters bestaan!";
                        break;
                    }

                case "Betalingswijze":
                    {
                        if (String.IsNullOrWhiteSpace(Betalingswijze))
                            result = "Betalingswijze moet verplicht gekozen worden!";
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

                case "SoortKlant":
                    {
                        if (String.IsNullOrWhiteSpace(SoortKlant))
                            result = "Soort klant moet verplicht gekozen worden!";
                        break;
                    }
                case "SoortKlantPlaats":
                    {
                        if (String.IsNullOrWhiteSpace(SoortKlantPlaats))
                            result = "Gelieve de soort klant te specificeren!";
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
        //Is used to check if we have to validate the id of the Klant
        //The validation of a klant only has to happen when the user is in the add mode (=> ButtonContent = Annuleren)
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
