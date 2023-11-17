namespace _2022._4;

public static class CampCleanup
{
    public static long CalculateCleanupOverlap()
    {
        var file = File.ReadLines("C:\\Users\\ChristopherIshaqueJa\\RiderProjects\\AdventOfCode2022\\AdventOfCode2022\\4\\AssignmentPairs.txt");

        var overlappedPairs = 0L;
        foreach (var line in file)
        {
            var pairs = line.Split(",");
            var firstRange = pairs[0].Split("-").Select(int.Parse).ToList();
            var lastRange = pairs[1].Split("-").Select(int.Parse).ToList();

            if (firstRange[0] <= lastRange[0] && firstRange[1] >= lastRange[1])
                overlappedPairs++;
            else if (lastRange[0] <= firstRange[0] && lastRange[1] >= firstRange[1])
                overlappedPairs++;
        }

        return overlappedPairs;
    }
    
    public static long CalculateCleanupPartialOverlap()
    {
        var file = File.ReadLines("C:\\Users\\ChristopherIshaqueJa\\RiderProjects\\AdventOfCode2022\\AdventOfCode2022\\4\\AssignmentPairs.txt");

        var overlappedPairs = 0L;
        foreach (var line in file)
        {
            var pairs = line.Split(",");
            var firstRange = pairs[0].Split("-").Select(int.Parse).ToList();
            var lastRange = pairs[1].Split("-").Select(int.Parse).ToList();

            if (firstRange[0] >= lastRange[0] && firstRange[0] <= lastRange[1])
                overlappedPairs++;
            else if (firstRange[1] >= lastRange[0] && firstRange[1] <= lastRange[1])
                overlappedPairs++;
            else if (lastRange[0] >= firstRange[0] && lastRange[0] <= firstRange[1])
                overlappedPairs++;
            else if (lastRange[1] >= firstRange[0] && lastRange[1] <= firstRange[1])
                overlappedPairs++;
        }

        return overlappedPairs;
    }
}