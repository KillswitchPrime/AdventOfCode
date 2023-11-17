namespace _2022._8;

public static class TreetopTreeHouse
{
    public static long TotalVisibleTrees()
    {
        return CreateTrees().Sum(treeList => treeList.LongCount(tree => tree.IsVisible()));
    }

    public static long CalculateScenicScore()
    {
        var scenicScores = new List<long>();
        foreach (var treeList in CreateTrees())
        {
            scenicScores.AddRange(treeList.Select(tree => tree.ScenicScore()));
        }

        return scenicScores.Max();
    }

    private static List<List<Tree>> CreateTrees()
    {
        var file = File.ReadLines("C:\\Users\\ChristopherIshaqueJa\\RiderProjects\\AdventOfCode2022\\AdventOfCode2022\\8\\TreeHeights.txt");
        var trees = new List<List<Tree>>();

        var indexY = 0;
        foreach (var line in file)
        {
            trees.Add(new List<Tree>());

            var indexX = 0;
            foreach (var height in line)
            {
                trees[indexY].Add(new Tree
                (
                    indexX,
                    indexY,
                    int.Parse(height.ToString())
                ));

                indexX++;
            }

            indexY++;
        }
        
        foreach (var tree in trees.SelectMany(t => t))
        {
            tree.Above = tree.Y == 0 ? null : trees[tree.Y - 1][tree.X];
            tree.Below = tree.Y == trees.Count - 1 ? null : trees[tree.Y + 1][tree.X];
            tree.Left = tree.X == 0 ? null : trees[tree.Y][tree.X - 1];
            tree.Right = tree.X == trees[tree.Y].Count - 1 ? null : trees[tree.Y][tree.X + 1];
        }

        return trees;
    }
}