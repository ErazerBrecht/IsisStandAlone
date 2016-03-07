namespace EF_Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Klant_Actief : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Klanten", "Actief", c => c.Byte(nullable: false));
            AlterColumn("dbo.Klanten", "Strijkbox", c => c.Byte());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Klanten", "Strijkbox", c => c.Int());
            AlterColumn("dbo.Klanten", "Actief", c => c.Int());
        }
    }
}
