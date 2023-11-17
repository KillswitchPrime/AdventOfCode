using System.Drawing;

namespace _2022._14;

public static class RegolithReservoir
{
    public static void SimulateFallingSand(bool drawGrid = false)
    {
        var grid = CreateGrid();

        var sandAtBottom = false;
        var bottom = grid.Values.MaxBy(p => p.Y).Y;
        var xSand = 500;
        var ySand = 0;
        while (sandAtBottom is false)
        {
            while (ySand + 1 <= bottom && grid[$"{xSand},{ySand + 1}"].Item is GridItemEnum.Air)
            {
                ySand++;

                if (ySand != bottom) continue;
                sandAtBottom = true;
                break;
            }

            if (sandAtBottom)
                break;

            if (grid[$"{xSand - 1},{ySand + 1}"].Item is GridItemEnum.Air)
            {
                xSand--;
                ySand++;
                continue;
            }
            
            if (grid[$"{xSand + 1},{ySand + 1}"].Item is GridItemEnum.Air)
            {
                xSand++;
                ySand++;
                continue;
            }

            grid[$"{xSand},{ySand}"].Item = GridItemEnum.Sand;
            ySand = 0;
            xSand = 500;
        }
        
        Console.WriteLine(grid.Values.Count(i => i.Item == GridItemEnum.Sand));
        
        if(drawGrid)
            DrawGrid(grid);
    }

    public static void SimulateSandFilling(bool drawGrid = false)
    {
        var grid = CreateGrid(true);

        var sandBlockingSource = false;
        var xSand = 500;
        var ySand = 0;
        while (sandBlockingSource is false)
        {
            
            while (grid[$"{xSand},{ySand + 1}"].Item is GridItemEnum.Air)
            {
                ySand++;
            }
            
            if (grid[$"{xSand - 1},{ySand + 1}"].Item is GridItemEnum.Air)
            {
                xSand--;
                ySand++;
                continue;
            }
            
            if (grid[$"{xSand + 1},{ySand + 1}"].Item is GridItemEnum.Air)
            {
                xSand++;
                ySand++;
                continue;
            }
            
            if (ySand == 0)
                sandBlockingSource = true;

            grid[$"{xSand},{ySand}"].Item = GridItemEnum.Sand;
            ySand = 0;
            xSand = 500;
        }
        
        Console.WriteLine(grid.Values.Count(i => i.Item == GridItemEnum.Sand));
        
        if(drawGrid)
            DrawGrid(grid);
    }

    private static void DrawGrid(Dictionary<string, GridItem> grid)
    {
        var stringGrid = new List<string>();
        var y = 0;
        foreach (var item in grid.Values)
        {
            if (item.Y > y)
            {
                stringGrid.Add("\n");
                y++;
            }

            switch (item.Item)
            {
                case GridItemEnum.Air:
                    stringGrid.Add(".");
                    break;
                case GridItemEnum.Rock:
                    stringGrid.Add("#");
                    break;
                case GridItemEnum.Sand:
                    stringGrid.Add("o");
                    break;
                case GridItemEnum.Source:
                    stringGrid.Add("+");
                    break;
                default:
                    stringGrid.Add(".");
                    break;
            }
        }
        Console.WriteLine(stringGrid.Aggregate((s1, s2) => s1 + s2));
    }

    private static Dictionary<string, GridItem> CreateGrid(bool hasFloor = false)
    {
        var file = File.ReadLines(
            "C:\\Users\\ChristopherIshaqueJa\\RiderProjects\\AdventOfCode2022\\AdventOfCode2022\\14\\RockPaths.txt");

        var rockPaths = new List<List<Point>>();

        foreach (var line in file)
        {
            rockPaths.Add(new List<Point>());
            
            var paths = line.Split(" -> ");
            
            foreach (var path in paths)
            {
                var points = path.Split(",").Select(int.Parse).ToList();
                
                rockPaths.Last().Add(new Point(points[0], points[1]));
            }
        }

        var pathMaxY = rockPaths.SelectMany(l => l).MaxBy(p => p.Y).Y;
        var pathMaxX = hasFloor ? 690 : rockPaths.SelectMany(l => l).MaxBy(p => p.X).X + 3;
        var pathMinX = hasFloor ? 310 : rockPaths.SelectMany(l => l).MinBy(p => p.X).X - 3;
        
        var grid = new Dictionary<string, GridItem>();
        for (var y = 0; y < pathMaxY + 10; y++)
        {
            for(var x = pathMinX; x < pathMaxX; x++)
            {
                var gridItem = GridItemEnum.Air;
                if (y == pathMaxY + 2 && hasFloor)
                    gridItem = GridItemEnum.Rock;
                
                grid.Add($"{x},{y}", new GridItem
                {
                    IsMoving = false,
                    Item = gridItem,
                    X = x,
                    Y = y
                });
            }
        }

        grid["500,0"].Item = GridItemEnum.Source;

        foreach (var paths in rockPaths)
            for (var i = 1; i < paths.Count; i++)
            {
                if (paths[i].Y - paths[i - 1].Y != 0)
                {
                    var start = paths[i].Y > paths[i - 1].Y ? paths[i - 1].Y : paths[i].Y;
                    var end = paths[i].Y < paths[i - 1].Y ? paths[i - 1].Y : paths[i].Y;
                    
                    for (var y = start; y <= end; y++)
                        grid[$"{paths[i].X},{y}"].Item = GridItemEnum.Rock;
                }
                
                var startX = paths[i].X > paths[i - 1].X ? paths[i - 1].X : paths[i].X;
                var endX = paths[i].X < paths[i - 1].X ? paths[i - 1].X : paths[i].X;
                for(var x = startX; x <= endX; x++)
                    grid[$"{x},{paths[i].Y}"].Item = GridItemEnum.Rock;
            }

        return grid;
    }
}