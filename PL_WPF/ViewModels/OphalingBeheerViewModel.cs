using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_Repository;

namespace PL_WPF.ViewModels
{
    class OphalingBeheerViewModel : KlantTypeViewModel
    {
        public OphalingBeheerViewModel(UnitOfWork ctx) : base(ctx)
        {
            AddType.Type = "Ophaling";
        }

        public override void GetData()
        {
            ViewSource.Source = Ctx.KlantTypes.Find(s => s.Type == "Ophaling");
        }

        public override void Add()
        {
            base.Add();
            AddType.Type = "Ophaling";
        }
    }
}
