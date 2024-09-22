using Microsoft.EntityFrameworkCore;
using Shop.Models.Configuration;
using System.Reflection.Emit;

namespace Shop.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        {
            
        }

        public DbSet<Hotels> Hotels { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<HotelCatigories> HotelCatigories { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Comments> Comments { get; set; }


        protected override void OnModelCreating(ModelBuilder model)
        {
            model.ApplyConfiguration(new UserConfiguration());
            model.ApplyConfiguration(new HotelConfiguration());
            model.ApplyConfiguration(new CatigoriesConfiguration());

            base.OnModelCreating(model);
        }
    }
}
