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
    class KlantRepository : Repository<Klant>, IKlantRepository
    {
        public KlantRepository(IsisContext context) : base(context)
        {
            context.Klanten.Load();
        }

        public bool Exists(int id)
        {
            return IsisContext.Klanten.Local.Any(k => k.Id == id);
        }

        public IEnumerable<Klant> GetEnabledClients()
        {
            return IsisContext.Klanten.Local.Where(k => k.Actief != 0);
        }

        public IsisContext IsisContext
        {
            get { return Context as IsisContext; }
        }
    }
}
