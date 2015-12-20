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
    [Table("SoortKlant")]
    public partial class SoortKlant : INotifyPropertyChanged
    { 
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

        public virtual ICollection<Klant> Klanten { get; set; }

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
