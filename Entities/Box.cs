using System.ComponentModel.DataAnnotations.Schema;

public class Box
{
    public int id { get; set; }
    public string type { get; set; } = string.Empty;
    public string terrain { get; set; } = string.Empty;
    
}