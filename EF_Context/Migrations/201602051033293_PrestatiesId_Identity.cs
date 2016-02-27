namespace EF_Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrestatiesId_Identity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Datums", "Id", "dbo.Prestaties");
            //http://stackoverflow.com/a/34549700/2961887
            Sql(@"ALTER TABLE [dbo].[Datums] DROP CONSTRAINT [FK_dbo.Datum_dbo.Prestaties_Id]");
            DropPrimaryKey("dbo.Prestaties");
            AlterColumn("dbo.Prestaties", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Prestaties", "Id");
            AddForeignKey("dbo.Datums", "Id", "dbo.Prestaties", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Datums", "Id", "dbo.Prestaties");
            DropPrimaryKey("dbo.Prestaties");
            AlterColumn("dbo.Prestaties", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Prestaties", "Id");
            AddForeignKey("dbo.Datums", "Id", "dbo.Prestaties", "Id", cascadeDelete: true);
        }
    }
}
