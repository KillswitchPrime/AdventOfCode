namespace _2022._11;

public class Monkey
{
    public List<long> Items { get; set; }
    public long Inspections { get; set; }
    public long WorryMultiplier { get; set; }
    public string Operator { get; set; }
    public long Divisible { get; set; }
    public Monkey Left { get; set; }
    public Monkey Right { get; set; }

    private long Operation(long item)
    {
        return Operator switch
        {
            "*" => item * WorryMultiplier,
            "+" => item + WorryMultiplier,
            "¤" => item * item,
            _ => item
        };
    }

    private Monkey Test(long worry)
    {
        return worry % Divisible == 0 ? Left : Right;
    }

    public void CompleteTurn(long factor = 0, long worryRelief = 3)
    {
        Inspections += Items.Count;
        
        foreach (var item in Items)
        {
            var worryItem = factor > 0 ? Operation(item) % factor : Operation(item) / worryRelief;
            var monkey = Test(worryItem);
            monkey.Items.Add(worryItem);
        }
        
        Items.Clear();
    }
}