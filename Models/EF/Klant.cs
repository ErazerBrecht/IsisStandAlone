using PropertyChanged;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ISIS.Annotations;

namespace ISIS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [ImplementPropertyChanged]
    [Table("Klanten")]
    public partial class Klant : INotifyPropertyChanged
    {
        public Klant()
        {
            Prestaties = new HashSet<Prestatie>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(13)]
        public string Gebruikersnummer { get; set; }

        [Required]
        [StringLength(50)]
        public string Naam { get; set; }

        [StringLength(50)]
        public string Voornaam { get; set; }

        [StringLength(50)]
        public string Straat { get; set; }

        public int? Nummer { get; set; }

        public int? Postcode { get; set; }

        [StringLength(50)]
        public string Gemeente { get; set; }

        [StringLength(15)]
        public string Telefoon { get; set; }

        [StringLength(15)]
        public string Gsm { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string AndereNaam { get; set; }

        [StringLength(12)]
        public string Betalingswijze { get; set; }

        [Required]
        [StringLength(10)]
        public string SoortKlantType { get; set; }

        [Required]
        [StringLength(50)]
        public string SoortKlantPlaats { get; set; }

        public int? Actief { get; set; }

        public int? Strijkbox { get; set; }

        public int? Waarborg { get; set; }

        [StringLength(4)]
        public string Bericht { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Datum { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LaatsteActiviteit { get; set; }

        public byte Tegoed { get; set; }

        public virtual SoortKlant SoortKlant { get; set; }

        public virtual ICollection<Prestatie> Prestaties { get; set; }

        #region PropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
