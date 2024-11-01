using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AAAAAAAAAAA.Models
{
    public class AppDataContext:IdentityDbContext<AppUser>
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> ShoppingCart {  get; set; }
        public DbSet<UserReviewRatings> ProductUserReview {  get; set; }
    }
}
