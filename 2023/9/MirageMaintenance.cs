namespace _2023._9;

public static class MirageMaintenance
{
    public static int ExtrapolateHistorySum()
    {
        var file = File.ReadLines(Path.Combine(AppContext.BaseDirectory, "9", "History.txt")).ToList();
        var histories = file.Select(x => x.Split(" ").Select(int.Parse).ToArray()).ToArray();
        var historySum = 0;

        foreach (var history in histories)
        {
            var sequences = new List<Sequence>();
            var currentNumber = int.MaxValue;
            while (currentNumber > 0)
            {
                currentNumber = history[^1] - history[^2];
                
                var sequence = new Sequence([currentNumber]);
                sequences.Add(sequence);
            }

        }

        return historySum;
    }
}

public class Sequence(List<int> numbers)
{
    public List<int> Numbers { get; set; } = numbers;
}