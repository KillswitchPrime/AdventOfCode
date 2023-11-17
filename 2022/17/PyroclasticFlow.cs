namespace _2022._17;

public static class PyroclasticFlow
{
    public static long CalculateTowerHeight()
    {
        var file = File.ReadLines(
            "C:\\Users\\ChristopherIshaqueJa\\RiderProjects\\AdventOfCode2022\\AdventOfCode2022\\17\\GasJets.txt");

        var jets = file.SelectMany(line => line).ToList();

        var rocks = new List<char[,]>
        {
            new char[,]
            {
                {'#', '#', '#', '#'}
            },
            new char[,]
            {
                {'.', '#', '.'},{'#', '#', '#'},{'.', '#', '.'}
            },
            new char[,]
            {
                {'.', '.', '#'},{'.', '.', '#'},{'#', '#', '#'}
            },
            new char[,]
            {
                {'#'},{'#'},{'#'},{'#'}
            },
            new char[,]
            {
                {'#', '#'},{'#', '#'}
            }
        };

        var jetIndex = 0;
        var rockIndex = 0;
        var towerLine = new char[7]
        {
            '.', '.', '.', '.', '.', '.' ,'.'
        };
        var rockTower = new[]
        {
            towerLine,
            towerLine,
            towerLine
        };
        
        for (var numberOfRocks = 0; numberOfRocks < 2022; numberOfRocks++)
        {
            var currentRock = rocks[rockIndex];

            var newLine = towerLine;
         
            var isFalling = true;
            while (isFalling)
            {
                
                jetIndex = jetIndex == jets.Count - 1 ? 0 : jetIndex + 1;
            }

            rockIndex = rockIndex == rocks.Count - 1 ? 0 : rockIndex + 1;
        }

        return 0L;
    }
}