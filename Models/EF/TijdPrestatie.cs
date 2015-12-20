namespace ISIS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TijdPrestatie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public byte AantalHemden { get; set; }

        public decimal ParameterHemden { get; set; }

        public byte AantalLakens1 { get; set; }

        public decimal ParameterLakens1 { get; set; }

        public byte AantalLakens2 { get; set; }

        public decimal ParameterLakens2 { get; set; }

        public byte AantalAndereStrijk { get; set; }

        public byte TijdAndereStrijk { get; set; }

        public decimal ParameterAndereStrijk { get; set; }

        public byte TijdAdministratie { get; set; }

        public virtual Prestatie Prestaties { get; set; }
    }
}
