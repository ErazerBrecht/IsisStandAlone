using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_Repository;

namespace PL_WPF.ViewModels
{
    class WinkelTypeViewModel : KlantTypeViewModel
    {
        public WinkelTypeViewModel(UnitOfWork ctx) : base(ctx)
        {
            AddType.Type = "Winkel";
        }

        public override void GetData()
        {
            ViewSource.Source = Ctx.KlantTypes.Find(s => s.Type == "Winkel");
        }

        public override void Add()
        {
            base.Add();
            AddType.Type = "Winkel";
        }
    }
}
