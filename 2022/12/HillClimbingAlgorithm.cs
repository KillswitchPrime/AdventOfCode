namespace _2022._12;

public static class HillClimbingAlgorithm
{
    public static long CalculateFewestSteps()
    {
        var map = CreateMap();

        var startingTile = map.SelectMany(x => x).First(t => t.Height == 'S');
        startingTile.Height = 'a';
        startingTile.Distance = 0;

        return DijkstrasShortestPath(startingTile);
    }
    
    public static long CalculateFewestStepsAllTiles()
    {
        var map = CreateMap();

        var startingTiles = map.SelectMany(l => l).Where(t => t.Height == 'a').ToList();

        var distances = new List<long>();
        foreach (var tile in startingTiles)
        {
            tile.Distance = 0;
            distances.Add(DijkstrasShortestPath(tile));
        }

        return distances.Min();
    }

    private static List<List<Tile>> CreateMap()
    {
        var file = File.ReadLines(Path.Combine(AppContext.BaseDirectory, "12", "HeightMap.txt"));

        var map = new List<List<Tile>>();

        foreach (var line in file)
        {
            map.Add(new List<Tile>());
            foreach (var symbol in line)
            {
                map.Last().Add(new Tile(symbol));
            }
        }

        for (var i = 0; i < map.Count; i++)
        {
            for (var j = 0; j < map.First().Count; j++)
            {
                map[i][j].Index = (i * 10) + j;
                map[i][j].Tiles[DirectionEnum.Down] = i < map.Count - 1 ? map[i + 1][j] : null;
                map[i][j].Tiles[DirectionEnum.Up] = i > 0 ? map[i - 1][j] : null;
                map[i][j].Tiles[DirectionEnum.Left] = j > 0 ? map[i][j - 1] : null;
                map[i][j].Tiles[DirectionEnum.Right] = j < map.First().Count - 1 ? map[i][j + 1] : null;
            }
        }

        return map;
    }

    // Using Dijkstras Shortest Path algorithm to find the shortest distance between the starting tile and all other tiles.
    // Then just select the distance from the ending tile.
    private static long DijkstrasShortestPath(Tile startingTile)
    {
        var visitedTiles = new HashSet<Tile>();
        var priorityQueue = new PriorityQueue<Tile, long>();
        priorityQueue.Enqueue(startingTile, 0);

        while (priorityQueue.Count > 0)
        {
            var currentTile = priorityQueue.Dequeue();

            if (visitedTiles.Contains(currentTile))
                continue;

            visitedTiles.Add(currentTile);

            foreach (var tile in currentTile.Tiles.Values.Where(t =>
                         t is not null && currentTile.Height - t.Height >= -1))
            {
                if (tile.Distance <= currentTile.Distance + 1) continue;
                
                tile.Distance = currentTile.Distance + 1;
                priorityQueue.Enqueue(tile, tile.Distance);
            }
        }

        var finalTile = visitedTiles.FirstOrDefault(t => t.Height == 'E');
        return  finalTile?.Distance ?? long.MaxValue;
    }
}