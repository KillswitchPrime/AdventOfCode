using System.Text.Json.Nodes;

namespace _2022._13;

public static class DistressSignal
{
    public static long CheckPacketPairs()
    {
        var packets = CreatePacketPairs();

        var correctPairs = 0L;
        foreach (var packet in packets)
        {
            var indexLeft = 0;
            var indexRight = 0;

            while (indexLeft < packet.Left.Length && indexRight < packet.Right.Length)
            {
                while (packet.Left[indexLeft] is '[' or ',' && indexLeft < packet.Left.Length)
                    indexLeft++;
                while(packet.Right[indexRight] is '[' or ',' && indexRight < packet.Right.Length)
                    indexRight++;

                if (packet.Left[indexLeft] != ']' && packet.Right[indexRight] == ']')
                    break;

                if (packet.Left[indexLeft] == ']' && packet.Right[indexRight] != ']')
                {
                    correctPairs += packet.Index;
                    break;
                }
                
                if (packet.Left[indexLeft] == ']' && packet.Right[indexRight] == ']')
                {
                    if (indexLeft == indexRight)
                    {
                        indexLeft++;
                        indexRight++;
                        continue;
                    }

                    if (indexLeft < indexRight)
                    {
                        correctPairs += packet.Index;
                        break;
                    }

                    if (indexLeft > indexRight)
                        break;
                }

                var left = "";
                while (packet.Left[indexLeft] != ',' && packet.Left[indexLeft] != ']' && indexLeft < packet.Left.Length)
                {
                    left += packet.Left[indexLeft].ToString();
                    indexLeft++;
                }
                
                var right = "";
                while (packet.Right[indexRight] != ',' && packet.Right[indexRight] != ']' && indexRight < packet.Right.Length)
                {
                    right += packet.Right[indexRight].ToString();
                    indexRight++;
                }

                var leftInt = int.Parse(left);
                var rightInt = int.Parse(right);

                if (leftInt == rightInt)
                {
                    if (indexLeft == indexRight)
                    {
                        continue;
                    }
                    
                    if (indexLeft < indexRight)
                    {
                        correctPairs += packet.Index;
                        break;
                    }

                    if (indexLeft > indexRight)
                        break;
                }
                
                if(leftInt < rightInt)
                {
                    correctPairs += packet.Index;
                    break;
                }

                if(leftInt > rightInt)
                    break;
            }

        }

        return correctPairs;
    }
    
    private static List<PacketPair> CreatePacketPairs()
    {
        var file = File.ReadLines(
            "C:\\Users\\ChristopherIshaqueJa\\RiderProjects\\AdventOfCode2022\\AdventOfCode2022\\13\\PacketPairs.txt");

        var packets = new List<PacketPair>();

        var index = 1;
        foreach (var line in file.Where(l => string.IsNullOrWhiteSpace(l) is false).Chunk(2))
        {
            packets.Add(new PacketPair(line[0], line[1], index));
            index++;
        }

        return packets;
    }

    public static long GetDecoderKey()
    {
        var packets = CreatePacketList();

        var decoder = new List<JsonNode>
        {
            JsonNode.Parse("[[2]]"),
            JsonNode.Parse("[[6]]")
        };

        var jsonPackets = packets.Select(p => JsonNode.Parse(p)).ToList();
        jsonPackets.AddRange(decoder);
        jsonPackets.Sort(Compare);
        
        return (jsonPackets.IndexOf(decoder[0]) + 1) * (jsonPackets.IndexOf(decoder[1]) + 1);
    }

    private static int Compare(JsonNode? nodeA, JsonNode? nodeB)
    {
        if (nodeA is JsonValue && nodeB is JsonValue) {
            return (int)nodeA - (int)nodeB;
        }

        // It's AoC time, let's exploit FirstOrDefault! 
        // 😈 if all items are equal, compare the length of the arrays 
        var arrayA = nodeA as JsonArray ?? new JsonArray((int)nodeA);
        var arrayB = nodeB as JsonArray ?? new JsonArray((int)nodeB);
        return arrayA.Zip(arrayB)
            .Select(p => Compare(p.First, p.Second))
            .FirstOrDefault(c => c != 0, arrayA.Count - arrayB.Count);
    }
    
    private static List<string> CreatePacketList()
    {
        var file = File.ReadLines(
            "C:\\Users\\ChristopherIshaqueJa\\RiderProjects\\AdventOfCode2022\\AdventOfCode2022\\13\\PacketPairs.txt");

        return file.Where(l => string.IsNullOrWhiteSpace(l) is false).ToList();
    }
}