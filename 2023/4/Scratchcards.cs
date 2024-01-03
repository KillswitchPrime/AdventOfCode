namespace _2023._4;

public static class Scratchcards
{
    public static int SumScratchcards()
    {
        var file = File.ReadLines(Path.Combine(AppContext.BaseDirectory, "4", "Scratchcards.txt"));
        var scratchcards = new List<int>();

        foreach (var line in file)
        {
            var numbers = line.Split([':', '|']);
            var winningNumbers = numbers[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var scratchcardNumbers = numbers[2].Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var points = 0;
            foreach (var number in scratchcardNumbers)
            {
                if (winningNumbers.Contains(number) is false) 
                    continue;
                
                if (points == 0)
                    points++;
                else
                    points *= 2;
            }
            
            scratchcards.Add(points);
        }

        return scratchcards.Sum();
    }

    public static int SumTotalNumberOfScratchcards()
    {
        var file = File.ReadLines(Path.Combine(AppContext.BaseDirectory, "4", "Scratchcards.txt"));
        var scratchcards = new List<Scratchcard>();

        foreach (var line in file)
        {
            var numbers = line.Split([':', '|']);
            var cardId = int.Parse(numbers[0].Split("Card", StringSplitOptions.RemoveEmptyEntries)[0]);
            var winningNumbers = numbers[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var scratchcardNumbers = numbers[2].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            
            var points = scratchcardNumbers.Count(number => winningNumbers.Contains(number));

            scratchcards.Add(new Scratchcard(cardId, points));
        }

        foreach (var card in scratchcards)
            for (var i = card.Id; i < card.Id + card.Points; i++)
                scratchcards[i].Copies += card.Copies;

        return scratchcards.Sum(s => s.Copies);
    }
    
    private class Scratchcard(int id, int points)
    {
        public int Id { get; set; } = id;
        public int Copies { get; set; } = 1;
        public int Points { get; set; } = points;
    }
}