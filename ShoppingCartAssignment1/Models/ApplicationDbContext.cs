using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShoppingCartAssignment1.Models
{
    public class ApplicationDbContext : DbContext
    {
       public ApplicationDbContext() : base("name=MyConnectionString")
        {
        }

        public DbSet<Product> Products { get; set;}
        public DbSet<Cart> Carts { get; set;}
        public virtual DbSet<User> Users { get; set; }


    }
}