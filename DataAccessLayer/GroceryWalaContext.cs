using GroceryWala.DomainLayer.Entities;
using GroceryWala.DomainLayer.Models.Single;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GroceryWala.DataAccessLayer
{
    public class GroceryWalaContext : IdentityDbContext<ApplicationUser>
    {
        public GroceryWalaContext(DbContextOptions<GroceryWalaContext> options) : base(options)
        {
        }

        public virtual DbSet<ProductEntity> Products { get; set; }
        public virtual DbSet<ImageEntity> Images { get; set; }
        public virtual DbSet<CartItemEntity> CartItems { get; set; }
        public virtual DbSet<CommentEntity> Comments { get; set; }
        public virtual DbSet<RatingEntity> Ratings{ get; set; }
        public virtual DbSet<OfferEntity> Offers{ get; set; }
        public virtual DbSet<OrderEntity> Orders{ get; set; }
        public virtual DbSet<UserOrderEntity> UserOrders{ get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            this.SeedUsers(builder);
            this.SeedRoles(builder);
            this.SeedUserRoles(builder);
        }

        private void SeedUsers(ModelBuilder builder)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Id = "b74ddd14-6340-4840-95c2-db12554843e5",
                FirstName = "Admin",
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                NormalizedUserName = "Admin",
                NormalizedEmail = "admin@gmail.com",
                PhoneNumber = "1234567890"
            };

            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "12345");

            builder.Entity<ApplicationUser>().HasData(user);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "fab4fac1-c546-41de-aebc-a14da6895711", Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole() { Id = "c7b013f0-5201-4317-abd8-c211f91b7330", Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" }
                );
        }

        private void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "fab4fac1-c546-41de-aebc-a14da6895711", UserId = "b74ddd14-6340-4840-95c2-db12554843e5" }
                );
        }


    }
}
