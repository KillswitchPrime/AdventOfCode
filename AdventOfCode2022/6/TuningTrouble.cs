namespace AdventOfCode2022._6;

public static class TuningTrouble
{
    public static long DetectMarkerCharacters(int marker = 4)
    {
        var file = File.ReadLines("C:\\Users\\ChristopherIshaqueJa\\RiderProjects\\AdventOfCode2022\\AdventOfCode2022\\6\\DatastreamBuffer.txt");

        var markerCharacters = 0L;
        var bufferList = new List<char>();
        foreach (var character in file.First())
        {
            markerCharacters++;
            if (bufferList.Contains(character))
            {
                bufferList.Clear();
            }
            
            bufferList.Add(character);
            if (bufferList.Count >= marker)
                break;
        }
        
        return markerCharacters;
    }
}