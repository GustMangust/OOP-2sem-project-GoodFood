using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodFood.Model
{
    class BookingContext:DbContext
    {
        public BookingContext(DbContextOptions<BookingContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Booking> Booking { get; set; }
    }
}
