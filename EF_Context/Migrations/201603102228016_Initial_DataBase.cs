namespace EF_Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_DataBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Datums",
                c => new
                    {
                        Date = c.DateTime(nullable: false),
                        PrestatieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Date, t.PrestatieId })
                .ForeignKey("dbo.Prestaties", t => t.PrestatieId, cascadeDelete: true)
                .Index(t => t.PrestatieId);
            
            CreateTable(
                "dbo.Prestaties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tegoed = c.Byte(nullable: false),
                        TotaalMinuten = c.Int(),
                        AantalHemden = c.Byte(),
                        ParameterHemden = c.Decimal(precision: 18, scale: 2),
                        AantalLakens1 = c.Byte(),
                        ParameterLakens1 = c.Decimal(precision: 18, scale: 2),
                        AantalLakens2 = c.Byte(),
                        ParameterLakens2 = c.Decimal(precision: 18, scale: 2),
                        AantalAndereStrijk = c.Byte(),
                        TijdAndereStrijk = c.Byte(),
                        ParameterAndereStrijk = c.Decimal(precision: 18, scale: 2),
                        TijdAdministratie = c.Byte(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Klant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Klanten", t => t.Klant_Id)
                .Index(t => t.Klant_Id);
            
            CreateTable(
                "dbo.Klanten",
                c => new
                    {
                        TypeNaam = c.String(nullable: false, maxLength: 10),
                        TypePlaats = c.String(nullable: false, maxLength: 50),
                        Id = c.Int(nullable: false),
                        Gebruikersnummer = c.String(maxLength: 13),
                        Naam = c.String(nullable: false, maxLength: 50),
                        Voornaam = c.String(maxLength: 50),
                        Straat = c.String(nullable: false, maxLength: 50),
                        Nummer = c.Int(nullable: false),
                        Bus = c.String(maxLength: 5),
                        Postcode = c.Int(nullable: false),
                        Gemeente = c.String(nullable: false, maxLength: 50),
                        Telefoon = c.String(maxLength: 15),
                        Gsm = c.String(maxLength: 15),
                        Email = c.String(maxLength: 50),
                        AndereNaam = c.String(maxLength: 50),
                        Betalingswijze = c.String(nullable: false, maxLength: 12),
                        Actief = c.Byte(nullable: false),
                        Strijkbox = c.Byte(),
                        Waarborg = c.Int(),
                        Bericht = c.String(maxLength: 4),
                        Datum = c.DateTime(storeType: "date"),
                        LaatsteActiviteit = c.DateTime(storeType: "date"),
                        Tegoed = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.KlantType", t => new { t.TypeNaam, t.TypePlaats }, cascadeDelete: true)
                .Index(t => new { t.TypeNaam, t.TypePlaats });
            
            CreateTable(
                "dbo.KlantType",
                c => new
                    {
                        Type = c.String(nullable: false, maxLength: 10),
                        Naam = c.String(nullable: false, maxLength: 50),
                        SnelheidsCoëfficiënt = c.Decimal(nullable: false, precision: 3, scale: 2),
                        Euro = c.Decimal(nullable: false, storeType: "money"),
                        StukTarief = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.Type, t.Naam });
            
            CreateTable(
                "dbo.Strijkers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Naam = c.String(nullable: false, maxLength: 50),
                        Voornaam = c.String(nullable: false, maxLength: 50),
                        Straat = c.String(nullable: false, maxLength: 50),
                        Nummer = c.Int(nullable: false),
                        Postcode = c.Int(nullable: false),
                        Gemeente = c.String(nullable: false, maxLength: 50),
                        Tel = c.String(maxLength: 20),
                        RNSZ = c.String(nullable: false, maxLength: 20),
                        Email = c.String(maxLength: 50),
                        Login = c.String(nullable: false, maxLength: 6),
                        Bankrekening = c.String(maxLength: 19),
                        IndienstVanaf = c.DateTime(storeType: "date"),
                        IndienstTot = c.DateTime(storeType: "date"),
                        UrenTewerkstelling = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StrijkerDatums",
                c => new
                    {
                        Strijker_Id = c.Int(nullable: false),
                        Datum_Date = c.DateTime(nullable: false),
                        Datum_PrestatieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Strijker_Id, t.Datum_Date, t.Datum_PrestatieId })
                .ForeignKey("dbo.Strijkers", t => t.Strijker_Id, cascadeDelete: true)
                .ForeignKey("dbo.Datums", t => new { t.Datum_Date, t.Datum_PrestatieId }, cascadeDelete: true)
                .Index(t => t.Strijker_Id)
                .Index(t => new { t.Datum_Date, t.Datum_PrestatieId });
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StrijkerDatums", new[] { "Datum_Date", "Datum_PrestatieId" }, "dbo.Datums");
            DropForeignKey("dbo.StrijkerDatums", "Strijker_Id", "dbo.Strijkers");
            DropForeignKey("dbo.Datums", "PrestatieId", "dbo.Prestaties");
            DropForeignKey("dbo.Prestaties", "Klant_Id", "dbo.Klanten");
            DropForeignKey("dbo.Klanten", new[] { "TypeNaam", "TypePlaats" }, "dbo.KlantType");
            DropIndex("dbo.StrijkerDatums", new[] { "Datum_Date", "Datum_PrestatieId" });
            DropIndex("dbo.StrijkerDatums", new[] { "Strijker_Id" });
            DropIndex("dbo.Klanten", new[] { "TypeNaam", "TypePlaats" });
            DropIndex("dbo.Prestaties", new[] { "Klant_Id" });
            DropIndex("dbo.Datums", new[] { "PrestatieId" });
            DropTable("dbo.StrijkerDatums");
            DropTable("dbo.Strijkers");
            DropTable("dbo.KlantType");
            DropTable("dbo.Klanten");
            DropTable("dbo.Prestaties");
            DropTable("dbo.Datums");
        }
    }
}
