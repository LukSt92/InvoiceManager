namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fgfgfg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "NetValue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Invoices", "GrossValue", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Invoices", "Value");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoices", "Value", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Invoices", "GrossValue");
            DropColumn("dbo.Invoices", "NetValue");
        }
    }
}
