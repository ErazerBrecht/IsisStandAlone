using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Model
{
    public partial class Prestatie
    {
        //Here I'll add properties that doesn't need to be saved in the database!
        //Because the value of these properties can be calculated out of the existing fields in the db!

        [NotMapped]
        public int TotaalBetalen { get; set; }
        [NotMapped]
        public int NieuwTegoed { get; set; }
    }
}
