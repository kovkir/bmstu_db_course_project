using System;
using Microsoft.EntityFrameworkCore;

namespace db_cp.Models
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Club> Club { get; set; }
        public DbSet<Coach> Coach { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<Squad> Squad { get; set; }

        public DbSet<SquadPlayer> SquadPlayer { get; set; }
    }
}
