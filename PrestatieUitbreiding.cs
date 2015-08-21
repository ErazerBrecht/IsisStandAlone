using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIS
{
    public partial class Prestatie
    {
        //Here I'll add properties that doesn't need to be saved in the database!
        //Because the value of these properties can be calculated out of the existing fields in the db!

        private int _totaalStrijk;
        public int TotaalStrijk {
            get
            {
                return _totaalStrijk;
            }
            set
            {
                _totaalStrijk = value;
                OnPropertyChanged("TotaalStrijk");
            }
        }

        private int _totaalHemden;
        public int TotaalHemden
        {
            get
            {
                return _totaalHemden;
            }
            set
            {
                _totaalHemden = value;
                OnPropertyChanged("TotaalHemden");
            }
        }

        private int _totaalLakens1;
        public int TotaalLakens1
        {
            get
            {
                return _totaalLakens1;
            }
            set
            {
                _totaalLakens1 = value;
                OnPropertyChanged("TotaalLakens1");
            }
        }

        private int _totaalLakens2;
        public int TotaalLakens2
        {
            get
            {
                return _totaalLakens2;
            }
            set
            {
                _totaalLakens2 = value;
                OnPropertyChanged("TotaalLakens2");
            }
        }

        private int _totaalAndereStrijk;
        public int TotaalAndereStrijk
        {
            get
            {
                return _totaalAndereStrijk;
            }
            set
            {
                _totaalAndereStrijk = value;
                OnPropertyChanged("TotaalAndereStrijk");
            }
        }

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
