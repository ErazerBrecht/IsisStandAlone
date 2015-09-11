using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ISIS.Models
{
    public class Parameters : INotifyPropertyChanged
    {
        /*public Parameters()
        {
        }

        public void SaveParameters()
        {
            Properties.Settings.Default.ParameterHemden = ParameterHemden;
            Properties.Settings.Default.ParameterLakens1 = ParameterLakens1;
            Properties.Settings.Default.ParameterLakens2 = ParameterLakens2;
            Properties.Settings.Default.ParameterAndereStrijk = ParameterAndereStrijk;
            Properties.Settings.Default.Save();
        }*/

        private decimal _parameterHemden;
        public decimal ParameterHemden
        {
            get { return _parameterHemden; }
            set
            {
                _parameterHemden = value;
                NoticeMe("ParameterHemden");
            }
        }

        private decimal _parameterLakens1;
        public decimal ParameterLakens1
        {
            get { return _parameterLakens1; }
            set
            {
                _parameterLakens1 = value;
                NoticeMe("ParameterLakens1");
            }
        }

        private decimal _parameterLakens2;
        public decimal ParameterLakens2
        {
            get { return _parameterLakens2; }
            set
            {
                _parameterLakens2 = value;
                NoticeMe("ParameterLakens2");
            }
        }

        private decimal _parameterAndereStrijk;
        public decimal ParameterAndereStrijk
        {
            get { return _parameterAndereStrijk; }
            set
            {
                _parameterAndereStrijk = value;
                NoticeMe("ParameterAndereStrijk");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NoticeMe(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

    }
}
