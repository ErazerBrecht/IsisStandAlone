using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EF_Model
{
    public class Parameters : INotifyPropertyChanged
    {
        public Parameters()
        {
            //Load parameters
            LoadParameters();
        }

        public void LoadParameters()
        {
            ParameterHemden = Properties.Settings.Default.ParameterHemden;
            ParameterLakens1 = Properties.Settings.Default.ParameterLakens1;
            ParameterLakens2 = Properties.Settings.Default.ParameterLakens2;
        }

        
        public void SaveParameters()
        {
            Properties.Settings.Default.ParameterHemden = Convert.ToDecimal(ParameterHemden);
            Properties.Settings.Default.ParameterLakens1 = Convert.ToDecimal(ParameterLakens1);
            Properties.Settings.Default.ParameterLakens2 = Convert.ToDecimal(ParameterLakens2);
            Properties.Settings.Default.Save();
        }

        public decimal? ParameterHemden { get; set; }
        public decimal? ParameterLakens1 { get; set; }
        public decimal? ParameterLakens2 { get; set; }

        public decimal ParameterAndereStrijk { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}