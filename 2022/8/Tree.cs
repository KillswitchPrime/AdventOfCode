namespace _2022._8;

public class Tree
{
    public Tree(int x, int y, int height)
    {
        X = x;
        Y = y;
        Height = height;
        Above = null;
        Below = null;
        Left = null;
        Right = null;
    }
    
    public int X { get; }
    public int Y { get; }
    private int Height { get; }
    
    public Tree? Above { get; set; }
    public Tree? Below { get; set; }
    public Tree? Left { get; set; }
    public Tree? Right { get; set; }

    public bool IsVisible()
    {
        var isAboveVisible = true;
        var isBelowVisible = true;
        var isLeftVisible = true;
        var isRightVisible = true;

        var currentTree = Above;
        while (currentTree is not null)
        {
            if (currentTree.Height >= Height)
            {
                isAboveVisible = false;
                break;
            }
            
            currentTree = currentTree.Above;
        }
        currentTree = Below;
        while (currentTree is not null)
        {
            if (currentTree.Height >= Height)
            {
                isBelowVisible = false;
                break;
            }
            
            currentTree = currentTree.Below;
        }
        currentTree = Left;
        while (currentTree is not null)
        {
            if (currentTree.Height >= Height)
            {
                isLeftVisible = false;
                break;
            }
            
            currentTree = currentTree.Left;
        }
        currentTree = Right;
        while (currentTree is not null)
        {
            if (currentTree.Height >= Height)
            {
                isRightVisible = false; 
                break;
            }
            
            currentTree = currentTree.Right;
        }

        return isRightVisible || isLeftVisible || isBelowVisible || isAboveVisible;
    }

    public long ScenicScore()
    {
        var aboveScenicScore = 0L;
        var belowScenicScore = 0L;
        var leftScenicScore = 0L;
        var rightScenicScore = 0L;

        var currentTree = Above;
        while (currentTree is not null)
        {
            aboveScenicScore++;
            if (currentTree.Height >= Height)
                break;

            currentTree = currentTree.Above;
        }
        currentTree = Below;
        while (currentTree is not null)
        {
            belowScenicScore++;
            if (currentTree.Height >= Height)
                break;

            currentTree = currentTree.Below;
        }
        currentTree = Left;
        while (currentTree is not null)
        {
            leftScenicScore++;
            if (currentTree.Height >= Height)
                break;

            currentTree = currentTree.Left;
        }
        currentTree = Right;
        while (currentTree is not null)
        {
            rightScenicScore++;
            if (currentTree.Height >= Height)
                break;

            currentTree = currentTree.Right;
        }

        return aboveScenicScore * belowScenicScore * leftScenicScore * rightScenicScore;
    }
}