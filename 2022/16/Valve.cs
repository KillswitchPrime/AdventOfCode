namespace _2022._16;

public class Valve
{
    public string? Name { get; set; }
    public Valve? Parent { get; set; }
    public List<Valve>? Neighbours { get; init; }
    public long FlowRate { get; init; }
    public bool IsOpen { get; set; }
    public long G { get; set; }
    public long H { get; set; }
    public long F => G + H;
}