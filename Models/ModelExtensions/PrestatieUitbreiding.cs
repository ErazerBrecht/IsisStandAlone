using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISIS.Models;

namespace ISIS.Models
{
    public partial class Prestatie
    {
        //Here I'll add properties that doesn't need to be saved in the database!
        //Because the value of these properties can be calculated out of the existing fields in the db!

        [NotMapped]
        public int TotaalMinuten { get; set; }
        [NotMapped]
        public int TotaalBetalen { get; set; }
        [NotMapped]
        public int NieuwTegoed { get; set; }
    }
}
