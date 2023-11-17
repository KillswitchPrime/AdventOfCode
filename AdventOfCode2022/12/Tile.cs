using System.Reflection;

namespace AdventOfCode2022._12;

public class Tile
{
    public Tile(char height)
    {
        Height = height;
        Index = 0;
        Distance = long.MaxValue;
        Visited = false;
        Tiles = new Dictionary<DirectionEnum, Tile?>
        {
            { DirectionEnum.Up, null },
            { DirectionEnum.Right, null },
            { DirectionEnum.Down, null },
            { DirectionEnum.Left, null },
        };
    }

    public char Height { get; set; }
    public int Index { get; set; }
    public bool Visited { get; set; }
    public long Distance { get; set; }
    public Dictionary<DirectionEnum, Tile?> Tiles { get; }
}