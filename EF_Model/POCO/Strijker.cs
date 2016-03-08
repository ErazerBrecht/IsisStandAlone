using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PropertyChanged;

namespace EF_Model
{
    public partial class Strijker : INotifyPropertyChanged

    {
        public Strijker()
        {
            Datums = new ObservableCollection<Datum>();
        }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int? Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Naam { get; set; }

        [Required]
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

        [StringLength(20)]
        public string Tel { get; set; }

        [Required]
        [StringLength(20)]
        public string RNSZ { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(6)]
        public string Login { get; set; }

        [StringLength(19)]
        public string Bankrekening { get; set; }

        [Column(TypeName = "date")]
        public DateTime? IndienstVanaf { get; set; }

        [Column(TypeName = "date")]
        [AlsoNotifyFor("ActiefBool")]
        public DateTime? IndienstTot { get; set; }

        [Required]
        [Range(0, 38)]
        public int? UrenTewerkstelling { get; set; }

        public virtual ObservableCollection<Datum> Datums { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
