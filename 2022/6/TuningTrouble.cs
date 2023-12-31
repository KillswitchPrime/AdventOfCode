namespace _2022._6;

public static class TuningTrouble
{
    public static long DetectMarkerCharacters(int marker = 4)
    {
        var file = File.ReadLines(Path.Combine(AppContext.BaseDirectory, "6", "DatastreamBuffer.txt"));

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