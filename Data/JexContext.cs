using Microsoft.EntityFrameworkCore;

namespace jexercise;

public class Company
{
   public int Id { get; set; }
   public required string Name { get; set; }
   public required Address Address { get; set; }

   public virtual required ICollection<JobOffer> JobOffers { get; set; }
}

public class Address
{
   public required string Street { get; set; }
   public required string City { get; set; }
   public required string Zipcode { get; set; }
   public required string Country { get; set; }
}

public class JobOffer
{
   public int Id { get; set; }
   public required string Title { get; set; }
   public required string Description { get; set; }

   public virtual required Company Company { get; set; }
}

public class JexContext(JexConfiguration configuration) : DbContext
{
   private readonly JexConfiguration configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

   protected override void OnConfiguring(DbContextOptionsBuilder opts)
      => opts.UseSqlite(configuration.JexConnectionString);

   protected override void OnModelCreating(ModelBuilder builder)
   {
      builder.Entity<Company>(entity =>
      {
         entity.ToTable("Companies");
         entity.OwnsOne(p => p.Address);
         entity.HasMany(p => p.JobOffers).WithOne(p => p.Company);
      });

      builder.Entity<JobOffer>(entity =>
      {
         entity.ToTable("JobOffers");
         entity.HasOne(p => p.Company).WithMany(p => p.JobOffers);
      });
   }

   public DbSet<Company> Companies { get; set; }
   public DbSet<JobOffer> JobOffers { get; set; }
}
