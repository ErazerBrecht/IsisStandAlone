namespace EF_Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedFKkLAN : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Klanten", new[] { "KlantType_Type", "KlantType_Naam" }, "dbo.KlantType");
            DropIndex("dbo.Klanten", new[] { "KlantType_Type", "KlantType_Naam" });
            RenameColumn(table: "dbo.Klanten", name: "KlantType_Type", newName: "TypeNaam");
            RenameColumn(table: "dbo.Klanten", name: "KlantType_Naam", newName: "TypePlaats");
            AlterColumn("dbo.Klanten", "TypeNaam", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Klanten", "TypePlaats", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Klanten", new[] { "TypeNaam", "TypePlaats" });
            AddForeignKey("dbo.Klanten", new[] { "TypeNaam", "TypePlaats" }, "dbo.KlantType", new[] { "Type", "Naam" }, cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Klanten", new[] { "TypeNaam", "TypePlaats" }, "dbo.KlantType");
            DropIndex("dbo.Klanten", new[] { "TypeNaam", "TypePlaats" });
            AlterColumn("dbo.Klanten", "TypePlaats", c => c.String(maxLength: 50));
            AlterColumn("dbo.Klanten", "TypeNaam", c => c.String(maxLength: 10));
            RenameColumn(table: "dbo.Klanten", name: "TypePlaats", newName: "KlantType_Naam");
            RenameColumn(table: "dbo.Klanten", name: "TypeNaam", newName: "KlantType_Type");
            CreateIndex("dbo.Klanten", new[] { "KlantType_Type", "KlantType_Naam" });
            AddForeignKey("dbo.Klanten", new[] { "KlantType_Type", "KlantType_Naam" }, "dbo.KlantType", new[] { "Type", "Naam" });
        }
    }
}
