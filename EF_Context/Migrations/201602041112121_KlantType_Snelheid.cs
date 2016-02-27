namespace EF_Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KlantType_Snelheid : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.KlantType", "SnelheidsCoëfficiënt", c => c.Decimal(nullable: false, precision: 3, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.KlantType", "SnelheidsCoëfficiënt", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
