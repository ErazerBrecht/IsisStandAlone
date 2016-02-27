namespace EF_Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameDatumAndSoortKlant : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Datum", newName: "Datums");
            RenameTable(name: "dbo.SoortKlant", newName: "KlantType");
            RenameColumn(table: "dbo.Klanten", name: "SoortKlant_Type", newName: "KlantType_Type");
            RenameColumn(table: "dbo.Klanten", name: "SoortKlant_Naam", newName: "KlantType_Naam");
            RenameIndex(table: "dbo.Klanten", name: "IX_SoortKlant_Type_SoortKlant_Naam", newName: "IX_KlantType_Type_KlantType_Naam");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Klanten", name: "IX_KlantType_Type_KlantType_Naam", newName: "IX_SoortKlant_Type_SoortKlant_Naam");
            RenameColumn(table: "dbo.Klanten", name: "KlantType_Naam", newName: "SoortKlant_Naam");
            RenameColumn(table: "dbo.Klanten", name: "KlantType_Type", newName: "SoortKlant_Type");
            RenameTable(name: "dbo.KlantType", newName: "SoortKlant");
            RenameTable(name: "dbo.Datums", newName: "Datum");
        }
    }
}
