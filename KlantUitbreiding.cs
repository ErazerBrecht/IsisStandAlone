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
                string result = null;

                switch (columnName)
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
                                ISIS_DataEntities _viewModel = new ISIS_DataEntities();
                                if ((_viewModel.Klanten.Any(k => k.ID == ID)))
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
                            if (!String.IsNullOrEmpty(Gsm) && (!Gsm.All(char.IsDigit) || Gsm.Length != 10))
                            {
                              result = "Geen geldig gsmnummer!";
                            }
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
                                if (ElektronischBetalen == Visibility.Visible)
                                {
                                    //TODO: Check length!
                                    if (String.IsNullOrWhiteSpace(Gebruikersnummer))
                                        result = "U hebt gekozen voor Elektronisch betalen dan is een Gebruikersnummer verplicht";
                                }
                            }
                            break;
                        }

                    case "SoortKlant":
                        {
                            if (String.IsNullOrWhiteSpace(SoortKlant))
                                result = "Soort klant moet verplicht gekozen worden!";
                            break;
                        }
                }

                return result;
            }
        }

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

        public Visibility ElektronischBetalen
        {
            get
            {
                if (this.Betalingswijze == "Elektronisch")
                {
                    if (Gebruikersnummer == null)       //Force validation --> Manual add something, this will cause validationerror!
                        this.Gebruikersnummer = "";
                    return Visibility.Visible;
                }
                else
                {           
                    this.Gebruikersnummer = null;         //Force validation --> Otherwise the error still keeps => unable to save
                    return Visibility.Hidden;
                }
            }
        }

        public override string ToString()
        {
            return ID + "  " + Naam + "  " + Voornaam; ;
        }
    }
}
