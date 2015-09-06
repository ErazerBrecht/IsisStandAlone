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
        public CollectionView ViewSource { get; protected set; }
    }
}
