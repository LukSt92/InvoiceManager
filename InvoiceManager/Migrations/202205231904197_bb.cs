namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvoicePositions", "NetValue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.InvoicePositions", "VatValue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvoicePositions", "VatValue");
            DropColumn("dbo.InvoicePositions", "NetValue");
        }
    }
}
