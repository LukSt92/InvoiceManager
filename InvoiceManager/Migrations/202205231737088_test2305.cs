namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2305 : DbMigration
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
            
            AddColumn("dbo.InvoicePositions", "UnitofMeasure_Id", c => c.Int());
            CreateIndex("dbo.InvoicePositions", "UnitofMeasure_Id");
            AddForeignKey("dbo.InvoicePositions", "UnitofMeasure_Id", "dbo.UnitofMeasures", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoicePositions", "UnitofMeasure_Id", "dbo.UnitofMeasures");
            DropIndex("dbo.InvoicePositions", new[] { "UnitofMeasure_Id" });
            DropColumn("dbo.InvoicePositions", "UnitofMeasure_Id");
            DropTable("dbo.UnitofMeasures");
        }
    }
}
