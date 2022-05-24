namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tfg : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.InvoicePositions", "Lp");
            DropColumn("dbo.InvoicePositions", "Value");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InvoicePositions", "Value", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.InvoicePositions", "Lp", c => c.Int(nullable: false));
        }
    }
}
