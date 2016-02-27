using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Model
{
    public partial class TijdPrestatie : Prestatie, INotifyPropertyChanged
    {
        [Required(ErrorMessage = "Dit veld is vereist!")]
        public byte? AantalHemden { get; set; }

        [Required]
        public decimal ParameterHemden { get; set; }

        [Required (ErrorMessage ="Dit veld is vereist!")]
        public byte? AantalLakens1 { get; set; }

        [Required]
        public decimal ParameterLakens1 { get; set; }

        [Required(ErrorMessage = "Dit veld is vereist!")]
        public byte? AantalLakens2 { get; set; }

        [Required]
        public decimal ParameterLakens2 { get; set; }

        [Required(ErrorMessage = "Dit veld is vereist!")]
        public byte? AantalAndereStrijk { get; set; }

        [Required(ErrorMessage = "Dit veld is vereist!")]
        public byte? TijdAndereStrijk { get; set; }

        [Required]
        public decimal ParameterAndereStrijk { get; set; }

        [Required]
        public byte TijdAdministratie { get; set; }
    }
}
