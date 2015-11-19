using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISIS.Models;
using ISIS.Commands;

namespace ISIS.ViewModels
{
    class OphalingBeheerViewModel : SoortKlantBeheerViewModel
    {
        public OphalingBeheerViewModel()
        {
            AddSoort.Type = "Ophaling";
        }

        public override void Refresh()
        {
            base.Refresh();
            ViewSource.Source = ctx.SoortKlant.Local.Where(s => s.Type == "Ophaling");
        }

        public override void SaveChanges()
        {
            base.SaveChanges();
            AddSoort.Type = "Ophaling";
        }
    }
}
