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
   public int CompanyId { get; set; }
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
      var jex = new Company { Id = 1, Name = "Jex" };
      var koekela = new Company { Id = 2, Name = "Koekela" };
      var bar3 = new Company { Id = 3, Name = "Bar 3" };

      builder.Entity<Company>(entity =>
      {
         entity.ToTable("Companies");
         entity.HasMany(p => p.JobOffers).WithOne(p => p.Company).HasForeignKey(p => p.CompanyId);
         entity.HasIndex(p => p.Name).IsUnique();

         entity.HasData(jex, koekela, bar3);
         entity.OwnsOne(p => p.Address).HasData(
            new Address { CompanyId = jex.Id, Street = "Nassaukade 5", City = "Rotterdam", Zipcode = "3071 JL", Country = "Netherlands" },
            new Address { CompanyId = koekela.Id, Street = "Nieuwe Binnenweg 79a", City = "Rotterdam", Zipcode = "3014 GB", Country = "Netherlands" },
            new Address { CompanyId = bar3.Id, Street = "", City = "Rotterdam", Zipcode = "3014 GE", Country = "Netherlands" });
      });

      builder.Entity<JobOffer>(entity =>
      {
         entity.ToTable("JobOffers");
         entity.HasOne(p => p.Company).WithMany(p => p.JobOffers).HasForeignKey(p => p.CompanyId);

         entity.HasData(
            new JobOffer { Id = 1, Title = "Software Engineer", CompanyId = jex.Id, Description = "Turn Code into Coffee" },
            new JobOffer { Id = 2, Title = "Cake Engineer", CompanyId = koekela.Id, Description = "Turn Cake into Coffee" },
            new JobOffer { Id = 3, Title = "CEO", CompanyId = jex.Id, Description = "Feed the children of Rotterdam" },
            new JobOffer { Id = 4, Title = "Bier Tapper", CompanyId = bar3.Id, Description = "Lekker biertjes tappen" },
            new JobOffer { Id = 5, Title = "Afwasser", CompanyId = bar3.Id, Description = "Spelen in een sopje" },
            new JobOffer { Id = 6, Title = "Fietscourier", CompanyId = koekela.Id, Description = "Uitkijken met die hobbels" },
            new JobOffer { Id = 7, Title = "Test Engineer", CompanyId = jex.Id, Description = "Turn food for children into Tests" });
      });
   }

   public DbSet<Company> Companies { get; set; }
   public DbSet<JobOffer> JobOffers { get; set; }
}
