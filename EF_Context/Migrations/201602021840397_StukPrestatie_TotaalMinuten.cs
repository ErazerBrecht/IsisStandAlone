namespace EF_Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StukPrestatie_TotaalMinuten : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prestaties", "TotaalMinuten", c => c.Int());
            DropColumn("dbo.Prestaties", "Totaal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Prestaties", "Totaal", c => c.Int());
            DropColumn("dbo.Prestaties", "TotaalMinuten");
        }
    }
}
