namespace ISIS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Prestatie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prestatie()
        {
            Datum = new HashSet<Datum>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int KlantenNummer { get; set; }

        public byte TotaalDienstenChecks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Datum> Datum { get; set; }

        public virtual Klant Klanten { get; set; }

        public virtual StukPrestatie StukPrestaties { get; set; }

        public virtual TijdPrestatie TijdPrestaties { get; set; }
    }
}
