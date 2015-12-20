namespace ISIS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Strijker
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Strijker()
        {
            Datum = new HashSet<Datum>();
            Datum1 = new HashSet<Datum>();
            Datum2 = new HashSet<Datum>();
            Datum3 = new HashSet<Datum>();
            Datum4 = new HashSet<Datum>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Naam { get; set; }

        [StringLength(50)]
        public string Straat { get; set; }

        public int? Nummer { get; set; }

        public int? Postcode { get; set; }

        [StringLength(50)]
        public string Gemeente { get; set; }

        public int? Tel { get; set; }

        [Required]
        [StringLength(20)]
        public string RNSZ { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(6)]
        public string Login { get; set; }

        [StringLength(19)]
        public string Bankrekening { get; set; }

        [Column(TypeName = "date")]
        public DateTime? IndienstVanaf { get; set; }

        public int? UrenTewerkstelling { get; set; }

        [StringLength(50)]
        public string Voornaam { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Datum> Datum { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Datum> Datum1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Datum> Datum2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Datum> Datum3 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Datum> Datum4 { get; set; }
    }
}
