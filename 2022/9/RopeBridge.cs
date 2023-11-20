namespace _2022._9;

public static class RopeBridge
{
    public static long CalculateRopeTailPositions()
    {
        var instructions = File.ReadLines(Path.Combine(AppContext.BaseDirectory, "9", "RopeMotions.txt"));
        
        var rope = new Rope(2);
        rope.PerformMotions(instructions);
        var tail = rope.Knots.Last();

        
        return tail.Visited.Count;
    }

    public static long CalculateLongRopeTailPositions()
    {
        var instructions = File.ReadLines(Path.Combine(AppContext.BaseDirectory, "9", "RopeMotions.txt"));
        
        var rope = new Rope(10);
        rope.PerformMotions(instructions);
        var tail = rope.Knots.Last();

        return tail.Visited.Count;
    }
}