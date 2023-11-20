namespace _2022._1;

public static class CalorieCounting
{
    public static long GetHighestCalorieCount()
    {
        return CreateCalorieList().Max();
    }

    public static long GetSumOfThreeHighestCalorieCounts()
    {
        return CreateCalorieList().OrderDescending().Take(3).Sum();
    }

    private static IReadOnlyList<long> CreateCalorieList()
    {
        var file = File.ReadLines(Path.Combine(AppContext.BaseDirectory, "1", "Calories.txt"));

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

        return calories;
    }
}