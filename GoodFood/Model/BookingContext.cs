using Microsoft.EntityFrameworkCore;

namespace GoodFood.Model {
  class BookingContext : DbContext {
    public BookingContext(DbContextOptions<BookingContext> options) : base(options) {
      Database.EnsureCreated();
    }
    public DbSet<Booking> Booking { get; set; }
  }
}
