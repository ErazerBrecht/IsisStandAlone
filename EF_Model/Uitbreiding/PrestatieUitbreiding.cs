﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Model
{
    public partial class Prestatie
    {
        //Here I'll add properties that doesn't need to be saved in the database!
        //Because the value of these properties can be calculated out of the existing fields in the db!

        [NotMapped]
        public int TotaalTijd
        {
            get
            {
                if (this is StukPrestatie)
                    return ((StukPrestatie) this).TotaalMinuten;
                if (this is TijdPrestatie)
                {
                    var tijdPrestatie = (TijdPrestatie) this;

                    if (tijdPrestatie.TotaalMinuten > 0)
                        return tijdPrestatie.TotaalMinuten;

                    tijdPrestatie.CalculateStrijk();
                    return tijdPrestatie.TotaalMinuten;
                }
                return 0;
            }
        }

        [NotMapped]
        public byte TotaalDienstenChecks => Convert.ToByte(Math.Ceiling(TotaalBetalen / 60.0));

        [NotMapped]
        public int Tegoed => TotaalBetalen - TotaalTijd;

        [NotMapped]
        private int _nieuwTegoed;
        
        [NotMapped]
        public int NieuwTegoed {
            get
            {
                //If we edit the last prestatie we set NiewTegoed manually (equals current Tegoed of Klant)
                //We want to get that value instead of newly calculate
                if (TotaalTijd > 0 || TotaalBetalen > 0)
                {
                    if (TotaalDienstenChecks == 0)
                        _nieuwTegoed = Tegoed + TotaalTijd;
                    else
                        _nieuwTegoed = (TotaalDienstenChecks*60) - TotaalBetalen;
                }

                return _nieuwTegoed;

            }
            set { _nieuwTegoed = value; }
        }
    }
}
