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
    public class KlantTypeRepository : Repository<KlantType>, IKlantTypeRepository
    {
        public KlantTypeRepository(IsisContext context) : base(context)
        {
            context.KlantenTypes.Load();
        }

        public bool Exists(string type, string naam)
        {
            return IsisContext.KlantenTypes.Local.Any(k => k.Naam == naam && k.Type == type);
        }

        public decimal GetSnelheidsCoëfficiënt(string type, string naam)
        {
            return IsisContext.KlantenTypes.Local.First(k => k.Naam == naam && k.Type == type).SnelheidsCoëfficiënt;
        }

        public IsisContext IsisContext
        {
            get { return Context as IsisContext; }
        }
    }
}
