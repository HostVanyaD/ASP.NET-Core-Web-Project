namespace HighPaw.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using HighPaw.Data.Models;

    public class HighPawDbContext : IdentityDbContext<User>
    {
        public HighPawDbContext(DbContextOptions<HighPawDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pet> Pets { get; set; }

        public DbSet<Shelter> Shelters { get; set; }

        public DbSet<Volunteer> Volunteers { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<SizeCategory> SizeCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
               .Entity<Pet>()
               .HasOne(p => p.Shelter)
               .WithMany(p => p.Pets)
               .HasForeignKey(c => c.ShelterId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
               .Entity<Pet>()
               .HasOne(p => p.SizeCategory)
               .WithMany(p => p.Pets)
               .HasForeignKey(c => c.SizeCategoryId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Volunteer>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Volunteer>(v => v.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}