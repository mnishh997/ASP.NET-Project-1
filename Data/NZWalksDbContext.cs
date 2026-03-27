using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;
using System.Data.Common;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext: DbContext
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for difficulties 
            // Easy, Medium, Hard

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("2e52a791-045d-49fd-afe4-25b4a1173cef"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("028be24b-6233-4fd5-bb4f-a289042ae4e1"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("06731ad4-d501-4e82-8050-a7c6df15645d"),
                    Name = "Hard"
                }
            };

            // Seed difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

        }
    }
}
