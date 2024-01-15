namespace _2023._8;

public static class HauntedWasteland
{
    public static int CalculateStepsToZzz()
    {
        var file = File.ReadLines(Path.Combine(AppContext.BaseDirectory, "8", "Documents.txt")).ToList();
        var directions = file.First().ToCharArray();
        var areas = new Dictionary<string, Area>();
        var currentArea = "AAA";
        var totalSteps = 0;

        foreach (var line in file.Skip(2))
        {
            var split = line.Split(" = ", StringSplitOptions.RemoveEmptyEntries);
            
            areas.Add(split[0], new Area(split[1].Substring(1, 3), split[1].Substring(6, 3)));
        }

        var directionIndex = 0;
        while (currentArea is not "ZZZ")
        {
            var currentDirection = directions[directionIndex % directions.Length];

            currentArea = currentDirection switch
            {
                'L' => areas[currentArea].Left,
                'R' => areas[currentArea].Right,
                _ => currentArea
            };

            directionIndex++;
            totalSteps++;
        }

        return totalSteps;
    }
    
    public static long CalculateGhostStepsToZzz()
    {
        var file = File.ReadLines(Path.Combine(AppContext.BaseDirectory, "8", "Documents.txt")).ToList();
        var directions = file.First().ToCharArray();
        var areas = new Dictionary<string, Area>();

        foreach (var line in file.Skip(2))
        {
            var split = line.Split(" = ", StringSplitOptions.RemoveEmptyEntries);
            
            areas.Add(split[0], new Area(split[1].Substring(1, 3), split[1].Substring(6, 3)));
        }

        var currentAreas = areas.Keys
            .Where(area => area[^1] is 'A')
            .Select(area => new Place(area, 0, false))
            .ToList();

        var directionIndex = 0;
        while (currentAreas.All(p => p.HasCycled) is false)
        {
            foreach (var currentArea in currentAreas)
            {
                currentArea.Area = directions[directionIndex % directions.Length] switch
                {
                    'L' => areas[currentArea.Area].Left,
                    'R' => areas[currentArea.Area].Right,
                    _ => currentArea.Area
                };
                
                if(currentArea.HasCycled is false)
                    currentArea.Steps++;
                
                if (currentArea.Area[^1] is 'Z')
                    currentArea.HasCycled = true;
            }

            directionIndex++;
        }
        
        return FindLcm(currentAreas.Select(p => p.Steps).ToArray());
    }
    
    private static long FindLcm(IReadOnlyList<int> numbers)
    {
        var lcm = (long)numbers[0];
        
        for (var i = 1; i < numbers.Count; i++)
        {
            lcm = FindLcm(lcm, numbers[i]);
        }

        return lcm;
    }

    private static long FindLcm(long a, long b)
    {
        return Math.Abs(a * b) / FindGcd(a, b);
    }

    private static long FindGcd(long a, long b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }

        return Math.Abs(a);
    }
}

internal readonly struct Area(string left, string right)
{
    public string Left { get; } = left;
    public string Right { get; } = right;
}

internal class Place(string area, int steps, bool hasCycled)
{
    public string Area { get; set; } = area;
    public int Steps { get; set; } = steps;
    public bool HasCycled { get; set; } = hasCycled;
}