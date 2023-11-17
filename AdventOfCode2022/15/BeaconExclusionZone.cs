using System.Drawing;

namespace AdventOfCode2022._15;

public static class BeaconExclusionZone
{
    public static long CountIneligibleBeaconPositions()
    {
        const int row = 2_000_000;

        var sensorReport = GetSensorReport();

        // Finding the number of coordinates covered by sensor signals.
        var signals = new HashSet<Point>();
        foreach (var (sensor, beacon) in sensorReport)
        {
            // Find the distance between the beacons and sensors x coordinates.
            var xDistance = sensor.X > beacon.X ? 
                sensor.X - beacon.X : 
                beacon.X - sensor.X;
            
            // Find the distance between the beacons and sensors y coordinates.
            var yDistance = sensor.Y > beacon.Y ? 
                sensor.Y - beacon.Y : 
                beacon.Y - sensor.Y;

            // Find the maximum radius of the sensor's signal area for its closest beacon.
            var signalRadius = xDistance + yDistance;

            // If y = 2_000_000 is not inside the signal area, continue.
            if (row > signalRadius + sensor.Y && 
                row < sensor.Y - signalRadius)
                continue;

            // Find the distance between y = 2_000_000 and the signal area radius.
            var rowDistance = row > sensor.Y ? 
                (sensor.Y + signalRadius) - row : 
                row - (sensor.Y - signalRadius);

            // Loop through every x coordinate for point that is inside the signal area, and has y = 2_000_000.
            for (var x = sensor.X - rowDistance; x < sensor.X + rowDistance; x++)
                signals.Add(new Point(x, row));
        }

        return signals.Count;
    }

    public static long FindDistressSignalLocation()
    {
        var sensorReport = GetSensorReport();

        // Finding the number of coordinates covered by sensor signals.
        var distressBeaconCandidates = new HashSet<BeaconPoint>();
        foreach (var (sensor, beacon) in sensorReport)
        {
            // Find the distance between the beacons and sensors x coordinates.
            var xDistance = sensor.X > beacon.X ? 
                sensor.X - beacon.X : 
                beacon.X - sensor.X;
            
            // Find the distance between the beacons and sensors y coordinates.
            var yDistance = sensor.Y > beacon.Y ? 
                sensor.Y - beacon.Y : 
                beacon.Y - sensor.Y;

            // Find the maximum radius of the sensor's signal area for its closest beacon.
            var signalRadius = xDistance + yDistance;
            
            // Find the line just outside of every sensor area (signalRadius + 1)
            // Find the intersection of all sensor area lines. The point that is intersected in some way will be the distress beacon.
            // line function: y = ax + b
            // intersect: f(x) = g(x)
        }
        
        return distressBeaconCandidates.First().X * 4_000_000L + distressBeaconCandidates.First().Y;
    }

    private static Dictionary<Point, Point> GetSensorReport()
    {
        var file = File.ReadLines(
            "C:\\Users\\ChristopherIshaqueJa\\RiderProjects\\AdventOfCode2022\\AdventOfCode2022\\15\\SensorData.txt");

        var sensorReport = new Dictionary<Point, Point>();

        // Parsing the input.
        foreach (var line in file)
        {
            var splitData = line.Split(": ");
            var sensorLine = splitData[0].Split(" ");
            var beaconLine = splitData[1].Split(" ");
            var sensorX = int.Parse(sensorLine[2].Split("x=")[1].Split(",")[0]);
            var sensorY = int.Parse(sensorLine[3].Split("y=")[1]);

            var beaconX = int.Parse(beaconLine[4].Split("x=")[1].Split(",")[0]);
            var beaconY = int.Parse(beaconLine[5].Split("y=")[1]);

            sensorReport.Add(new Point(sensorX, sensorY), new Point(beaconX, beaconY));
        }

        return sensorReport;
    }
}