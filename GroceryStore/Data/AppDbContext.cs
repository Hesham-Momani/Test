using GroceryStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GroceryStore.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Admin> admins{ get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Category> categories  { get; set; }
        public DbSet<Product> products  { get; set; }
        public DbSet<Cart> carts  { get; set; }
        public DbSet<Order> orders  { get; set; }
        public DbSet<Contact> contacts { get; set; }
        public DbSet<Featur> features { get; set; }
        
    }
}
