using System.ComponentModel;
using System.Runtime.CompilerServices;
using ISIS.Annotations;
using PropertyChanged;

namespace ISIS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [ImplementPropertyChanged]
    [Table("Datum")]
    public partial class Datum : INotifyPropertyChanged
    {
        [Key]
        public DateTime Date { get; set; }

        public int Id { get; set; }

        public int Strijker1_ID { get; set; }

        public int? Strijker2_ID { get; set; }

        public int? Strijker3_ID { get; set; }

        public int? Strijker4_ID { get; set; }

        public int? Strijker5_ID { get; set; }

        public virtual Prestatie Prestaties { get; set; }

        public virtual Strijker Strijker1 { get; set; }

        public virtual Strijker Strijker2 { get; set; }

        public virtual Strijker Strijker3 { get; set; }

        public virtual Strijker Strijker4 { get; set; }

        public virtual Strijker Strijker5 { get; set; }


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
