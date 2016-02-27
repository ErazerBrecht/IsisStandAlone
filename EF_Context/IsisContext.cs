using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF_Model;


namespace EF_Context
{
    public class IsisContext : DbContext
    {
        public virtual DbSet<Datum> Datums { get; set; }
        public virtual DbSet<Klant> Klanten { get; set; }
        public virtual DbSet<Prestatie> Prestaties { get; set; }
        public virtual DbSet<KlantType> KlantenTypes { get; set; }
        public virtual DbSet<Strijker> Strijkers { get; set; }
        public virtual DbSet<StukPrestatie> StukPrestaties { get; set; }
        public virtual DbSet<TijdPrestatie> TijdPrestaties { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<KlantType>().Property(k => k.SnelheidsCoëfficiënt).HasPrecision(3, 2);
        }
    }
}
