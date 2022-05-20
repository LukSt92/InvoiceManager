namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testtttttt2005 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "VatRate", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "VatRate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
