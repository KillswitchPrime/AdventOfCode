namespace AdventOfCode2022._10;

public static class CathodeRayTube
{
    public static long GetSumSignalStrengths()
    {
        var file = File.ReadLines(
            "C:\\Users\\ChristopherIshaqueJa\\RiderProjects\\AdventOfCode2022\\AdventOfCode2022\\10\\ClockInstructions.txt");

        var signalStrengthSum = 0L;
        var register = 1;
        var cycle = 0;
        var loopIndex = 0;
        
        foreach (var line in file)
        {
            var instructions = line.Split(" ");

            switch (instructions[0])
            {
                case "noop":
                    cycle++;
                    break;
                case "addx":
                    cycle += 2;
                    break;
            }

            while (loopIndex < cycle)
            {
                loopIndex++;
                if ((loopIndex - 20) % 40 == 0)
                    signalStrengthSum += loopIndex * register;
            }
            
            if(instructions.Length > 1)
                register += int.Parse(instructions[1]);
        }

        return signalStrengthSum;
    }

    public static void DrawCRTScreen()
    {
        var file = File.ReadLines(
            "C:\\Users\\ChristopherIshaqueJa\\RiderProjects\\AdventOfCode2022\\AdventOfCode2022\\10\\ClockInstructions.txt");

        var register = 1;
        var cycle = 0;
        var loopIndex = 0;
        var crtDrawer = 0;
        
        foreach (var line in file)
        {
            var instructions = line.Split(" ");

            switch (instructions[0])
            {
                case "noop":
                    cycle++;
                    break;
                case "addx":
                    cycle += 2;
                    break;
            }

            while (loopIndex < cycle)
            {
                loopIndex++;
                
                if(crtDrawer == register || crtDrawer == register -1 || crtDrawer == register + 1)
                    Console.Write("#");
                else
                    Console.Write(".");
                
                crtDrawer++;

                if (crtDrawer < 40) continue;
                
                crtDrawer = 0;
                Console.WriteLine();
            }
            
            if(instructions.Length > 1)
                register += int.Parse(instructions[1]);
        }
    }
}