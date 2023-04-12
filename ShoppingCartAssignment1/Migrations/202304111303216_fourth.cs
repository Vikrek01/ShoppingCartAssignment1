namespace ShoppingCartAssignment1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fourth : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRolesMappings", "RoleMaster_ID", "dbo.RoleMasters");
            DropForeignKey("dbo.UserRolesMappings", "UserID", "dbo.Users");
            DropIndex("dbo.UserRolesMappings", new[] { "UserID" });
            DropIndex("dbo.UserRolesMappings", new[] { "RoleMaster_ID" });
            AddColumn("dbo.Users", "Role", c => c.String());
            DropTable("dbo.RoleMasters");
            DropTable("dbo.UserRolesMappings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserRolesMappings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        RoleID = c.Int(nullable: false),
                        RoleMaster_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RoleMasters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RollName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.Users", "Role");
            CreateIndex("dbo.UserRolesMappings", "RoleMaster_ID");
            CreateIndex("dbo.UserRolesMappings", "UserID");
            AddForeignKey("dbo.UserRolesMappings", "UserID", "dbo.Users", "ID", cascadeDelete: true);
            AddForeignKey("dbo.UserRolesMappings", "RoleMaster_ID", "dbo.RoleMasters", "ID");
        }
    }
}
