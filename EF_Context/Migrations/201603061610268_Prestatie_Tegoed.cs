namespace EF_Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Prestatie_Tegoed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prestaties", "Tegoed", c => c.Int(nullable: false));
            DropColumn("dbo.Prestaties", "TotaalBetalen");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Prestaties", "TotaalBetalen", c => c.Int(nullable: false));
            DropColumn("dbo.Prestaties", "Tegoed");
        }
    }
}
