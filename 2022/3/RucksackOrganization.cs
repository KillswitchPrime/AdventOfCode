namespace _2022._3;

public static class RucksackOrganization
{
    public static long CalculatePriorities()
    {
        var file = File.ReadLines("C:\\Users\\ChristopherIshaqueJa\\RiderProjects\\AdventOfCode2022\\AdventOfCode2022\\3\\Rucksacks.txt").ToList();

        var prioritySum = 0L;
        foreach (var line in file)
        {
            var firstCompartment = line[..(line.Length / 2)];
            var secondCompartment = line[(line.Length / 2)..];

            var priorityLetter = firstCompartment.FirstOrDefault(l => secondCompartment.Contains(l));

            var index = priorityLetter - 64;

            prioritySum += index switch
            {
                > 0 and <= 26 => index + 26,
                > 32 and <= 59 => index - 32,
                _ => 0
            };
        }

        return prioritySum;
    }

    public static long CalculateBadgePriority()
    {
        var file = File.ReadLines("C:\\Users\\ChristopherIshaqueJa\\RiderProjects\\AdventOfCode2022\\AdventOfCode2022\\3\\Rucksacks.txt").ToList();

        var prioritySum = 0L;
        for(var i = 0; i < file.Count; i+=3)
        {
            var badgeLetter = file[i].FirstOrDefault(x => file[i + 1].Where(y => file[i + 2].Contains(y)).Contains(x));

            var index = badgeLetter - 64;

            prioritySum += index switch
            {
                > 0 and <= 26 => index + 26,
                > 32 and <= 59 => index - 32,
                _ => 0
            };
        }

        return prioritySum;
    }
}