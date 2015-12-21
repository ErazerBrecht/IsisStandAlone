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

        private Strijker _strijker1;

        public virtual Strijker Strijker1
        {
            get { return _strijker1; }
            set
            {
                _strijker1 = value;
                OnPropertyChanged("Strijker1");
                OnPropertyChanged("Strijker2");
                OnPropertyChanged("Strijker3");
                OnPropertyChanged("Strijker4");
                OnPropertyChanged("Strijker5");
            }
        }

        private Strijker _strijker2;

        public virtual Strijker Strijker2
        {
            get { return _strijker2; }
            set
            {
                _strijker2 = value;
                OnPropertyChanged("Strijker2");
                OnPropertyChanged("Strijker3");
                OnPropertyChanged("Strijker4");
                OnPropertyChanged("Strijker5");
            }
        }

        private Strijker _strijker3;

        public virtual Strijker Strijker3
        {
            get { return _strijker3; }
            set
            {
                _strijker3= value;
                OnPropertyChanged("Strijker3");
                OnPropertyChanged("Strijker4");
                OnPropertyChanged("Strijker5");
            }
        }

        private Strijker _strijker4;

        public virtual Strijker Strijker4
        {
            get { return _strijker4; }
            set
            {
                _strijker4 = value;
                OnPropertyChanged("Strijker4");
                OnPropertyChanged("Strijker5");
            }
        }

        private Strijker _strijker5;

        public virtual Strijker Strijker5
        {
            get { return _strijker5; }
            set
            {
                _strijker5 = value;
                OnPropertyChanged("Strijker5");
            }
        }


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
