using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ISIS.ViewModels
{
    abstract class WorkspaceViewModel : INotifyPropertyChanged
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

        #region INotifyPropertyChanged
        private void NoticeMe(string property)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
