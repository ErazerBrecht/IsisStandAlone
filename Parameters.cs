using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ISIS
{
    public class Parameters : INotifyPropertyChanged
    {
        public Parameters()
        {
            ParameterHemden = Properties.Settings.Default.ParameterHemden;
            ParameterLakens1 = Properties.Settings.Default.ParameterLakens1;
            ParameterLakens2 = Properties.Settings.Default.ParameterLakens2;
            ParameterAndereStrijk = Properties.Settings.Default.ParameterAndereStrijk;
        }

        private byte _parameterHemden;
        public byte ParameterHemden
        {
            get { return _parameterHemden; }
            set
            {
                _parameterHemden = value;
                NoticeMe("ParameterHemden");
            }
        }

        private byte _parameterLakens1;
        public byte ParameterLakens1
        {
            get { return _parameterLakens1; }
            set
            {
                _parameterLakens1 = value;
                NoticeMe("ParameterLakens1");
            }
        }

        private byte _parameterLakens2;
        public byte ParameterLakens2
        {
            get { return _parameterLakens2; }
            set
            {
                _parameterLakens2 = value;
                NoticeMe("ParameterLakens2");
            }
        }

        private byte _parameterAndereStrijk;
        public byte ParameterAndereStrijk
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
