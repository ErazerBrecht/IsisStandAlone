using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF_Model;

namespace EF_Context.Repositories
{
    public interface IKlantTypeRepository : IRepository<KlantType>
    {
        bool Exists(string type, string naam);
        decimal GetSnelheidsCoëfficiënt(string type, string naam);
    }
}
