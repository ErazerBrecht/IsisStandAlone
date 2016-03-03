using EF_Context;
using EF_Context.Repositories;
using DAL_Repository.Repositories;
using System.Linq;
using System.Data.Entity;

namespace DAL_Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IsisContext _context;

        public UnitOfWork(IsisContext context)
        {
            _context = context;
            Datums = new DatumRepository(context);
            Klanten = new KlantRepository(context);
            KlantTypes = new KlantTypeRepository(context);
            Strijkers = new StrijkerRepository(context);
            Prestaties = new PrestatieRepository(context);
            StukPrestaties = new StukPrestatieRepository(context);
            TijdPrestaties = new TijdPrestatieRepository(context);
        }

        public IDatumRepository Datums { get; private set; }
        public IKlantRepository Klanten { get; private set; }
        public IKlantTypeRepository KlantTypes { get; private set; }
        public IStrijkerRepository Strijkers { get; private set; }
        public IPrestatieRepository Prestaties { get; private set; }
        public IStukPrestatieRepository StukPrestaties { get; private set; }
        public ITijdPrestatieRepository TijdPrestaties { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public void DiscardChanges()
        {
            var changedEntries = _context.ChangeTracker.Entries();

            //Reset changes made to those entries
            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Modified))
            {
                //entry.CurrentValues.SetValues(entry.OriginalValues);
                entry.State = EntityState.Unchanged;
            }

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Added))
            {
                entry.State = EntityState.Detached;
            }

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Deleted))
            {
                entry.State = EntityState.Unchanged;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
