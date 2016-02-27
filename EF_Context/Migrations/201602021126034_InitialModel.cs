namespace EF_Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Datum",
                c => new
                    {
                        Date = c.DateTime(nullable: false),
                        Id = c.Int(nullable: false),
                        Strijker_Id = c.Int(),
                        Strijker_Id1 = c.Int(),
                        Strijker_Id2 = c.Int(),
                        Strijker_Id3 = c.Int(),
                        Strijker_Id4 = c.Int(),
                        Strijker1_Id = c.Int(),
                        Strijker2_Id = c.Int(),
                        Strijker3_Id = c.Int(),
                        Strijker4_Id = c.Int(),
                        Strijker5_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.Date, t.Id })
                .ForeignKey("dbo.Prestaties", t => t.Id, cascadeDelete: true)
                .ForeignKey("dbo.Strijkers", t => t.Strijker_Id)
                .ForeignKey("dbo.Strijkers", t => t.Strijker_Id1)
                .ForeignKey("dbo.Strijkers", t => t.Strijker_Id2)
                .ForeignKey("dbo.Strijkers", t => t.Strijker_Id3)
                .ForeignKey("dbo.Strijkers", t => t.Strijker_Id4)
                .ForeignKey("dbo.Strijkers", t => t.Strijker1_Id)
                .ForeignKey("dbo.Strijkers", t => t.Strijker2_Id)
                .ForeignKey("dbo.Strijkers", t => t.Strijker3_Id)
                .ForeignKey("dbo.Strijkers", t => t.Strijker4_Id)
                .ForeignKey("dbo.Strijkers", t => t.Strijker5_Id)
                .Index(t => t.Id)
                .Index(t => t.Strijker_Id)
                .Index(t => t.Strijker_Id1)
                .Index(t => t.Strijker_Id2)
                .Index(t => t.Strijker_Id3)
                .Index(t => t.Strijker_Id4)
                .Index(t => t.Strijker1_Id)
                .Index(t => t.Strijker2_Id)
                .Index(t => t.Strijker3_Id)
                .Index(t => t.Strijker4_Id)
                .Index(t => t.Strijker5_Id);
            
            CreateTable(
                "dbo.Prestaties",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        TotaalDienstenChecks = c.Byte(nullable: false),
                        Totaal = c.Int(),
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
                        Id = c.Int(nullable: false),
                        Gebruikersnummer = c.String(maxLength: 13),
                        Naam = c.String(nullable: false, maxLength: 50),
                        Voornaam = c.String(maxLength: 50),
                        Straat = c.String(nullable: false, maxLength: 50),
                        Nummer = c.Int(nullable: false),
                        Postcode = c.Int(nullable: false),
                        Gemeente = c.String(nullable: false, maxLength: 50),
                        Telefoon = c.String(maxLength: 15),
                        Gsm = c.String(maxLength: 15),
                        Email = c.String(maxLength: 50),
                        AndereNaam = c.String(maxLength: 50),
                        Betalingswijze = c.String(nullable: false, maxLength: 12),
                        Actief = c.Int(),
                        Strijkbox = c.Int(),
                        Waarborg = c.Int(),
                        Bericht = c.String(maxLength: 4),
                        Datum = c.DateTime(storeType: "date"),
                        LaatsteActiviteit = c.DateTime(storeType: "date"),
                        Tegoed = c.Byte(nullable: false),
                        SoortKlant_Type = c.String(maxLength: 10),
                        SoortKlant_Naam = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SoortKlant", t => new { t.SoortKlant_Type, t.SoortKlant_Naam })
                .Index(t => new { t.SoortKlant_Type, t.SoortKlant_Naam });
            
            CreateTable(
                "dbo.SoortKlant",
                c => new
                    {
                        Type = c.String(nullable: false, maxLength: 10),
                        Naam = c.String(nullable: false, maxLength: 50),
                        SnelheidsCoëfficiënt = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                        Voornaam = c.String(maxLength: 50),
                        Straat = c.String(nullable: false, maxLength: 50),
                        Nummer = c.Int(nullable: false),
                        Postcode = c.Int(nullable: false),
                        Gemeente = c.String(nullable: false, maxLength: 50),
                        Tel = c.Int(),
                        RNSZ = c.String(nullable: false, maxLength: 20),
                        Email = c.String(maxLength: 50),
                        Login = c.String(nullable: false, maxLength: 6),
                        Bankrekening = c.String(maxLength: 19),
                        IndienstVanaf = c.DateTime(storeType: "date"),
                        UrenTewerkstelling = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Datum", "Strijker5_Id", "dbo.Strijkers");
            DropForeignKey("dbo.Datum", "Strijker4_Id", "dbo.Strijkers");
            DropForeignKey("dbo.Datum", "Strijker3_Id", "dbo.Strijkers");
            DropForeignKey("dbo.Datum", "Strijker2_Id", "dbo.Strijkers");
            DropForeignKey("dbo.Datum", "Strijker1_Id", "dbo.Strijkers");
            DropForeignKey("dbo.Datum", "Strijker_Id4", "dbo.Strijkers");
            DropForeignKey("dbo.Datum", "Strijker_Id3", "dbo.Strijkers");
            DropForeignKey("dbo.Datum", "Strijker_Id2", "dbo.Strijkers");
            DropForeignKey("dbo.Datum", "Strijker_Id1", "dbo.Strijkers");
            DropForeignKey("dbo.Datum", "Strijker_Id", "dbo.Strijkers");
            DropForeignKey("dbo.Klanten", new[] { "SoortKlant_Type", "SoortKlant_Naam" }, "dbo.SoortKlant");
            DropForeignKey("dbo.Prestaties", "Klant_Id", "dbo.Klanten");
            DropForeignKey("dbo.Datum", "Id", "dbo.Prestaties");
            DropIndex("dbo.Klanten", new[] { "SoortKlant_Type", "SoortKlant_Naam" });
            DropIndex("dbo.Prestaties", new[] { "Klant_Id" });
            DropIndex("dbo.Datum", new[] { "Strijker5_Id" });
            DropIndex("dbo.Datum", new[] { "Strijker4_Id" });
            DropIndex("dbo.Datum", new[] { "Strijker3_Id" });
            DropIndex("dbo.Datum", new[] { "Strijker2_Id" });
            DropIndex("dbo.Datum", new[] { "Strijker1_Id" });
            DropIndex("dbo.Datum", new[] { "Strijker_Id4" });
            DropIndex("dbo.Datum", new[] { "Strijker_Id3" });
            DropIndex("dbo.Datum", new[] { "Strijker_Id2" });
            DropIndex("dbo.Datum", new[] { "Strijker_Id1" });
            DropIndex("dbo.Datum", new[] { "Strijker_Id" });
            DropIndex("dbo.Datum", new[] { "Id" });
            DropTable("dbo.Strijkers");
            DropTable("dbo.SoortKlant");
            DropTable("dbo.Klanten");
            DropTable("dbo.Prestaties");
            DropTable("dbo.Datum");
        }
    }
}
