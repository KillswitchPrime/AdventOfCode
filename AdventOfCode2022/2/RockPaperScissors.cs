namespace AdventOfCode2022._2;

public static class RockPaperScissors
{
    public static long CalculateScore()
    {
        var file = File.ReadLines("C:\\Users\\ChristopherIshaqueJa\\RiderProjects\\AdventOfCode2022\\AdventOfCode2022\\2\\StrategyGuid.txt");

        var totalScore = 0L;
        foreach (var line in file)
        {
            var selection = line.Split(" ");
            totalScore += selection[1] switch
            {
                "X" => selection[0] switch
                {
                    "A" => 4,
                    "B" => 1,
                    "C" => 7,
                    _ => 0
                },
                "Y" => selection[0] switch
                {
                    "A" => 8,
                    "B" => 5,
                    "C" => 2,
                    _ => 0
                },
                "Z" => selection[0] switch
                {
                    "A" => 3,
                    "B" => 9,
                    "C" => 6,
                    _ => 0
                },
                _ => 0
            };
        }

        return totalScore;
    }

    public static long CalculateActualScore()
    {
        var file = File.ReadLines("C:\\Users\\ChristopherIshaqueJa\\RiderProjects\\AdventOfCode2022\\AdventOfCode2022\\2\\StrategyGuid.txt");

        var totalScore = 0L;
        foreach (var line in file)
        {
            var selection = line.Split(" ");
            totalScore += selection[1] switch
            {
                "X" => selection[0] switch
                {
                    "A" => 3,
                    "B" => 1,
                    "C" => 2,
                    _ => 0
                },
                "Y" => selection[0] switch
                {
                    "A" => 4,
                    "B" => 5,
                    "C" => 6,
                    _ => 0
                },
                "Z" => selection[0] switch
                {
                    "A" => 8,
                    "B" => 9,
                    "C" => 7,
                    _ => 0
                },
                _ => 0
            };
        }

        return totalScore;
    }
}