namespace _2023._1;

public static class Trebuchet
{
    public static int SumCalibrations()
    {
        var file = File.ReadLines(Path.Combine(AppContext.BaseDirectory, "1", "Calibrations.txt"));
        var calibrations = new List<int>();

        foreach (var line in file)
        {
            char? firstDigit = null;
            var lastDigit = '0';

            foreach (var item in line.Where(item => int.TryParse(new ReadOnlySpan<char>(in item), out _)))
            {
                firstDigit = firstDigit ?? item;
                lastDigit = item;
            }
            
            calibrations.Add(int.Parse(firstDigit.ToString()+lastDigit));
        }
        
        return calibrations.Sum();
    }

    public static int SumRealCalibrations()
    {
        var file = File.ReadLines(Path.Combine(AppContext.BaseDirectory, "1", "Calibrations.txt"));
        var calibrations = new List<int>();

        foreach (var line in file)
        {
            var stringIndexes = line.IndexOfAllStringDigits();
            var digitIndexes = line.IndexOfAllNumericalDigits();

            var smallestStringIndex = stringIndexes.Any(s => s > -1) ? stringIndexes.Where(s => s > -1).Min() : int.MaxValue;
            var smallestDigitIndex = digitIndexes.Any(d=> d > -1) ? digitIndexes.Where(d => d > -1).Min() : int.MaxValue;

            var lastStringIndexes = line.LastIndexOfAllStringDigits();
            var lastDigitIndexes = line.LastIndexOfAllNumericalDigits();
            
            var biggestStringIndex = lastStringIndexes.Max();
            var biggestDigitIndex = lastDigitIndexes.Max();

            var firstDigit = smallestStringIndex < smallestDigitIndex ? 
                stringIndexes.IndexOf(smallestStringIndex) + 1 : 
                digitIndexes.IndexOf(smallestDigitIndex) + 1;
            
            var lastDigit = biggestStringIndex > biggestDigitIndex ? 
                lastStringIndexes.LastIndexOf(biggestStringIndex) + 1 : 
                lastDigitIndexes.LastIndexOf(biggestDigitIndex) + 1;
            
            calibrations.Add(int.Parse($"{firstDigit}{lastDigit}"));
        }

        return calibrations.Sum();
    }

    private static List<int> IndexOfAllStringDigits(this string input)
    {
        var indexList = new List<int>
        {
            input.IndexOf("one", StringComparison.Ordinal),
            input.IndexOf("two", StringComparison.Ordinal),
            input.IndexOf("three", StringComparison.Ordinal),
            input.IndexOf("four", StringComparison.Ordinal),
            input.IndexOf("five", StringComparison.Ordinal),
            input.IndexOf("six", StringComparison.Ordinal),
            input.IndexOf("seven", StringComparison.Ordinal),
            input.IndexOf("eight", StringComparison.Ordinal),
            input.IndexOf("nine", StringComparison.Ordinal)
        };

        return indexList;
    }
    
    private static List<int> LastIndexOfAllStringDigits(this string input)
    {
        var indexList = new List<int>
        {
            input.LastIndexOf("one", StringComparison.Ordinal),
            input.LastIndexOf("two", StringComparison.Ordinal),
            input.LastIndexOf("three", StringComparison.Ordinal),
            input.LastIndexOf("four", StringComparison.Ordinal),
            input.LastIndexOf("five", StringComparison.Ordinal),
            input.LastIndexOf("six", StringComparison.Ordinal),
            input.LastIndexOf("seven", StringComparison.Ordinal),
            input.LastIndexOf("eight", StringComparison.Ordinal),
            input.LastIndexOf("nine", StringComparison.Ordinal)
        };

        return indexList;
    }
    
    private static List<int> IndexOfAllNumericalDigits(this string input)
    {
        var indexList = new List<int>
        {
            input.IndexOf('1'),
            input.IndexOf('2'),
            input.IndexOf('3'),
            input.IndexOf('4'),
            input.IndexOf('5'),
            input.IndexOf('6'),
            input.IndexOf('7'),
            input.IndexOf('8'),
            input.IndexOf('9')
        };

        return indexList;
    }
    
    private static List<int> LastIndexOfAllNumericalDigits(this string input)
    {
        var indexList = new List<int>
        {
            input.LastIndexOf('1'),
            input.LastIndexOf('2'),
            input.LastIndexOf('3'),
            input.LastIndexOf('4'),
            input.LastIndexOf('5'),
            input.LastIndexOf('6'),
            input.LastIndexOf('7'),
            input.LastIndexOf('8'),
            input.LastIndexOf('9')
        };

        return indexList;
    }
}