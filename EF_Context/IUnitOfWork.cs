using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF_Context.Repositories;

namespace EF_Context
{
    public interface IUnitOfWork : IDisposable
    {
        IDatumRepository Datums { get; }
        IKlantRepository Klanten { get;  }
        IKlantTypeRepository KlantTypes { get; }
        IStrijkerRepository Strijkers { get; }
        IStukPrestatieRepository StukPrestaties { get; }
        ITijdPrestatieRepository TijdPrestaties { get; }

        int Complete();
    }
}
