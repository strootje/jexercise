using Microsoft.EntityFrameworkCore;

namespace jexercise;

public interface ModelWithId
{
   int Id { get; }
}

public class Company : ModelWithId
{
   public int Id { get; set; }
   public required string Name { get; set; }
   public required Address Address { get; set; }

   public virtual List<JobOffer> JobOffers { get; set; } = [];
}

public class Address
{
   public required string Street { get; set; }
   public required string City { get; set; }
   public required string Zipcode { get; set; }
   public required string Country { get; set; }
}

public class JobOffer : ModelWithId
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
      // var companyJex = new Company { Id = 1, Name = "Jex", Address = new Address { Street = "Nassaukade 5", City = "Rotterdam", Zipcode = "3071 JL", Country = "Netherlands" } };
      // var companyKoekela = new Company { Id = 2, Name = "Koekela", Address = new Address { Street = "Nieuwe Binnenweg 79A", City = "Rotterdam", Zipcode = "3014 GE", Country = "Netherlands" } };
      // var companyAhoy = new Company { Id = 3, Name = "Ahoy", Address = new Address { Street = "Ahoyweg 10", City = "Rotterdam", Zipcode = "3084 BA", Country = "Netherlands" } };

      builder.Entity<Company>(entity =>
      {
         entity.ToTable("Companies");
         entity.OwnsOne(p => p.Address);
         entity.HasMany(p => p.JobOffers).WithOne(p => p.Company);
         entity.HasIndex(p => p.Name).IsUnique();

         // entity.HasData(companyJex, companyKoekela, companyAhoy);
      });

      builder.Entity<JobOffer>(entity =>
      {
         entity.ToTable("JobOffers");
         entity.HasOne(p => p.Company).WithMany(p => p.JobOffers);

         // entity.HasData(
         //    new JobOffer { Id = 1, Title = "Software Engineer", Description = "Turn coffee into code", Company = companyJex },
         //    new JobOffer { Id = 2, Title = "Cake Engineer", Description = "Turn code into cake?..", Company = companyKoekela });
      });
   }

   public DbSet<Company> Companies { get; set; }
   public DbSet<JobOffer> JobOffers { get; set; }
}
