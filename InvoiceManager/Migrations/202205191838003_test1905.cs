namespace InvoiceManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1905 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "Nip", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Addresses", "Nip");
        }
    }
}
