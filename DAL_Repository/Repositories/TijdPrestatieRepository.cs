using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF_Context;
using EF_Context.Repositories;
using EF_Model;

namespace DAL_Repository.Repositories
{
    public class TijdPrestatieRepository : Repository<TijdPrestatie>, ITijdPrestatieRepository
    {
        public TijdPrestatieRepository(IsisContext context) : base(context)
        {
            context.TijdPrestaties.Load();
        }
  
        public bool Any()
        {
            return IsisContext.TijdPrestaties.Local.Any();
        }

        public TijdPrestatie GetLatestPrestatie(Klant klant)
        {
            return IsisContext.TijdPrestaties.Local.Where(p => p.Klant.Id == klant.Id).OrderByDescending(p => p.Id).FirstOrDefault();
        }

        public IsisContext IsisContext
        {
            get { return Context as IsisContext; }
        }
    }
}
