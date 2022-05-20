namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testnr21905 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "NetPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "VatRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "VatAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "GrossPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Products", "Value");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Value", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Products", "GrossPrice");
            DropColumn("dbo.Products", "VatAmount");
            DropColumn("dbo.Products", "VatRate");
            DropColumn("dbo.Products", "NetPrice");
        }
    }
}
