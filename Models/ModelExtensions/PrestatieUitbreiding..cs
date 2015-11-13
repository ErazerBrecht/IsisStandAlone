using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISIS.Models;

namespace ISIS
{
    public partial class Prestatie
    {
        //Here I'll add properties that doesn't need to be saved in the database!
        //Because the value of these properties can be calculated out of the existing fields in the db!

        private int _totaalMinuten;
        public int TotaalMinuten
        {
            get
            {
                return _totaalMinuten;
            }
            set
            {
                _totaalMinuten = value;
                OnPropertyChanged("TotaalMinuten");
            }
        }

        private int _totaalBetalen;
        public int TotaalBetalen
        {
            get
            {
                return _totaalBetalen;
            }
            set
            {
                _totaalBetalen = value;
                OnPropertyChanged("TotaalBetalen");
            }
        }

        private int _nieuwTegoed;
        public int NieuwTegoed
        {
            get
            {
                return _nieuwTegoed;
            }
            set
            {
                _nieuwTegoed = value;
                OnPropertyChanged("NieuwTegoed");
            }
        }
    }
}
