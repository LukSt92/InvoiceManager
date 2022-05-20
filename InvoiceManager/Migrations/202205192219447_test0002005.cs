namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test0002005 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "VatAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "VatAmount");
        }
    }
}
