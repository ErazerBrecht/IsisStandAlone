using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EF_Model
{
    public class BegroetingPrint : INotifyPropertyChanged
    {
        public BegroetingPrint()
        {
            //Load Strings
            LoadStrings();
        }

        public void LoadStrings()
        {
            StringRegel1 = Properties.Settings.Default.StringRegel1;
            StringRegel2 = Properties.Settings.Default.StringRegel2;
        }

        public void SaveStrings()
        {
            Properties.Settings.Default.StringRegel1 = StringRegel1;
            Properties.Settings.Default.StringRegel2 = StringRegel2;
            Properties.Settings.Default.Save();
        }

        public string StringRegel1 { get; set; }
        public string StringRegel2 { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
