namespace AdventOfCode2022._11;

public static class MonkeyInTheMiddle
{
    public static long CalculateMonkeyBusiness()
    {
        var monkeys = CreateMonkeys();

        for (var i = 0; i < 20; i++)
        {
            foreach (var monkey in monkeys)
            {
                monkey.CompleteTurn();
            }
        }

        var mostActiveMonkeys = monkeys.Select(m => m.Inspections).OrderDescending().ToList();
        return mostActiveMonkeys[0] * mostActiveMonkeys[1];
    }
    
    public static long CalculateExtremeMonkeyBusiness()
    {
        var monkeys = CreateMonkeys();
        var factor = monkeys.Aggregate(1L, (factor, monkey) => factor * monkey.Divisible);

        for (var i = 0; i < 10_000; i++)
        {
            foreach (var monkey in monkeys)
            {
                monkey.CompleteTurn(factor);
            }
        }

        var mostActiveMonkeys = monkeys.Select(m => m.Inspections).OrderDescending().ToList();
        return mostActiveMonkeys[0] * mostActiveMonkeys[1];
    }

    private static List<Monkey> CreateMonkeys()
    {
        var file = File.ReadLines(
            "C:\\Users\\ChristopherIshaqueJa\\RiderProjects\\AdventOfCode2022\\AdventOfCode2022\\11\\SimianShenanigans.txt");

        var monkeys = new List<Monkey>();
        var monkeyNeighbours = new List<string>();
        foreach (var line in file.Select(l => l.Trim()))
        {
            if (line.StartsWith("Monkey"))
            {
                monkeys.Add(new Monkey());
                monkeyNeighbours.Add("");
            }
            else if (line.StartsWith("Starting"))
            {
                var items = line[16..].Split(", ").Select(long.Parse);
                monkeys.Last().Items = items.ToList();
            }
            else if (line.StartsWith("Operation"))
            {
                var didParse = long.TryParse(line[23..], out var worryMultiplier);
                monkeys.Last().WorryMultiplier = didParse ? worryMultiplier : 0;
                monkeys.Last().Operator = didParse ? line[21].ToString() : "¤";
            }
            else if (line.StartsWith("Test"))
            {
                var divisible = long.Parse(line[19..]);
                monkeys.Last().Divisible = divisible;
            }
            else if (line.Contains("true"))
            {
                var monkey = line[25..];
                monkeyNeighbours[^1] += monkey;
            }
            else if (line.Contains("false"))
            {
                var monkey = line[26..];
                monkeyNeighbours[^1] += $";{monkey}";
            }
        }
        
        for (var i = 0; i < monkeyNeighbours.Count; i++)
        {
            monkeys[i].Left = monkeys[int.Parse(monkeyNeighbours[i].Split(";")[0])];
            monkeys[i].Right = monkeys[int.Parse(monkeyNeighbours[i].Split(";")[1])];
        }

        return monkeys;
    }
}