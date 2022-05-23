namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ttt : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UnitofMeasures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.InvoicePositions", "UnitofMeasureId", c => c.Int(nullable: false));
            CreateIndex("dbo.InvoicePositions", "UnitofMeasureId");
            AddForeignKey("dbo.InvoicePositions", "UnitofMeasureId", "dbo.UnitofMeasures", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoicePositions", "UnitofMeasureId", "dbo.UnitofMeasures");
            DropIndex("dbo.InvoicePositions", new[] { "UnitofMeasureId" });
            DropColumn("dbo.InvoicePositions", "UnitofMeasureId");
            DropTable("dbo.UnitofMeasures");
        }
    }
}
