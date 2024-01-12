namespace _2023._6;

public static class WaitForIt
{
    public static int GetNumberOfWaysToBeatRecord()
    {
        var file = File.ReadLines(Path.Combine(AppContext.BaseDirectory, "6", "Timetable.txt")).ToList();
        var times = new List<int>();
        var distances = new List<int>();
        var records = new List<int>();

        var timeString = file.First().Split(" ", StringSplitOptions.RemoveEmptyEntries);
        var distanceString = file.Last().Split(" ", StringSplitOptions.RemoveEmptyEntries);


        for (var i = 1; i < timeString.Length; i++)
            times.Add(int.Parse(timeString[i]));
        
        for (var i = 1; i < distanceString.Length; i++)
            distances.Add(int.Parse(distanceString[i]));
        
        for(var i = 0; i < times.Count; i++)
        {
            var numberOfWaysToBeatRecord = 0;
            for (var ii = 1; ii < times[i] - 1; ii++)
            {
                if (ii * (times[i] - ii) > distances[i])
                    numberOfWaysToBeatRecord++;
            }
            records.Add(numberOfWaysToBeatRecord);
        }

        return records.Aggregate(1, (current, record) => current * record);
    }

    public static int GetNumberOfWaysToBeatBigRecord()
    {
        var file = File.ReadLines(Path.Combine(AppContext.BaseDirectory, "6", "Timetable.txt")).ToList();
        var timeString = file.First().Split(" ", StringSplitOptions.RemoveEmptyEntries);
        var distanceString = file.Last().Split(" ", StringSplitOptions.RemoveEmptyEntries);
        
        var time = long.Parse(timeString[1] + timeString[2] + timeString[3] + timeString[4]);
        var distance = long.Parse(distanceString[1] + distanceString[2] + distanceString[3] + distanceString[4]);
        
        var numberOfWaysToBeatRecord = 0;
        for (var ii = 1; ii < time - 1; ii++)
        {
            if (ii * (time - ii) > distance)
                numberOfWaysToBeatRecord++;
        }

        return numberOfWaysToBeatRecord;
    }
}