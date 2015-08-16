using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;

namespace ISIS
{
    partial class Klanten : IDataErrorInfo
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

                if (columnName == "Naam")
                {
                    if (String.IsNullOrWhiteSpace(Naam))
                    {
                        result = "Naam moet verplicht ingevuld worden!";
                    }
                }
                if (columnName == "ID")
                {
                    if (CanValidateID == true)
                    {
                        ISIS_DataEntities _viewModel = new ISIS_DataEntities();
                        if ((_viewModel.Klanten.Any(k => k.ID == ID)))
                        {
                            result = "ID is al in gebruik!";
                        }
                    }
                }
                if (columnName == "Gebruikersnummer")
                {
                    if (this.Betalingswijze == "Elektronisch")
                    {
                        if (String.IsNullOrWhiteSpace(Gebruikersnummer))
                        {
                            result = "U hebt gekozen voor elektronisch betalen, gelieve het gebruikersnummer in te vullen!";
                        }
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
                    if (Gebruikersnummer == null)
                        this.Gebruikersnummer = "";
                    return Visibility.Visible;
                }
                else
                    return Visibility.Hidden;
            }
        }

        public override string ToString()
        {
            return ID + "  " + Naam + "  " + Voornaam; ;
        }
    }
}
