using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISIS.Models;

namespace ISIS.Models
{
    public partial class TijdPrestatie
    {
        //Here I'll add properties that doesn't need to be saved in the database!
        //Because the value of these properties can be calculated out of the existing fields in the db!

        public void AddParameters(Parameters p)
        {
            ParameterHemden = p.ParameterHemden;
            ParameterLakens1 = p.ParameterLakens1;
            ParameterLakens2 = p.ParameterLakens2;
            ParameterAndereStrijk = p.ParameterAndereStrijk;
        }

        [NotMapped]
        public int TotaalStrijk { get; set; }
        [NotMapped]
        public int TotaalHemden { get; set; }
        [NotMapped]
        public int TotaalLakens1 { get; set; }
        [NotMapped]
        public int TotaalLakens2 { get; set; }
        [NotMapped]
        public int TotaalAndereStrijk { get; set; }
    }
}
