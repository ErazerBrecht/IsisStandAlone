using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PropertyChanged;

namespace EF_Model
{
 

    [Table("Datums")]
    public partial class Datum : INotifyPropertyChanged
    {
        public Datum()
        {
            Strijkers = new ObservableCollection<Strijker>();
        }

        [Key, Column(Order = 0)]
        public DateTime Date { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Prestatie")]
        public int PrestatieId { get; set; }

        public virtual Prestatie Prestatie { get; set; }

        public virtual ObservableCollection<Strijker> Strijkers { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
