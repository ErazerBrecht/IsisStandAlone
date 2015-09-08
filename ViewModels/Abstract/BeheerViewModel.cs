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
        public CollectionViewSource ViewSource { get; protected set; }

        public BeheerViewModel()
        {
            ViewSource = new CollectionViewSource();
        }

        protected abstract void View_CurrentChanged(object sender, EventArgs e);
    }
}
