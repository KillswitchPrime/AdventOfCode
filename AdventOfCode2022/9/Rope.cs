namespace AdventOfCode2022._9;

public class Rope
{
    public Rope(int numberOfKnots)
    {
        Knots = Enumerable.Range(0, numberOfKnots).Select(_ => new Knot(0, 0)).ToList();
    }

    public List<Knot> Knots { get; }

    public void PerformMotions(IEnumerable<string> moves)
    {
        foreach (var move in moves)
        {
            var instruction = move.Split(" ");
            var direction = instruction[0];
            var steps = int.Parse(instruction[1]);

            for (var i = 0; i < steps; i++)
            for (var knotNum = 0; knotNum < Knots.Count; knotNum++)
            {
                var knot = Knots[knotNum];
                if (knotNum == 0) knot.Move(direction);
                else knot.Follow(Knots[knotNum - 1]);
            }
        }
    }
}