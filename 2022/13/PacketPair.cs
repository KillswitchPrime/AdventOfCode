namespace _2022._13;

public class PacketPair
{
    public PacketPair(string left, string right, int index)
    {
        Left = left;
        Right = right;
        Index = index;
    }
    
    public int Index { get; }
    public string Left { get; }
    public string Right { get; }
}