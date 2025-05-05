namespace _4Time.DataCore.Models;

internal class Category
{
    public int CategoryID { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsWorkTime { get; set; }
}
