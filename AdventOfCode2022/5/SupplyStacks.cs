namespace AdventOfCode2022._5;

public static class SupplyStacks
{
    public static string CalculateCargoStack()
    {
        var (cargoInstructions, cargoStacks) = GenerateCargoStacks();
        
        foreach (var instruction in cargoInstructions)
        {
            var instructionSplit = instruction.Split(" ");
            
            var cargoToMove = int.Parse(instructionSplit[1]);
            var stackToTake = int.Parse(instructionSplit[3]) - 1;
            var stackToReceive = int.Parse(instructionSplit[5]) - 1;

            for (var i = 0; i < cargoToMove; i++)
            {
                cargoStacks[stackToReceive].Push(cargoStacks[stackToTake].Pop());
            }
            
        }

        return cargoStacks.Values.Aggregate("", (current, stack) => current + stack.Pop());
    }

    public static string CrateMover9001()
    {
        var (cargoInstructions, cargoStacks) = GenerateCargoStacks();
        
        foreach (var instruction in cargoInstructions)
        {
            var instructionSplit = instruction.Split(" ");
            
            var cargoToMove = int.Parse(instructionSplit[1]);
            var stackToTake = int.Parse(instructionSplit[3]) - 1;
            var stackToReceive = int.Parse(instructionSplit[5]) - 1;

            var cargoList = new List<string>();
            for (var i = 0; i < cargoToMove; i++)
            {
                cargoList.Add(cargoStacks[stackToTake].Pop());
            }

            cargoList.Reverse();
            cargoList.ForEach(s => cargoStacks[stackToReceive].Push(s));
        }

        return cargoStacks.Values.Aggregate("", (current, stack) => current + stack.Pop());
    }

    private static Tuple<List<string>, Dictionary<int, Stack<string>>> GenerateCargoStacks()
    {
        var file = File.ReadLines("C:\\Users\\ChristopherIshaqueJa\\RiderProjects\\AdventOfCode2022\\AdventOfCode2022\\5\\CargoCrates.txt").ToList();
        var initialCargoState = file.Take(file.FindIndex(string.IsNullOrWhiteSpace)).ToList();
        var cargoInstructions = file.Skip(file.FindIndex(string.IsNullOrWhiteSpace) + 1).ToList();

        var numberOfStacks = initialCargoState[^1].Select(x => x.ToString()).ToList();

        var totalStacks = int.Parse(initialCargoState[^1].Trim().Split(" ").ToList()[^1]);

        var cargoDictionary = new Dictionary<int, Stack<string>>();

        for (var i = 0; i < totalStacks; i++)
        {
            cargoDictionary.Add(i, new Stack<string>());
        }
        
        for(var i = 0; i < initialCargoState.Count - 1; i++)
        {
            var index = 0;
            for(var j = 1; j < initialCargoState[i].Length; j++)
            {
                if (j % 4 == 0)
                    index++;
                
                if(char.IsLetter(initialCargoState[i][j]) && string.IsNullOrWhiteSpace(numberOfStacks[j]) is false)
                    cargoDictionary[index].Push(initialCargoState[i][j].ToString());
            }
        }

        var cargoStacks = new Dictionary<int, Stack<string>>();
        foreach (var (key, stack) in cargoDictionary)
        {
            cargoStacks.Add(key, new Stack<string>());
            while(stack.Count > 0)
                cargoStacks[key].Push(stack.Pop());
        }

        return new Tuple<List<string>, Dictionary<int, Stack<string>>>(cargoInstructions, cargoStacks);
    }
}