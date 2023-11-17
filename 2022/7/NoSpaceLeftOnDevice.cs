namespace _2022._7;

public static class NoSpaceLeftOnDevice
{
    public static long CalculateDirectorySize()
    {
        return GetFileSizes().Sum(directorySize => directorySize <= 100_000L ? directorySize : 0L);
    }
    
    public static long GetRelevantDirectorySize()
    {
        var directorySizes = GetFileSizes();
        return directorySizes.Where(d => 70_000_000 - (directorySizes.First() - d) >= 30_000_000).Order().First();
    }

    private static List<long> GetFileSizes()
    {
        var file = File.ReadLines("C:\\Users\\ChristopherIshaqueJa\\RiderProjects\\AdventOfCode2022\\AdventOfCode2022\\7\\ComputerInstructions.txt");

        var directorySizes = new List<long>();
        var currentDirectory = new Node("/",new List<Node>());

        foreach (var line in file.Skip(1))
        {
            if (line.StartsWith("$ ls"))
                continue;
            
            if (line.StartsWith("$ cd .."))
                currentDirectory = currentDirectory.LeaveDirectory();
            else if (line.StartsWith("$ cd"))
                currentDirectory = currentDirectory.EnterDirectory(line.Split(" ")[2]); // 0:$ 1:dir 2:name
            else if (line.StartsWith("dir"))
            {
                var parents = currentDirectory.ParentDirectories.ToList();
                parents.Add(currentDirectory);
                var name = line.Split(" ")[1]; // 0:dir 1:name
                currentDirectory.ChildDirectories.Add(name, new Node(name, parents));
                
            }
            else
                currentDirectory.Files.Add(long.Parse(line.Split(" ")[0])); // 0:filesize 1:filename
        }

        currentDirectory = currentDirectory.ParentDirectories.First();
        
        TraverseTree(currentDirectory, directorySizes);

        return directorySizes;
    }

    private static void TraverseTree(Node node, List<long> directorySizes)
    {
        directorySizes.Add(node.AllChildDirectorySizes(node));
        foreach (var directory in node.ChildDirectories.Values)
        {
            TraverseTree(directory, directorySizes);
        }
    }
}