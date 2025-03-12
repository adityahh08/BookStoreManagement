
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using DigitalBookStoreManagement.Models;
using Microsoft.EntityFrameworkCore;







namespace DigitalBookStoreManagement.Data
{

    public class ApplicationDbContextClass : DbContext
    {
        //public ApplicationDbContextClass() { }  
        public ApplicationDbContextClass(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();


        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //base.OnConfiguring(optionsBuilder);

        //    optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BookStoreDB;Integrated Security=True;");
        //}

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<User> Users { get; set; }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>().HasOne<Order>()
                                            .WithMany(o => o.OrderItems)
                                            .HasForeignKey(oi => oi.OrderID)
                                             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItem>().HasOne<Cart>()
                                            .WithMany(o => o.CartItems)
                                            .HasForeignKey(oi => oi.CartID)
                                            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}


