using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.ComponentModel;

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
                        CanSave = false;
                    }
                    else
                    {
                        CanSave = true;
                    }
                }
                if (columnName == "ID")
                {
                    ISIS_DataEntities _viewModel = new ISIS_DataEntities();
                    if ((_viewModel.Klanten.Any(k => k.ID == ID)))
                    {
                        result = "ID is al in gebruik!";
                        CanSave = false;
                    }
                    else
                    {
                        CanSave = true;
                    }
                }

                return result;
            }
        }

        private bool _canSave;
        public bool CanSave
        {
            get { return _canSave; }
            set
            {
                _canSave = value;
                OnPropertyChanged("CanSave");
            }
        }
    }
}
