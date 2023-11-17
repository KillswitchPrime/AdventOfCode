namespace _2022._9;

public class Knot
{
    public Knot(int x, int y)
    {
        X = x;
        Y = y;

        Visited.Add((x, y));
    }

    private int X { get; set; }
    private int Y { get; set; }

    public HashSet<(int, int)> Visited { get; } = new();

    public void Follow(Knot leadKnot)
    {
        if (Math.Abs(leadKnot.X - X) < 2 && Math.Abs(leadKnot.Y - Y) < 2) return;
        if (leadKnot.X == X) // if same row, move across column
        {
            if (leadKnot.Y > Y) Y++;
            else Y--;
        }
        else if (leadKnot.Y == Y) // if same column, move across row
        {
            if (leadKnot.X > X) X++;
            else X--;
        }
        else // move diagonally
        {
            if (leadKnot.X > X) X++;
            else X--;

            if (leadKnot.Y > Y) Y++;
            else Y--;
        }

        Visited.Add((X, Y));
    }

    public void Move(string direction)
    {
        if (direction == "L") X--;
        else if (direction == "R") X++;
        else if (direction == "U") Y++;
        else if (direction == "D") Y--;

        Visited.Add((X, Y));
    }
}