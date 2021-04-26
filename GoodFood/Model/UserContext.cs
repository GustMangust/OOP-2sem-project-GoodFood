using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodFood.Model
{
    class UserContext:DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<User> User { get; set; }
    }
}
