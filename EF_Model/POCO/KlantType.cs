using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Model
{
    [Table("KlantType")]
    public partial class KlantType : INotifyPropertyChanged
    {
        public KlantType()
        {
            Klanten = new ObservableCollection<Klant>();
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string Type { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        [Required]
        public string Naam { get; set; }

        [Required]
        [Range(0.00, 9.99)]
        public decimal SnelheidsCoëfficiënt { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Euro { get; set; }

        public bool StukTarief { get; set; }

        public virtual ObservableCollection<Klant> Klanten { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
