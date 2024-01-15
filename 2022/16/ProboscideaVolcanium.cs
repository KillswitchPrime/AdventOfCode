namespace _2022._16;

public static class ProboscideaVolcanium
{
    public static long MaximizePressureRelease()
    {
        var file = File.ReadLines(Path.Combine(AppContext.BaseDirectory, "16", "PipeValves.txt"));

        var valves = new Dictionary<string, Valve>();
        var tunnels = new Dictionary<string, List<string>>();
        foreach (var line in file)
        {
            var data = line.Split("; ");
            var valveData = data[0].Split(" ");
            var valveName = valveData[1];
            var flow = long.Parse(valveData[4].Split("=")[1]);
            var neighbours = data[1].Split(" ");
            tunnels.Add(valveName, neighbours.Length > 4 ? neighbours.Skip(4).Select(s => s.Split(",")[0]).ToList() : new List<string>{neighbours[4]} );
            
            valves.Add(valveName, new Valve
            {
                Name = valveName,
                FlowRate = flow,
                Neighbours = new List<Valve>(),
                IsOpen = false,
                G = 0,
                H = 0
            });
        }

        foreach (var (valveName, neighbours) in tunnels)
        {
            foreach (var neighbour in neighbours)
            {
                valves[valveName].Neighbours?.Add(valves[neighbour]);
            }
        }

        var currentValve = valves["AA"];
        var totalPressure = 0L;
        var currentPressure = 0L;
        var openValve = false;
        var paths = new Queue<Valve>();
        for (var time = 0; time < 30; time++)
        {
            if (openValve)
            {
                openValve = false;
                currentValve.IsOpen = true;
                totalPressure += currentPressure;
                currentPressure += currentValve.FlowRate;
                continue;
            }
            
            if (paths.Any() is false)
            {
                var valvePaths = FindPath(currentValve, valves.Values.Skip(1).ToList());
                foreach(var valve in valvePaths)
                    paths.Enqueue(valve);
            }
            
            currentValve = paths.Dequeue();

            if (paths.Any() is false)
                openValve = true;
            
            totalPressure += currentPressure;
        }

        return totalPressure;
    }

    private static List<Valve> FindPath(Valve start, List<Valve> valves)
    {
        // Create the open and closed lists
        var openList = new List<Valve>();
        var closedList = new HashSet<Valve>();

        // Add the start valve to the open list
        openList.Add(start);

        while (openList.Count > 0)
        {
            // Get the valve with the highest F value
            var currentValve = openList[0];
            for (var i = 1; i < openList.Count; i++)
            {
                if (openList[i].F > currentValve.F && openList[i].IsOpen is false)
                {
                    currentValve = openList[i];
                }
            }

            // Remove the current valve from the open list and add it to the closed list
            openList.Remove(currentValve);
            closedList.Add(currentValve);

            // If this is the valve with the highest flow rate, return the path
            if (currentValve.FlowRate == valves.Where(v => v.IsOpen is false).Max(v => v.FlowRate))
            {
                return GetPath(currentValve);
            }

            // Get the adjacent valves
            var adjacentValves = currentValve.Neighbours;
            if (adjacentValves is null) continue;
            
            foreach (var adjacentValve in
                     adjacentValves.Where(adjacentValve => !closedList.Contains(adjacentValve)))
            {
                adjacentValve.Parent = currentValve;
                // If the valve is in the closed list or has already been opened, ignore it
                if (closedList.Contains(adjacentValve)) // MODIFIED LINE
                {
                    continue;
                }

                // If the valve is not in the open list, add it
                if (!openList.Contains(adjacentValve))
                {
                    openList.Add(adjacentValve);
                }
                else
                {
                    // If we have found a better route to the valve, update its parent
                    // and G and F values
                    if (currentValve.G + 1 <= adjacentValve.G)
                        continue;

                    adjacentValve.G = currentValve.G + 1;
                    adjacentValve.H = valves.Max(v => v.FlowRate) - adjacentValve.FlowRate;
                }
            }
        }

        // Return an empty path if no path was found
        return [];
    }
    
    private static List<Valve> GetPath(Valve valve)
    {
        var path = new List<Valve>();

        // Start at the end valve and work backwards to the start valve
        var currentValve = valve;
        while (currentValve != null)
        {
            path.Add(currentValve);
            currentValve = currentValve.Parent;
        }

        path.Reverse();
        return path;
    }
}