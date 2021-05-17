using Microsoft.EntityFrameworkCore;

namespace GoodFood.Model {
  class RatingContext : DbContext {
    public RatingContext(DbContextOptions<RatingContext> options) : base(options) {
      Database.EnsureCreated();
    }
    public DbSet<Rating> Rating { get; set; }
  }
}
