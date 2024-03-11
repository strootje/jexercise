namespace jexercise;

public class JexConfiguration(IConfiguration configuration)
{
   private readonly IConfiguration configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
   public string JexConnectionString => configuration.GetConnectionString("JexData") ?? throw new MissingFieldException("Missing JexData in ConnectionStrings");
}