using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PropertyChanged;

namespace EF_Model
{
    [Table("Klanten")]
    public partial class Klant : INotifyPropertyChanged
    {
        public Klant()
        {
            Prestaties = new ObservableCollection<Prestatie>();
            Actief = 1;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(13)]
        public string Gebruikersnummer { get; set; }

        [Required]
        [StringLength(50)]
        public string Naam { get; set; }

        [StringLength(50)]
        public string Voornaam { get; set; }

        [Required]
        [StringLength(50)]
        public string Straat { get; set; }

        [Required]
        public int? Nummer { get; set; }

        [Required]
        public int? Postcode { get; set; }

        [Required]
        [StringLength(50)]
        public string Gemeente { get; set; }

        [StringLength(15)]
        [AlsoNotifyFor("Gsm")]
        public string Telefoon { get; set; }

        [StringLength(15)]
        public string Gsm { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string AndereNaam { get; set; }

        [Required]
        [StringLength(12)]
        [AlsoNotifyFor("Gebruikersnummer")]
        public string Betalingswijze { get; set; }

        [AlsoNotifyFor("ActiefString")]
        public byte Actief { get; set; }

        public byte? Strijkbox { get; set; }

        public int? Waarborg { get; set; }

        [StringLength(4)]
        public string Bericht { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Datum { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LaatsteActiviteit { get; set; }

        public byte Tegoed { get; set; }

        [Required]
        [StringLength(10)]
        [ForeignKey("KlantType"), Column(Order = 0)]
        public string TypeNaam { get; set; }

        [Required]
        [StringLength(50)]
        [ForeignKey("KlantType"), Column(Order = 1)]
        public string TypePlaats { get; set; }
        public virtual KlantType KlantType { get; set; }

        public virtual ObservableCollection<Prestatie> Prestaties { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
