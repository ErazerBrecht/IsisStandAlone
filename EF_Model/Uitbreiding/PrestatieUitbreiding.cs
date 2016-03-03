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
                    return ((StukPrestatie) this).TotaalMinuten;
                if (this is TijdPrestatie)
                {
                    var tijdPrestatie = (TijdPrestatie) this;

                    if (tijdPrestatie.TotaalMinuten > 0)
                        return tijdPrestatie.TotaalMinuten;

                    tijdPrestatie.TotaalHemden = (int)Math.Ceiling(Convert.ToByte(tijdPrestatie.AantalHemden) * tijdPrestatie.ParameterHemden);
                    tijdPrestatie.TotaalLakens1 = (int)Math.Ceiling(Convert.ToByte(tijdPrestatie.AantalLakens1) * tijdPrestatie.ParameterLakens1);
                    tijdPrestatie.TotaalLakens2 = (int)Math.Ceiling(Convert.ToByte(tijdPrestatie.AantalLakens2) * tijdPrestatie.ParameterLakens2);
                    tijdPrestatie.TotaalAndereStrijk = (int)Math.Ceiling(Convert.ToByte(tijdPrestatie.TijdAndereStrijk) * tijdPrestatie.ParameterAndereStrijk);
                    tijdPrestatie.TotaalStrijk = Convert.ToInt32(tijdPrestatie.AantalHemden + tijdPrestatie.AantalLakens1 + tijdPrestatie.AantalLakens2 + tijdPrestatie.AantalAndereStrijk);

                    //Calculate the "administratie" time
                    if (tijdPrestatie.TotaalStrijk < 20)
                        tijdPrestatie.TijdAdministratie = 5;
                    else if (tijdPrestatie.TotaalStrijk < 40)
                        tijdPrestatie.TijdAdministratie = 10;
                    else if (tijdPrestatie.TotaalStrijk < 80)
                        tijdPrestatie.TijdAdministratie = 15;
                    else
                        tijdPrestatie.TijdAdministratie = 20;

                    tijdPrestatie.TotaalMinuten = Convert.ToInt32(tijdPrestatie.TotaalHemden + tijdPrestatie.TotaalLakens1 + tijdPrestatie.TotaalLakens2 + tijdPrestatie.TotaalAndereStrijk + tijdPrestatie.TijdAdministratie);
                    return tijdPrestatie.TotaalMinuten;
                }
                return 0;
            }
        }
        [NotMapped]
        public int TotaalBetalen { get; set; }
        [NotMapped]
        public int NieuwTegoed { get; set; }
    }
}
