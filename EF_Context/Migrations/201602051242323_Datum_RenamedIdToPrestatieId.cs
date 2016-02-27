namespace EF_Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Datum_RenamedIdToPrestatieId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.StrijkerDatums", name: "Datum_Id", newName: "Datum_PrestatieId");
            RenameColumn(table: "dbo.Datums", name: "Id", newName: "PrestatieId");
            RenameIndex(table: "dbo.Datums", name: "IX_Id", newName: "IX_PrestatieId");
            RenameIndex(table: "dbo.StrijkerDatums", name: "IX_Datum_Date_Datum_Id", newName: "IX_Datum_Date_Datum_PrestatieId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.StrijkerDatums", name: "IX_Datum_Date_Datum_PrestatieId", newName: "IX_Datum_Date_Datum_Id");
            RenameIndex(table: "dbo.Datums", name: "IX_PrestatieId", newName: "IX_Id");
            RenameColumn(table: "dbo.Datums", name: "PrestatieId", newName: "Id");
            RenameColumn(table: "dbo.StrijkerDatums", name: "Datum_PrestatieId", newName: "Datum_Id");
        }
    }
}
