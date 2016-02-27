namespace EF_Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Klant_NummerOptional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Klanten", "Nummer", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Klanten", "Nummer", c => c.Int(nullable: false));
        }
    }
}
