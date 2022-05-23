namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tralralalsdlasld : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InvoicePositions", "UnitOfMeasureId", "dbo.UnitofMeasures");
            DropIndex("dbo.InvoicePositions", new[] { "UnitOfMeasureId" });
            DropColumn("dbo.InvoicePositions", "UnitOfMeasureId");
            DropTable("dbo.UnitofMeasures");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UnitofMeasures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.InvoicePositions", "UnitOfMeasureId", c => c.Int(nullable: false));
            CreateIndex("dbo.InvoicePositions", "UnitOfMeasureId");
            AddForeignKey("dbo.InvoicePositions", "UnitOfMeasureId", "dbo.UnitofMeasures", "Id", cascadeDelete: true);
        }
    }
}
