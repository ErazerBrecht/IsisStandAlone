using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF_Model;

namespace EF_Context.Repositories
{
    public interface IKlantRepository : IRepository<Klant>
    {
        bool Exists(int id);
        IEnumerable<Klant> GetEnabledClients();
    }
}
