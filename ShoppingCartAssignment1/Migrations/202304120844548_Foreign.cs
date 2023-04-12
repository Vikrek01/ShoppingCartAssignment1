namespace ShoppingCartAssignment1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Foreign : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        datetime = c.DateTime(nullable: false),
                        Total = c.Int(nullable: false),
                        PId = c.Guid(nullable: false),
                        ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Products", t => t.PId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.ID, cascadeDelete: true)
                .Index(t => t.PId)
                .Index(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ID", "dbo.Users");
            DropForeignKey("dbo.Orders", "PId", "dbo.Products");
            DropIndex("dbo.Orders", new[] { "ID" });
            DropIndex("dbo.Orders", new[] { "PId" });
            DropTable("dbo.Orders");
        }
    }
}
