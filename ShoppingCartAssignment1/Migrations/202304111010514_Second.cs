namespace ShoppingCartAssignment1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoleMasters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RollName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserRolesMappings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        RoleID = c.Int(nullable: false),
                        RoleMaster_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.RoleMasters", t => t.RoleMaster_ID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.RoleMaster_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        UserPassword = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRolesMappings", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserRolesMappings", "RoleMaster_ID", "dbo.RoleMasters");
            DropIndex("dbo.UserRolesMappings", new[] { "RoleMaster_ID" });
            DropIndex("dbo.UserRolesMappings", new[] { "UserID" });
            DropTable("dbo.Users");
            DropTable("dbo.UserRolesMappings");
            DropTable("dbo.RoleMasters");
        }
    }
}
