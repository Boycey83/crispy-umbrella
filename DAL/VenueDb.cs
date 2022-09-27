using AB.BeerQuest.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AB.BeerQuest.DAL
{
    public class VenueDb : DbContext
    {
        public VenueDb(DbContextOptions<VenueDb> options)
            : base(options)
        {
        }

        public DbSet<Venue> Venues => Set<Venue>();
        public DbSet<VenueTag> VenueTags => Set<VenueTag>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Venue>(entity =>
            {
                entity
                    .HasKey(v => v.Id);
                entity
                    .HasMany(v => v.Tags)
                    .WithOne(t => t.Venue)
                    .HasForeignKey(t => t.VenueId);
                entity
                    .Navigation(v => v.Tags)
                    .AutoInclude();
            });

            mb.Entity<VenueTag>(entity =>
            {
                entity.HasKey(v => v.Id);
                entity
                    .HasOne(t => t.Venue)
                    .WithMany(v => v.Tags);
            });
        }
    }
}
