namespace _2023._7;

public static class CamelCards
{
    public static long CalculateTotalWinnings(bool withJokers = false)
    {
        var file = File.ReadLines(Path.Combine(AppContext.BaseDirectory, "7", "HandBids.txt"));
        var handComparer = new HandComparer();
        var handComparerWithJokers = new HandComparerWithJokers();
        var hands = file
            .Select(line => line.Split(" ", StringSplitOptions.RemoveEmptyEntries))
            .Select(item => new Tuple<string, int>(item[0], int.Parse(item[1])))
            .OrderByDescending(x => x.Item1, withJokers ? handComparerWithJokers : handComparer)
            .ToList();

        var totalWinnings = 0L;
        for(var i = 0; i < hands.Count; i++)
        {
            totalWinnings += hands[i].Item2 * (i + 1);
        }

        return totalWinnings;
    }
}

public class HandComparer : Comparer<string>
{
    private static readonly Dictionary<char, int> CardValues = new()
    {
        {'A', 14},
        {'K', 13},
        {'Q', 12},
        {'J', 11},
        {'T', 10},
        {'9', 9},
        {'8', 8},
        {'7', 7},
        {'6', 6},
        {'5', 5},
        {'4', 4},
        {'3', 3},
        {'2', 2}
    };
    
    public override int Compare(string? x, string? y)
    {
        if(x is null || y is null)
            return 0;
        
        var cards = new Dictionary<char, Cards>
        {
            {'A', new Cards()},
            {'K', new Cards()},
            {'Q', new Cards()},
            {'J', new Cards()},
            {'T', new Cards()},
            {'9', new Cards()},
            {'8', new Cards()},
            {'7', new Cards()},
            {'6', new Cards()},
            {'5', new Cards()},
            {'4', new Cards()},
            {'3', new Cards()},
            {'2', new Cards()}
        };
        
        foreach (var card in x)
            cards[card].X++;
        foreach (var card in y)
            cards[card].Y++;

        var cardNumbers = cards.Select(s => s.Value).ToList();
        
        if(cardNumbers.Any(c => c.X == 5) && cardNumbers.Any(c => c.Y == 5))
            return BestKicker(x, y);
        if (cardNumbers.Any(c => c.X == 5))
            return -1;
        if (cardNumbers.Any(c => c.Y == 5))
            return 1;

        if (cardNumbers.Any(c => c.X == 4) && cardNumbers.Any(c => c.Y == 4))
            return BestKicker(x, y);
        if (cardNumbers.Any(c => c.X == 4))
            return -1;
        if (cardNumbers.Any(c => c.Y == 4))
            return 1;
        
        if (cardNumbers.Any(c => c.X == 3) && cardNumbers.Any(c => c.X == 2) && 
            cardNumbers.Any(c => c.Y == 3) && cardNumbers.Any(c => c.Y == 2))
            return BestKicker(x, y);
        if (cardNumbers.Any(c => c.X == 3) && cardNumbers.Any(c => c.X == 2))
            return -1;
        if (cardNumbers.Any(c => c.Y == 3) && cardNumbers.Any(c => c.Y == 2))
            return 1;

        if (cardNumbers.Any(c => c.X == 3) && cardNumbers.Any(c => c.Y == 3))
            return BestKicker(x, y);
        if (cardNumbers.Any(c => c.X == 3))
            return -1;
        if (cardNumbers.Any(c => c.Y == 3))
            return 1;

        var xPair1 = cards.FirstOrDefault(x => x.Value.X == 2);
        var xPair2 = cards.LastOrDefault(x => x.Value.X == 2);
        var yPair1 = cards.FirstOrDefault(x => x.Value.Y == 2);
        var yPair2 = cards.LastOrDefault(x => x.Value.Y == 2);
        if (xPair1.Value is not null && xPair2.Value is not null && 
            yPair1.Value is not null && yPair2.Value is not null &&
            xPair1.Key != xPair2.Key && yPair1.Key != yPair2.Key)
            return BestKicker(x, y);
        if (xPair1.Value is not null && xPair2.Value is not null && xPair1.Key != xPair2.Key)
            return -1;
        if (yPair1.Value is not null && yPair2.Value is not null && yPair1.Key != yPair2.Key)
            return 1;
        
        if (cardNumbers.Any(c => c.X == 2) && cardNumbers.Any(c => c.Y == 2))
            return BestKicker(x, y);
        if (cardNumbers.Any(c => c.X == 2))
            return -1;
        if (cardNumbers.Any(c => c.Y == 2))
            return 1;
        
        return BestKicker(x, y);
    }

    private static int BestKicker(string x, string y)
    {
        for (var i = 0; i < x.Length; i++)
        {
            if (CardValues[x[i]] > CardValues[y[i]])
               return -1;
            if (CardValues[x[i]] < CardValues[y[i]])
                return 1;
        }

        return 0;
    }
}

