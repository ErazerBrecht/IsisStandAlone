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
    }
}
