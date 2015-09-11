using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISIS.Models;
using ISIS.Commands;

namespace ISIS.ViewModels
{
    class ParameterBeheerViewModel: WorkspaceViewModel
    {
        public Parameters  ParameterData { get; set; }
        public SaveParameterCommand SaveParameterCommandEvent { get; set; }
        public RefreshParameterCommand RefreshParameterCommandEvent { get; set; }

        public ParameterBeheerViewModel()
        {
            Header = "ParameterBeheer";
            LoadParameters();
            SaveParameterCommandEvent = new SaveParameterCommand(this);
            RefreshParameterCommandEvent = new RefreshParameterCommand(this);
        }

        public void LoadParameters()
        {
            ParameterData = new Parameters();
            ParameterData.ParameterHemden = Properties.Settings.Default.ParameterHemden;
            ParameterData.ParameterLakens1 = Properties.Settings.Default.ParameterLakens1;
            ParameterData.ParameterLakens2 = Properties.Settings.Default.ParameterLakens2;
            ParameterData.ParameterAndereStrijk = Properties.Settings.Default.ParameterAndereStrijk;
        }

        public void SaveParameters()
        {
            Properties.Settings.Default.ParameterHemden = ParameterData.ParameterHemden;
            Properties.Settings.Default.ParameterLakens1 = ParameterData.ParameterLakens1;
            Properties.Settings.Default.ParameterLakens2 = ParameterData.ParameterLakens2;
            Properties.Settings.Default.ParameterAndereStrijk = ParameterData.ParameterAndereStrijk;
            Properties.Settings.Default.Save();
        }
    }
}
