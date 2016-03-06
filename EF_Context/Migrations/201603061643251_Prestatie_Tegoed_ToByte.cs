namespace EF_Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Prestatie_Tegoed_ToByte : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Prestaties", "Tegoed", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Prestaties", "Tegoed", c => c.Int(nullable: false));
        }
    }
}
