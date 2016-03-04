using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Model
{
    public abstract partial class Prestatie : INotifyPropertyChanged
    {
        protected Prestatie()
        {
            Datum = new ObservableCollection<Datum>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public int TotaalBetalen { get; set; }

        //Relationships
        public virtual ObservableCollection<Datum> Datum { get; set; }
        public virtual Klant Klant { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}