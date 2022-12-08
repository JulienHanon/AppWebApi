using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]

public class HeroController : ControllerBase
{
    private static List<Hero> heroes = new List<Hero>
    {
        new Hero { ID = 1, name = "Iron Man", Firstname="Tony", LastName="Stark", Place="Somewhere"},
        new Hero { ID = 2, name = "Captain America", Firstname="Steve", LastName="Roger", Place="Queens"},
        new Hero { ID = 3, name = "Hulk", Firstname="Bruce", LastName="Banner", Place="Somewhere"},
    };

    private readonly ApplicationDbContext _context;
    public HeroController(ApplicationDbContext dbContext) //permet de recuperer une instance uniquement (singleton)
    {
        this._context = dbContext;
    }
    
    //Permet de récuperer la liste de tous les héros de la bdd
    [HttpGet("/GetHero")]
    public async Task<ActionResult<List<Hero>>> Get()
    {
        var myHeroes = await _context.Heroes.ToListAsync();
        return Ok(myHeroes);
    }
    //Permet de récuperer 1 hero de la bdd en fonction de son id
    [HttpGet("{id}")]
    public async Task<ActionResult<Hero>> Get(int id)
    {
        var myHeroes = await _context.Heroes.ToListAsync();
        var hero = myHeroes.Find(x => x.ID == id);
        if (hero == null)
        {
            return NotFound("Hero not Found");
        }
        return Ok(hero);
    }
    // Permet de créer un hero dans la bdd
    [HttpPost("/PostHero")]
    public async Task<ActionResult<List<Hero>>> CreateHero([FromBody] Hero hero)
    {
        if(hero == null) return BadRequest();
        await this._context.Heroes.AddAsync(hero);
        await this._context.SaveChangesAsync(); //Commit changement bdd
        return Ok(await this._context.Heroes.ToListAsync());
    }

    [HttpPut]
    public async Task<ActionResult<List<Hero>>> UpdateHero([FromBody] Hero request)
    {
        if(request == null) {
            return BadRequest();
        } 
        var hero = await this._context.Heroes.FindAsync(request.ID);
        if(hero == null) {
            return BadRequest();
        } 
        if(hero.name != null) {
            hero.name = request.name; 
        }
        if(hero.Firstname != null) {
            hero.Firstname = request.Firstname; 
        } 
        if(hero.LastName != null) {
            hero.LastName = request.LastName;
        }
        if(hero.Place != null) {
            hero.Place = request.Place; 
        } 
        this._context.Heroes.Update(hero);  
        this._context.SaveChanges(); 
        return Ok(); 
    }

    //Supprime hero de la bdd en fonction de l'id 
    [HttpDelete("delete_hero")]
    public async Task<ActionResult<List<Hero>>> DeleteHero([FromBody] int id) {
        if(id == 0) {
            return BadRequest();
        } 
        var hero = await this._context.Heroes.FindAsync(id); 
        if(hero == null) {
            return BadRequest();
        } 
        this._context.Heroes.Remove(hero);
        this._context.SaveChanges();  
        return Ok(); 
    }



   


    
}