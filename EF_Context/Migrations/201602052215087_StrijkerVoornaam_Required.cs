namespace EF_Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StrijkerVoornaam_Required : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Strijkers", "Voornaam", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Strijkers", "Voornaam", c => c.String(maxLength: 50));
        }
    }
}