public class HandComparerWithJokers : Comparer<string>
{
    private static readonly Dictionary<char, int> CardValues = new()
    {
        {'A', 14},
        {'K', 13},
        {'Q', 12},
        {'T', 10},
        {'9', 9},
        {'8', 8},
        {'7', 7},
        {'6', 6},
        {'5', 5},
        {'4', 4},
        {'3', 3},
        {'2', 2},
        {'J', 1}
    };
    
    public override int Compare(string? x, string? y)
    {
        if(x is null || y is null)
            return 0;
        
        var cards = new Dictionary<char, Cards>
        {
            {'A', new Cards()},
            {'K', new Cards()},
            {'Q', new Cards()},
            {'T', new Cards()},
            {'9', new Cards()},
            {'8', new Cards()},
            {'7', new Cards()},
            {'6', new Cards()},
            {'5', new Cards()},
            {'4', new Cards()},
            {'3', new Cards()},
            {'2', new Cards()},
            {'J', new Cards()}
        };
        
        foreach (var card in x)
            cards[card].X++;
        
        foreach (var card in y)
            cards[card].Y++;
        
        HandleJokers(cards);
        
        var cardNumbers = cards.Select(s => s.Value).ToList();
        
        if(cardNumbers.Any(c => c.X == 5) && cardNumbers.Any(c => c.Y == 5))
            return BestKicker(x, y);
        if (cardNumbers.Any(c => c.X == 5))
            return -1;
        if (cardNumbers.Any(c => c.Y == 5))
            return 1;

        if (cardNumbers.Any(c => c.X == 4) && cardNumbers.Any(c => c.Y == 4))
            return BestKicker(x, y);
        if (cardNumbers.Any(c => c.X == 4))
            return -1;
        if (cardNumbers.Any(c => c.Y == 4))
            return 1;
        
        if (cardNumbers.Any(c => c.X == 3) && cardNumbers.Any(c => c.X == 2) && 
            cardNumbers.Any(c => c.Y == 3) && cardNumbers.Any(c => c.Y == 2))
            return BestKicker(x, y);
        if (cardNumbers.Any(c => c.X == 3) && cardNumbers.Any(c => c.X == 2))
            return -1;
        if (cardNumbers.Any(c => c.Y == 3) && cardNumbers.Any(c => c.Y == 2))
            return 1;

        if (cardNumbers.Any(c => c.X == 3) && cardNumbers.Any(c => c.Y == 3))
            return BestKicker(x, y);
        if (cardNumbers.Any(c => c.X == 3))
            return -1;
        if (cardNumbers.Any(c => c.Y == 3))
            return 1;

        var xPair1 = cards.FirstOrDefault(x => x.Value.X == 2);
        var xPair2 = cards.LastOrDefault(x => x.Value.X == 2);
        var yPair1 = cards.FirstOrDefault(x => x.Value.Y == 2);
        var yPair2 = cards.LastOrDefault(x => x.Value.Y == 2);
        if (xPair1.Value is not null && xPair2.Value is not null && 
            yPair1.Value is not null && yPair2.Value is not null &&
            xPair1.Key != xPair2.Key && yPair1.Key != yPair2.Key)
            return BestKicker(x, y);
        if (xPair1.Value is not null && xPair2.Value is not null && xPair1.Key != xPair2.Key)
            return -1;
        if (yPair1.Value is not null && yPair2.Value is not null && yPair1.Key != yPair2.Key)
            return 1;
        
        if (cardNumbers.Any(c => c.X == 2) && cardNumbers.Any(c => c.Y == 2))
            return BestKicker(x, y);
        if (cardNumbers.Any(c => c.X == 2))
            return -1;
        if (cardNumbers.Any(c => c.Y == 2))
            return 1;
        
        return BestKicker(x, y);
    }
    
    private static int BestKicker(string x, string y)
    {
        for (var i = 0; i < x.Length; i++)
        {
            if (CardValues[x[i]] > CardValues[y[i]])
                return -1;
            if (CardValues[x[i]] < CardValues[y[i]])
                return 1;
        }

        return 0;
    }

    private static void HandleJokers(IReadOnlyDictionary<char, Cards> cards)
    {
        if(cards['J'].X is 0 && cards['J'].Y is 0)
            return;

        var bestCardX = cards
            .Where(card => card.Key is not 'J')
            .MaxBy(card => card.Value.X).Key;
        var jokersX = cards['J'].X;
        
        var bestCardY = cards
            .Where(card => card.Key is not 'J')
            .MaxBy(card => card.Value.Y).Key;
        var jokersY = cards['J'].Y;
        
        cards[bestCardX].X += jokersX;
        cards[bestCardY].Y += jokersY;
        
        cards['J'].X = 0;
        cards['J'].Y = 0;
    }
}

public class Cards
{
    public int X { get; set; }
    public int Y { get; set; }
}