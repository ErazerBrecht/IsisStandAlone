namespace ISIS.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ISIS_DataEntities : DbContext
    {
        public ISIS_DataEntities(): base("name=ISIS")
        {
        }

        public virtual DbSet<Datum> Datum { get; set; }
        public virtual DbSet<Klant> Klanten { get; set; }
        public virtual DbSet<Prestatie> Prestaties { get; set; }
        public virtual DbSet<SoortKlant> SoortKlant { get; set; }
        public virtual DbSet<Strijker> Strijkers { get; set; }
        public virtual DbSet<StukPrestatie> StukPrestaties { get; set; }
        public virtual DbSet<TijdPrestatie> TijdPrestaties { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Klant>()
                .Property(e => e.Gebruikersnummer)
                .IsFixedLength();

            modelBuilder.Entity<Klant>()
                .Property(e => e.Naam)
                .IsUnicode(false);

            modelBuilder.Entity<Klant>()
                .Property(e => e.Voornaam)
                .IsUnicode(false);

            modelBuilder.Entity<Klant>()
                .Property(e => e.Straat)
                .IsUnicode(false);

            modelBuilder.Entity<Klant>()
                .Property(e => e.Gemeente)
                .IsUnicode(false);

            modelBuilder.Entity<Klant>()
                .Property(e => e.Telefoon)
                .IsUnicode(false);

            modelBuilder.Entity<Klant>()
                .Property(e => e.Gsm)
                .IsUnicode(false);

            modelBuilder.Entity<Klant>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Klant>()
                .Property(e => e.AndereNaam)
                .IsUnicode(false);

            modelBuilder.Entity<Klant>()
                .Property(e => e.Betalingswijze)
                .IsUnicode(false);

            modelBuilder.Entity<Klant>()
                .Property(e => e.SoortKlantType)
                .IsUnicode(false);

            modelBuilder.Entity<Klant>()
                .Property(e => e.SoortKlantPlaats)
                .IsUnicode(false);

            modelBuilder.Entity<Klant>()
                .Property(e => e.Bericht)
                .IsUnicode(false);

            modelBuilder.Entity<Klant>()
                .HasMany(e => e.Prestaties)
                .WithRequired(e => e.Klanten)
                .HasForeignKey(e => e.KlantenNummer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Prestatie>()
                .HasMany(e => e.Datum)
                .WithRequired(e => e.Prestaties)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Prestatie>()
                .HasOptional(e => e.StukPrestaties)
                .WithRequired(e => e.Prestaties);

            modelBuilder.Entity<Prestatie>()
                .HasOptional(e => e.TijdPrestaties)
                .WithRequired(e => e.Prestaties);

            modelBuilder.Entity<SoortKlant>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<SoortKlant>()
                .Property(e => e.Naam)
                .IsUnicode(false);

            modelBuilder.Entity<SoortKlant>()
                .Property(e => e.SnelheidsCoëfficiënt)
                .HasPrecision(3, 2);

            modelBuilder.Entity<SoortKlant>()
                .Property(e => e.Euro)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SoortKlant>()
                .HasMany(e => e.Klanten)
                .WithRequired(e => e.SoortKlant)
                .HasForeignKey(e => new { e.SoortKlantType, e.SoortKlantPlaats })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Strijker>()
                .Property(e => e.Naam)
                .IsUnicode(false);

            modelBuilder.Entity<Strijker>()
                .Property(e => e.Straat)
                .IsUnicode(false);

            modelBuilder.Entity<Strijker>()
                .Property(e => e.Gemeente)
                .IsUnicode(false);

            modelBuilder.Entity<Strijker>()
                .Property(e => e.RNSZ)
                .IsUnicode(false);

            modelBuilder.Entity<Strijker>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Strijker>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<Strijker>()
                .Property(e => e.Bankrekening)
                .IsFixedLength();

            modelBuilder.Entity<Strijker>()
                .Property(e => e.Voornaam)
                .IsUnicode(false);

            modelBuilder.Entity<Strijker>()
                .HasMany(e => e.Datum)
                .WithRequired(e => e.Strijker1)
                .HasForeignKey(e => e.Strijker1_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Strijker>()
                .HasMany(e => e.Datum1)
                .WithOptional(e => e.Strijker2)
                .HasForeignKey(e => e.Strijker2_ID);

            modelBuilder.Entity<Strijker>()
                .HasMany(e => e.Datum2)
                .WithOptional(e => e.Strijker3)
                .HasForeignKey(e => e.Strijker3_ID);

            modelBuilder.Entity<Strijker>()
                .HasMany(e => e.Datum3)
                .WithOptional(e => e.Strijker4)
                .HasForeignKey(e => e.Strijker4_ID);

            modelBuilder.Entity<Strijker>()
                .HasMany(e => e.Datum4)
                .WithOptional(e => e.Strijker5)
                .HasForeignKey(e => e.Strijker5_ID);

            modelBuilder.Entity<TijdPrestatie>()
                .Property(e => e.ParameterHemden)
                .HasPrecision(3, 1);

            modelBuilder.Entity<TijdPrestatie>()
                .Property(e => e.ParameterLakens1)
                .HasPrecision(3, 1);

            modelBuilder.Entity<TijdPrestatie>()
                .Property(e => e.ParameterLakens2)
                .HasPrecision(3, 1);

            modelBuilder.Entity<TijdPrestatie>()
                .Property(e => e.ParameterAndereStrijk)
                .HasPrecision(3, 2);
        }
    }
}
