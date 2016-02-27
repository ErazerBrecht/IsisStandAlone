using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_Repository;

namespace PL_WPF.ViewModels
{
    class BedrijfTypeViewModel : KlantTypeViewModel
    {
        public BedrijfTypeViewModel(UnitOfWork ctx) : base(ctx)
        {
            AddType.Type = "Bedrijf";
        }

        public override void GetData()
        {
            ViewSource.Source = Ctx.KlantTypes.Find(s => s.Type == "Bedrijf");
        }

        public override void Add()
        {
            base.Add();
            AddType.Type = "Bedrijf";
        }
    }
}
