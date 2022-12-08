
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        
    }
    public DbSet<Hero> Heroes { get; set; } //Autant de ligne que de table dans bdd

}
