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
        new Hero { ID = 4, name = "Black Widow", Firstname="flemme", LastName="flemme", Place="Somewhere"},
        new Hero { ID = 5, name = "flemmeman", Firstname="truc", LastName="autretruc", Place="Somewhere"}
    };

    private readonly ApplicationDbContext _context;
    public HeroController(ApplicationDbContext dbContext) //permet de recuperer une instance uniquement (singleton)
    {
        this._context = dbContext;
    }
    
    [HttpGet("/GetHero")]
    public async Task<ActionResult<List<Hero>>> Get()
    {
        var myHeroes = await _context.Heroes.ToListAsync();
        return Ok(myHeroes);
    }

    [HttpPost("/PostHero")]
    public async Task<ActionResult<List<Hero>>> CreateHero([FromBody] Hero hero)
    {
        if(hero == null) return BadRequest();
        await this._context.Heroes.AddAsync(hero);
        await this._context.SaveChangesAsync(); //Commit changement bdd
        return Ok(await this._context.Heroes.ToListAsync());
    }

    [HttpPut]
    public async Task<ActionResult<List<Hero>>> UpdateHero(Hero request)
    {

        var hero = heroes.Find(hero=> hero.ID == request.ID);
        if (hero == null)
        {
            return BadRequest(request);
        }

        hero.name = request.name;
        hero.Firstname = request.Firstname;
        hero.LastName = request.LastName;
        hero.Place = request.Place;

        return heroes;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Hero>> Get(int id)
    {
        var hero = heroes.Find(x => x.ID == id);
        if (hero == null)
        {
            return NotFound("Hero not Found");
        }
        return Ok(hero);
    }


    
}