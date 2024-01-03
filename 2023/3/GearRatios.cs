using System.Text.RegularExpressions;

namespace _2023._3;

public static partial class GearRatios
{
    public static Tuple<int, int> SumPartNumbers()
    {
        var file = File.ReadLines(Path.Combine(AppContext.BaseDirectory, "3", "EngineSchematic.txt")).ToList();
        var lineLength = file[0].Length;

        var symbolsRegex = SymbolsRegex();
        var starRegex = StarRegex();
        var numbersRegex = NumbersRegex();
        
        var potentialGears = new Dictionary<string, List<int>>();
        var validPartNumbers = new List<int>();

        // Get all numbers, along with their line number, start index and end index relative to the line that they are on.
        var numbers = file.SelectMany(
                (line, lineNumber) => numbersRegex.Matches(line)
                    .Select(x => new
                    {
                        Value = int.Parse(x.Value),
                        LineNumber = lineNumber,
                        StartIndex = x.Index, x.Length,
                        EndIndex = x.Index + x.Length
                    }));

        foreach (var number in numbers)
        {
            // Build line scan range
            var start = Math.Clamp(number.StartIndex - 1, 0, lineLength);
            var end = Math.Clamp(number.EndIndex + 1, 0, lineLength);
            var length = end - start;

            var lineNumbersToCheck = new List<int>(3) { number.LineNumber };
            if (number.LineNumber > 0) lineNumbersToCheck.Add(number.LineNumber - 1);
            if (number.LineNumber + 1 < file.Count) lineNumbersToCheck.Add(number.LineNumber + 1);

            foreach (var lineNumber in lineNumbersToCheck)
            {
                var range = file[lineNumber].Substring(start, length);
                if (symbolsRegex.Match(range).Success)
                {
                    validPartNumbers.Add(number.Value);
                }

                var starMatch = starRegex.Match(range);
                if (!starMatch.Success) 
                    continue;
                
                var potentialGearKey = $"{start + starMatch.Index},{lineNumber}";
                if (potentialGears.TryGetValue(potentialGearKey, out var value))
                {
                    value.Add(number.Value);
                }
                else
                {
                    potentialGears[potentialGearKey] = [number.Value];
                }
            }
        }
        
        var gears = potentialGears
            .Where(x => x.Value.Count == 2)
            .Select(x => x.Value);
        
        return new Tuple<int, int>(validPartNumbers.Sum(), gears.Sum(x => x[0] * x[1]));
    }

    [GeneratedRegex("[^0-9.\r\n]")]
    private static partial Regex SymbolsRegex();
    [GeneratedRegex("[*]")]
    private static partial Regex StarRegex();
    [GeneratedRegex(@"\d+")]
    private static partial Regex NumbersRegex();
}