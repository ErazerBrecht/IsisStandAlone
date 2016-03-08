namespace EF_Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Strijker_IndienstTot : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Strijkers", "IndienstTot", c => c.DateTime(storeType: "date"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Strijkers", "IndienstTot");
        }
    }
}
