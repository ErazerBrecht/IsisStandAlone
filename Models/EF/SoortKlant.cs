namespace ISIS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SoortKlant")]
    public partial class SoortKlant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SoortKlant()
        {
            Klanten = new HashSet<Klant>();
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string Type { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Naam { get; set; }

        public decimal SnelheidsCoëfficiënt { get; set; }

        [Column(TypeName = "money")]
        public decimal Euro { get; set; }

        public bool StukTarief { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Klant> Klanten { get; set; }
    }
}
