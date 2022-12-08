using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[ApiController]
[Route("[controller]")]

public class BoxController : ControllerBase 
{
    private readonly ApplicationDbContext _context;
    public BoxController(ApplicationDbContext dbContext)
    {
        this._context = dbContext;
    }

     //Permet de récuperer la liste de tous les box de la bdd
    [HttpGet("GetBox")]
    public async Task<ActionResult<List<Box>>> Get()
    {
        var myBoxes = await _context.Boxes.ToListAsync();
        return Ok(myBoxes);
    }

    [HttpPost("PostBox")]
    public async Task<ActionResult<List<Box>>> CreateBird([FromBody] Box box)
    {
        if(box == null) return BadRequest();
        await this._context.Boxes.AddAsync(box);
        await this._context.SaveChangesAsync(); //Commit changement bdd
        return Ok(await this._context.Boxes.ToListAsync());
    }
    //Permet de changer le box d'un oiseau
    [HttpPut("UpdateBirdBox")]
    public async Task<ActionResult<List<Box>>> UpdateBirdBox([FromBody] int BirdId, int BoxId)
    {
        // on recupere l'oiseau que l'on souhaite deplacer
        var bird = await this._context.Birds.FindAsync(BirdId);
        // on recupere le box dans lequel on met l'oiseau
        var box = await this._context.Boxes.FindAsync(BoxId);
        if (bird == null){
            return BadRequest();
        }
        if (box == null){
            return BadRequest();
        }
        //on assigne le box a l'oiseau
        bird.Box = box;
        this._context.Birds.Update(bird);  
        this._context.SaveChanges(); 
        return Ok(); 
    }


    //Supprime un box de la bdd en fonction de l'id 
    [HttpDelete("deleteBox")]
    public async Task<ActionResult<List<Bird>>> DeleteBird([FromBody] int id) {
        if(id == 0) {
            return BadRequest();
        } 
        var box = await this._context.Boxes.FindAsync(id); 
        if(box == null) {
            return BadRequest();
        } 
        this._context.Boxes.Remove(box);
        this._context.SaveChanges();  
        return Ok(); 
    }
}