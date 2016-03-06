using System;
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
                    return ((StukPrestatie)this).TotaalMinuten;
                if (this is TijdPrestatie)
                    return ((TijdPrestatie)this).TotaalMinuten;
                return 0;
            }
        }

        [NotMapped]
        public byte TotaalDienstenChecks { get; set; }

        [NotMapped]
        public int TotaalBetalen { get; set; }

        [NotMapped]
        public int NieuwTegoed { get; set; }

        [NotMapped]
        public bool IsTijdPrestatie => this is TijdPrestatie;

        public abstract void CalculatePrestatie();
        public abstract byte RecalculatePrestatie(byte newTegoed);
    }
}
