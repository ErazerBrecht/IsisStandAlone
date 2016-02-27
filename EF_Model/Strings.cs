using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EF_Model
{
    public class Strings : INotifyPropertyChanged
    {
        public Strings()
        {
            //Load Strings
            LoadStrings();
        }

        public void LoadStrings()
        {
            StringHemden = Properties.Settings.Default.StringHemden;
            StringLakens1 = Properties.Settings.Default.StringLakens1;
            StringLakens2 = Properties.Settings.Default.StringLakens2;
            StringAndere = Properties.Settings.Default.StringAndere;
            StringAdministratie = Properties.Settings.Default.StringAdministratie;
        }

        public void SaveStrings()
        {
            Properties.Settings.Default.StringHemden = StringHemden;
            Properties.Settings.Default.StringLakens1 = StringLakens1;
            Properties.Settings.Default.StringLakens2 = StringLakens2;
            Properties.Settings.Default.StringAndere = StringAndere;
            Properties.Settings.Default.StringAdministratie = StringAdministratie;
            Properties.Settings.Default.Save();
        }

        public string StringHemden { get; set; }
        public string StringLakens1 { get; set; }
        public string StringLakens2 { get; set; }
        public string StringAndere { get; set; }
        public string StringAdministratie { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
