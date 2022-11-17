namespace API.Utilities;

public class UserParams
{
    public int MinAge { get; set; } = 18;
    public int MaxAge { get; set; } = 200;
    public string Gender { get; set; } = null!;
    public string? CurrentUsername { get; set; }
    public string OrderBy { get; set; } = "created";
}