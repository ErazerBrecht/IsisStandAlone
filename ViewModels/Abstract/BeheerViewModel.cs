using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ISIS.ViewModels
{
    abstract class BeheerViewModel : WorkspaceViewModel
    {
        protected ISIS_DataEntities ctx;
        public CollectionViewSource ViewSource { get; protected set; }     
    
        abstract public string ButtonToevoegenContent { get; set; }

        public BeheerViewModel()
        {
            ViewSource = new CollectionViewSource();
        }

        public abstract void Delete();
        public abstract void Refresh();
        public abstract void Add();
        public abstract void SaveChanges();
        protected abstract void View_CurrentChanged(object sender, EventArgs e);
    }
}
