using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]

public class BirdController : ControllerBase
{

    private readonly ApplicationDbContext _context;
    public BirdController(ApplicationDbContext dbContext) //permet de recuperer une instance uniquement (singleton)
    {
        this._context = dbContext;
    }
    
    //Permet de récuperer la liste de tous les oiseaux de la bdd
    [HttpGet("GetBird")]
    public async Task<ActionResult<List<Bird>>> Get()
    {
        var myBirds = await this._context.Birds.ToListAsync();
        foreach (Bird bird in myBirds){
            var box = await this._context.Boxes.FindAsync(bird.BoxId);
            if (box != null){
               bird.Box = box;
               this._context.Birds.Update(bird);  
            }
        }
        
        this._context.SaveChanges(); 
        return Ok(myBirds);
    }
    //Permet de récuperer 1 oiseau de la bdd en fonction de son id
    [HttpGet("{id}")]
    public async Task<ActionResult<Bird>> Get(int id)
    {
        var myBirds = await this._context.Birds.ToListAsync();
        var bird = myBirds.Find(x => x.ID == id);
        if (bird == null)
        {
            return NotFound("Bird not Found");
        }
        return Ok(bird);
    }
    // Permet de créer un oiseau dans la bdd
    [HttpPost("PostBird")]
    public async Task<ActionResult<List<Bird>>> CreateBird([FromBody] Bird bird)
    {
        
        if(bird == null) return BadRequest();
        var box = await this._context.Boxes.FindAsync(bird.BoxId);
        if(box != null){
            bird.Box = box;
        }
        await this._context.Birds.AddAsync(bird);
        await this._context.SaveChangesAsync(); //Commit changement bdd
        return Ok(await this._context.Birds.ToListAsync());
    }
    
    
    [HttpPut("UpdateBird")]
    public async Task<ActionResult<List<Bird>>> UpdateBird([FromBody] Bird request)
    {
        if(request == null) {
            return BadRequest();
        } 
        var bird = await this._context.Birds.FindAsync(request.ID);
        if(bird == null) {
            return BadRequest();
        } 
        if(bird.name != null) {
            bird.name = request.name; 
        }
        if(bird.type != null) {
            bird.type = request.type; 
        }
        if(bird.species != null) {
            bird.species = request.species; 
        }
        if(bird.color != null) {
            bird.color = request.color; 
        }
        this._context.Birds.Update(bird);  
        this._context.SaveChanges(); 
        return Ok(); 
    
    }

    //Supprime oiseau de la bdd en fonction de l'id 
    [HttpDelete("deleteBird")]
    public async Task<ActionResult<List<Bird>>> DeleteBird([FromBody] int id) {
        if(id == 0) {
            return BadRequest();
        } 
        var bird = await this._context.Birds.FindAsync(id); 
        if(bird == null) {
            return BadRequest();
        } 
        this._context.Birds.Remove(bird);
        this._context.SaveChanges();  
        return Ok(); 
    }



   


    
}