namespace EF_Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StrijkerTelefoon_ChangedToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Strijkers", "Tel", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Strijkers", "Tel", c => c.Int());
        }
    }
}
