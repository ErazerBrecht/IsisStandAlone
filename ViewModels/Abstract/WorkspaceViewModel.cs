using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ISIS.Models;

namespace ISIS.ViewModels
{
    public abstract class WorkspaceViewModel : INotifyPropertyChanged
    {
        private string _header;

        public string Header
        {
            get { return _header; }
            protected set {
                _header = value;
                NoticeMe("Header");
            }
        }

        public ISIS_DataEntities ctx { get; set; }

        public virtual void LoadData()
        {
            //TODO
        }

        #region INotifyPropertyChanged
        protected void NoticeMe(string property)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
