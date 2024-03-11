using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace jexercise;

public class Company
{
   public int Id { get; set; }
   public string? Name { get; set; }
   public Address? Address { get; set; }

   [JsonIgnore]
   public virtual List<JobOffer> JobOffers { get; set; } = [];
}

public class Address
{
   public string? Street { get; set; }
   public string? City { get; set; }
   public string? Zipcode { get; set; }
   public string? Country { get; set; }
}

public class JobOffer
{
   public int Id { get; set; }
   public string? Title { get; set; }
   public string? Description { get; set; }

   public int CompanyId { get; set; }
   public virtual Company? Company { get; set; }
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
         entity.HasMany(p => p.JobOffers).WithOne(p => p.Company).HasForeignKey(p => p.CompanyId);
         entity.HasIndex(p => p.Name).IsUnique();
      });

      builder.Entity<JobOffer>(entity =>
      {
         entity.ToTable("JobOffers");
         entity.HasOne(p => p.Company).WithMany(p => p.JobOffers).HasForeignKey(p => p.CompanyId);
      });
   }

   public DbSet<Company> Companies { get; set; }
   public DbSet<JobOffer> JobOffers { get; set; }
}
