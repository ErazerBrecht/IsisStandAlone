using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIS
{
    public static class SoortKlant
    {
        public static void Load()
        {
            //Need ObservableCollection for beter binding => Otherwise the changes won't update in the UI!
            Winkels = new ObservableCollection<string>(Properties.Settings.Default.Winkels.Cast<string>().ToList());
            Bedrijven = new ObservableCollection<string>(Properties.Settings.Default.Bedrijven.Cast<string>().ToList());
            Scholen = new ObservableCollection<string>(Properties.Settings.Default.Scholen.Cast<string>().ToList());
            Ophaling = new ObservableCollection<string>(Properties.Settings.Default.Ophaling.Cast<string>().ToList());
        }
       

        public static void Save()
        {
            StringCollection saveWinkels = new StringCollection();
            saveWinkels.AddRange(Winkels.ToArray());

            StringCollection saveBedrijven = new StringCollection();
            saveBedrijven.AddRange(Bedrijven.ToArray());

            StringCollection saveOphaling = new StringCollection();
            saveOphaling.AddRange(Ophaling.ToArray());

            StringCollection saveScholen = new StringCollection();
            saveScholen.AddRange(Scholen.ToArray());

            Properties.Settings.Default.Winkels = saveWinkels;
            Properties.Settings.Default.Bedrijven = saveBedrijven;
            Properties.Settings.Default.Scholen = saveScholen;
            Properties.Settings.Default.Ophaling = saveOphaling;
            Properties.Settings.Default.Save();
        }

        public static ObservableCollection<string> Winkels {get; set;}
        public static ObservableCollection<string> Bedrijven { get; set; }
        public static ObservableCollection<string> Scholen { get; set; }
        public static ObservableCollection<string> Ophaling { get; set; }
    }
}
