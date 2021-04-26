using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodFood.Model
{
    class RestaurantContext:DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }
        public DbSet<Restaurant> Restaurant { get; set; }
    }
}
