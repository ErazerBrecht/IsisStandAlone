namespace EF_Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Klant_Bus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Klanten", "Bus", c => c.String(maxLength: 5));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Klanten", "Bus");
        }
    }
}
