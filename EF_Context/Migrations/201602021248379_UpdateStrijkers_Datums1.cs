namespace EF_Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStrijkers_Datums1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Datum", "Strijker_Id", "dbo.Strijkers");
            DropForeignKey("dbo.Datum", "Strijker1_Id", "dbo.Strijkers");
            DropForeignKey("dbo.Datum", "Strijker2_Id", "dbo.Strijkers");
            DropForeignKey("dbo.Datum", "Strijker3_Id", "dbo.Strijkers");
            DropForeignKey("dbo.Datum", "Strijker4_Id", "dbo.Strijkers");
            DropForeignKey("dbo.Datum", "Strijker5_Id", "dbo.Strijkers");
            DropIndex("dbo.Datum", new[] { "Strijker_Id" });
            DropIndex("dbo.Datum", new[] { "Strijker1_Id" });
            DropIndex("dbo.Datum", new[] { "Strijker2_Id" });
            DropIndex("dbo.Datum", new[] { "Strijker3_Id" });
            DropIndex("dbo.Datum", new[] { "Strijker4_Id" });
            DropIndex("dbo.Datum", new[] { "Strijker5_Id" });
            CreateTable(
                "dbo.StrijkerDatums",
                c => new
                    {
                        Strijker_Id = c.Int(nullable: false),
                        Datum_Date = c.DateTime(nullable: false),
                        Datum_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Strijker_Id, t.Datum_Date, t.Datum_Id })
                .ForeignKey("dbo.Strijkers", t => t.Strijker_Id, cascadeDelete: true)
                .ForeignKey("dbo.Datum", t => new { t.Datum_Date, t.Datum_Id }, cascadeDelete: true)
                .Index(t => t.Strijker_Id)
                .Index(t => new { t.Datum_Date, t.Datum_Id });
            
            DropColumn("dbo.Datum", "Strijker_Id");
            DropColumn("dbo.Datum", "Strijker1_Id");
            DropColumn("dbo.Datum", "Strijker2_Id");
            DropColumn("dbo.Datum", "Strijker3_Id");
            DropColumn("dbo.Datum", "Strijker4_Id");
            DropColumn("dbo.Datum", "Strijker5_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Datum", "Strijker5_Id", c => c.Int());
            AddColumn("dbo.Datum", "Strijker4_Id", c => c.Int());
            AddColumn("dbo.Datum", "Strijker3_Id", c => c.Int());
            AddColumn("dbo.Datum", "Strijker2_Id", c => c.Int());
            AddColumn("dbo.Datum", "Strijker1_Id", c => c.Int());
            AddColumn("dbo.Datum", "Strijker_Id", c => c.Int());
            DropForeignKey("dbo.StrijkerDatums", new[] { "Datum_Date", "Datum_Id" }, "dbo.Datum");
            DropForeignKey("dbo.StrijkerDatums", "Strijker_Id", "dbo.Strijkers");
            DropIndex("dbo.StrijkerDatums", new[] { "Datum_Date", "Datum_Id" });
            DropIndex("dbo.StrijkerDatums", new[] { "Strijker_Id" });
            DropTable("dbo.StrijkerDatums");
            CreateIndex("dbo.Datum", "Strijker5_Id");
            CreateIndex("dbo.Datum", "Strijker4_Id");
            CreateIndex("dbo.Datum", "Strijker3_Id");
            CreateIndex("dbo.Datum", "Strijker2_Id");
            CreateIndex("dbo.Datum", "Strijker1_Id");
            CreateIndex("dbo.Datum", "Strijker_Id");
            AddForeignKey("dbo.Datum", "Strijker5_Id", "dbo.Strijkers", "Id");
            AddForeignKey("dbo.Datum", "Strijker4_Id", "dbo.Strijkers", "Id");
            AddForeignKey("dbo.Datum", "Strijker3_Id", "dbo.Strijkers", "Id");
            AddForeignKey("dbo.Datum", "Strijker2_Id", "dbo.Strijkers", "Id");
            AddForeignKey("dbo.Datum", "Strijker1_Id", "dbo.Strijkers", "Id");
            AddForeignKey("dbo.Datum", "Strijker_Id", "dbo.Strijkers", "Id");
        }
    }
}
