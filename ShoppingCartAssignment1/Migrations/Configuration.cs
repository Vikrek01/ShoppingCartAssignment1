namespace ShoppingCartAssignment1.Migrations
{
    using ShoppingCartAssignment1.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<ShoppingCartAssignment1.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ShoppingCartAssignment1.Models.ApplicationDbContext context)
        {
            context.Users.Add(new User { UserName = "Admin", UserPassword = encryptPassword("admin"), Role = "Admin" });
            context.Users.Add(new User { UserName = "User", UserPassword = encryptPassword("user"), Role = "User" });

           

            context.SaveChanges();
            base.Seed(context);
        }
        private string encryptPassword(string pass)
        {
            string password = "";
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(pass));
                password = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
            return password;
        }
    }
}
