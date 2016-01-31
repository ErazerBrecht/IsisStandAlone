using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ISIS.Annotations;
using System.Runtime.CompilerServices;
using PropertyChanged;

namespace ISIS.Models
{

    [ImplementPropertyChanged]
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
            //ParameterAndereStrijk = Properties.Settings.Default.ParameterAndereStrijk;
        }

        
        public void SaveParameters()
        {
            Properties.Settings.Default.ParameterHemden = ParameterHemden;
            Properties.Settings.Default.ParameterLakens1 = ParameterLakens1;
            Properties.Settings.Default.ParameterLakens2 = ParameterLakens2;
            //Properties.Settings.Default.ParameterAndereStrijk = ParameterAndereStrijk;
            Properties.Settings.Default.Save();
        }

        public decimal ParameterHemden { get; set; }
        public decimal ParameterLakens1 { get; set; }
        public decimal ParameterLakens2 { get; set; }
        public decimal ParameterAndereStrijk { get; set; }


        #region PropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
