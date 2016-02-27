using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Model
{
    public partial class StukPrestatie : Prestatie, INotifyPropertyChanged
    {
        [Required]
        [Range (1, int.MaxValue)]
        public int TotaalMinuten { get; set; }
    }
}
