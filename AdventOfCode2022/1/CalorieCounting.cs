using System.Runtime.InteropServices.ComTypes;

namespace AdventOfCode2022._1;

public static class CalorieCounting
{
    public static long GetHighestCalorieCount()
    {
        var file = File.ReadLines("C:\\Users\\ChristopherIshaqueJa\\RiderProjects\\AdventOfCode2022\\AdventOfCode2022\\1\\Calories.txt");

        var calories = new List<long>();
        var currentCalorieCount = 0L;
        foreach (var line in file)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                calories.Add(currentCalorieCount);
                currentCalorieCount = 0;
                continue;
            }

            currentCalorieCount += long.Parse(line);
        }
        
        //return calories.Max();
        return calories.OrderDescending().Take(3).Sum();
    }
}