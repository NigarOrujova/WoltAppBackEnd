using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WoltDataAccess.Configurations;
using WoltEntity.Entities;

namespace WoltDataAccess.DAL
{
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<RestaurantCategory> RestaurantCategories { get; set; }
        public DbSet<RestaurantProduct> RestaurantProducts { get; set; }
        public DbSet<StoreCategory> StoreCategories { get; set; }
        public DbSet<StoreProduct> StoreProducts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new RestaurantConfiguration());
            modelBuilder.ApplyConfiguration(new StoreConfiguration());
            modelBuilder.ApplyConfiguration(new RestaurantCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new RestaurantProductConfiguration());
            modelBuilder.ApplyConfiguration(new StoreCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new StoreProductConfiguration());
            modelBuilder.ApplyConfiguration(new SliderConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
