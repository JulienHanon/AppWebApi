using System.ComponentModel.DataAnnotations.Schema;
public class Bird
{
    public int ID { get; set; }
    public string name { get; set; } = string.Empty;
    public string type { get; set; } = string.Empty;
    public string species { get; set; } = string.Empty;
    public int envergure { get; set; } 
    public beak beak { get; set; } 
    public string color { get; set; } = string.Empty;
    [ForeignKey("BoxId")]
    public int BoxId { get; set; }
    public Box Box { get; set; }
}
public enum beak
{
    Pointu,
    Crochu,
}
