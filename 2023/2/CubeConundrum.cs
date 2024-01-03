namespace _2023._2;

public static class CubeConundrum
{
    public static int SumGameIds()
    {
        var file = File.ReadLines(Path.Combine(AppContext.BaseDirectory, "2", "GameCubes.txt"));
        var gameIds = new List<int>();
        var cubes = new Dictionary<string, int>{{"red", 12}, {"green", 13}, {"blue", 14}};

        foreach (var line in file)
        {
            var game = line.Split(": ");
            var currentId = int.Parse(game[0].Split(" ")[1]);

            var shouldAddId = true;
            foreach (var color in game[1].Split(new[] { ", ", "; " }, StringSplitOptions.None))
            {
                var cube = color.Split(" ");
                if (int.Parse(cube[0]) <= cubes[cube[1]]) 
                    continue;
                
                shouldAddId = false;
                break;
            }
            if(shouldAddId)
                gameIds.Add(currentId);
        }

        return gameIds.Sum();
    }

    public static int SumCubePowers()
    {
        var file = File.ReadLines(Path.Combine(AppContext.BaseDirectory, "2", "GameCubes.txt"));
        var cubes = new Dictionary<string, int>
        {
            {"red", 0},{"green", 0},{"blue", 0}
        };
        var sumPowers = 0;

        foreach (var line in file)
        {
            foreach (var color in line
                         .Split(": ")[1]
                         .Split(new[] { ", ", "; " }, 
                             StringSplitOptions.None))
            {
                var cube = color.Split(" ");
                
                if(cubes[cube[1]] < int.Parse(cube[0]))
                    cubes[cube[1]] = int.Parse(cube[0]);
            }

            var currentPower = 1;
            foreach(var key in cubes.Keys)
            {
                currentPower *= cubes[key];
                cubes[key] = 0;
            }
            sumPowers += currentPower;
        }

        return sumPowers;
    }
}