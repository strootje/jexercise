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
      // var companyJex = new Company { Id = 1, Name = "Jex", Address = new Address { Street = "Nassaukade 5", City = "Rotterdam", Zipcode = "3071 JL", Country = "Netherlands" } };
      // var companyKoekela = new Company { Id = 2, Name = "Koekela", Address = new Address { Street = "Nieuwe Binnenweg 79A", City = "Rotterdam", Zipcode = "3014 GE", Country = "Netherlands" } };
      // var companyAhoy = new Company { Id = 3, Name = "Ahoy", Address = new Address { Street = "Ahoyweg 10", City = "Rotterdam", Zipcode = "3084 BA", Country = "Netherlands" } };

      builder.Entity<Company>(entity =>
      {
         entity.ToTable("Companies");
         entity.OwnsOne(p => p.Address);
         entity.HasMany(p => p.JobOffers).WithOne(p => p.Company).HasForeignKey(p => p.CompanyId);
         entity.HasIndex(p => p.Name).IsUnique();

         // entity.HasData(companyJex, companyKoekela, companyAhoy);
      });

      builder.Entity<JobOffer>(entity =>
      {
         entity.ToTable("JobOffers");
         entity.HasOne(p => p.Company).WithMany(p => p.JobOffers).HasForeignKey(p => p.CompanyId);

         // entity.HasData(
         //    new JobOffer { Id = 1, Title = "Software Engineer", Description = "Turn coffee into code", Company = companyJex },
         //    new JobOffer { Id = 2, Title = "Cake Engineer", Description = "Turn code into cake?..", Company = companyKoekela });
      });
   }

   public DbSet<Company> Companies { get; set; }
   public DbSet<JobOffer> JobOffers { get; set; }
}
