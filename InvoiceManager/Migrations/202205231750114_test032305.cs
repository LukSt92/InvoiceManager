namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test032305 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InvoicePositions", "UnitofMeasure_Id", "dbo.UnitofMeasures");
            DropIndex("dbo.InvoicePositions", new[] { "UnitofMeasure_Id" });
            RenameColumn(table: "dbo.InvoicePositions", name: "UnitofMeasure_Id", newName: "UnitOfMeasureId");
            AlterColumn("dbo.InvoicePositions", "UnitOfMeasureId", c => c.Int(nullable: false));
            CreateIndex("dbo.InvoicePositions", "UnitOfMeasureId");
            AddForeignKey("dbo.InvoicePositions", "UnitOfMeasureId", "dbo.UnitofMeasures", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoicePositions", "UnitOfMeasureId", "dbo.UnitofMeasures");
            DropIndex("dbo.InvoicePositions", new[] { "UnitOfMeasureId" });
            AlterColumn("dbo.InvoicePositions", "UnitOfMeasureId", c => c.Int());
            RenameColumn(table: "dbo.InvoicePositions", name: "UnitOfMeasureId", newName: "UnitofMeasure_Id");
            CreateIndex("dbo.InvoicePositions", "UnitofMeasure_Id");
            AddForeignKey("dbo.InvoicePositions", "UnitofMeasure_Id", "dbo.UnitofMeasures", "Id");
        }
    }
}
