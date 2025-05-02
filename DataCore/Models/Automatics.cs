namespace _4Time.DataCore.Models;

internal class Automatics
{
    public int AutomaticID { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsChangeable { get; set; }
    public bool IsActive { get; set; }
}
