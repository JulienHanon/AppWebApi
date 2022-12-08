
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        
    }
    //Autant de ligne que de table dans bdd
    public DbSet<Bird> Birds { get; set; } 

    public DbSet<Box> Boxes { get; set; }

}
