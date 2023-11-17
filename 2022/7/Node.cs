namespace _2022._7;

public class Node
{
    public Node(string name, List<Node> parentDirectories)
    {
        Name = name;
        Files = new List<long>();
        ChildDirectories = new Dictionary<string, Node>();
        ParentDirectories = parentDirectories;
    }
    
    public string Name { get; }
    public Dictionary<string,Node> ChildDirectories { get; }
    public List<Node> ParentDirectories { get; }
    public List<long> Files { get; }

    public Node EnterDirectory(string name)
    {
        return ChildDirectories[name];
    }

    public Node LeaveDirectory()
    {
        return ParentDirectories.Last();
    }

    private long GetDirectorySize()
    {
        return Files.Sum();
    }

    public long AllChildDirectorySizes(Node node)
    {
        var fileSize = node.GetDirectorySize();

        foreach (var child in node.ChildDirectories.Values)
        {
            fileSize += AllChildDirectorySizes(child);
        }

        return fileSize;
    }
}