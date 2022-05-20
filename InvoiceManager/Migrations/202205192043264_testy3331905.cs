namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testy3331905 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "VatAmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "VatAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
