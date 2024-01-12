namespace _2023._5;

public static class IfYouGiveASeedAFertilizer
{
    private static bool foundLowestLocation;
    private static long currentLocation;
    public static long LowestLocationNumberOfSeed(bool isPart2 = false)
    {
        var file = File.ReadLines(Path.Combine(AppContext.BaseDirectory, "5", "Almanac.txt"));
        var seeds = new List<long>();
        var soilMaps = new List<Tuple<long,long,long>>();
        var fertilizersMaps = new List<Tuple<long,long,long>>();
        var waterMaps = new List<Tuple<long,long,long>>();
        var lightMaps = new List<Tuple<long,long,long>>();
        var temperatureMaps = new List<Tuple<long,long,long>>();
        var humidityMaps = new List<Tuple<long,long,long>>();
        var locationMaps = new List<Tuple<long,long,long>>();
        var lowestLocation = long.MaxValue;

        var currentMap = Map.Seeds;
        foreach (var line in file)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            if (line.StartsWith("seed-"))
            {
                currentMap = Map.Soil;
                continue;
            }
            if (line.StartsWith("soil"))
            {
                currentMap = Map.Fertilizers;
                continue;
            }
            if (line.StartsWith("fertilizer"))
            {
                currentMap = Map.Water;
                continue;
            }
            if (line.StartsWith("water"))
            {
                currentMap = Map.Light;
                continue;
            }
            if (line.StartsWith("light"))
            {
                currentMap = Map.Temperature;
                continue;
            }
            if (line.StartsWith("temperature"))
            {
                currentMap = Map.Humidity;
                continue;
            }
            if (line.StartsWith("humidity"))
            {
                currentMap = Map.Location;
                continue;
            }
            
            switch (currentMap)
            {
                case Map.Seeds:
                    seeds.AddRange(line.Split(["seeds:", " "], StringSplitOptions.RemoveEmptyEntries).Select(long.Parse));
                    break;
                case Map.Soil:
                    var soils = line.Split(" ").Select(long.Parse).ToList();
                    soilMaps.Add(new Tuple<long, long, long>(soils[0], soils[1], soils[2]));
                    break;
                case Map.Fertilizers:
                    var fertilizers = line.Split(" ").Select(long.Parse).ToList();
                    fertilizersMaps.Add(new Tuple<long, long, long>(fertilizers[0], fertilizers[1], fertilizers[2]));
                    break;
                case Map.Water:
                    var waters = line.Split(" ").Select(long.Parse).ToList();
                    waterMaps.Add(new Tuple<long, long, long>(waters[0], waters[1], waters[2]));
                    break;
                case Map.Light:
                    var lights = line.Split(" ").Select(long.Parse).ToList();
                    lightMaps.Add(new Tuple<long, long, long>(lights[0], lights[1], lights[2]));
                    break;
                case Map.Temperature:
                    var temperatures = line.Split(" ").Select(long.Parse).ToList();
                    temperatureMaps.Add(new Tuple<long, long, long>(temperatures[0], temperatures[1], temperatures[2]));
                    break;
                case Map.Humidity:
                    var humidities = line.Split(" ").Select(long.Parse).ToList();
                    humidityMaps.Add(new Tuple<long, long, long>(humidities[0], humidities[1], humidities[2]));
                    break;
                case Map.Location:
                    var locations = line.Split(" ").Select(long.Parse).ToList();
                    locationMaps.Add(new Tuple<long, long, long>(locations[0], locations[1], locations[2]));
                    break;
            }
        }
        soilMaps.Sort();
        fertilizersMaps.Sort();
        waterMaps.Sort();
        lightMaps.Sort();
        temperatureMaps.Sort();
        humidityMaps.Sort();
        locationMaps.Sort();

        if (isPart2)
        {
            var currentLocations = GetCurrentLocation();
            var location = GetMappedSeedReverse(currentLocations, locationMaps);
            var humidity = GetMappedSeedReverse(location, humidityMaps);
            var temperature = GetMappedSeedReverse(humidity, temperatureMaps);
            var light = GetMappedSeedReverse(temperature, lightMaps);
            var water = GetMappedSeedReverse(light, waterMaps);
            var fertilizer = GetMappedSeedReverse(water, fertilizersMaps);
            var soil = GetMappedSeedReverse(fertilizer, soilMaps);

            foreach (var seed in soil)
            {
                for (var i = 0; i < seeds.Count; i += 2)
                {
                    if(seed < seeds[i] || seed > seeds[i] + seeds[i + 1])
                        continue;
                    
                    foundLowestLocation = true;
                    return currentLocation;
                }
            }
        }
        else
        {
            foreach (var seed in seeds)
            {
                var seedSoil = seed;
                foreach (var soilMap in soilMaps)
                {
                    seedSoil = GetMappedValue(seed, soilMap.Item1, soilMap.Item2, soilMap.Item3);
                    if (seedSoil != seed)
                        break;
                }

                var seedFertilizer = seedSoil;
                foreach (var fertilizerMap in fertilizersMaps)
                {
                    seedFertilizer = GetMappedValue(seedFertilizer, fertilizerMap.Item1, fertilizerMap.Item2,
                        fertilizerMap.Item3);
                    if (seedFertilizer != seedSoil)
                        break;
                }

                var seedWater = seedFertilizer;
                foreach (var waterMap in waterMaps)
                {
                    seedWater = GetMappedValue(seedWater, waterMap.Item1, waterMap.Item2, waterMap.Item3);
                    if (seedWater != seedFertilizer)
                        break;
                }

                var seedLight = seedWater;
                foreach (var lightMap in lightMaps)
                {
                    seedLight = GetMappedValue(seedLight, lightMap.Item1, lightMap.Item2, lightMap.Item3);
                    if (seedLight != seedWater)
                        break;
                }

                var seedTemperature = seedLight;
                foreach (var temperatureMap in temperatureMaps)
                {
                    seedTemperature = GetMappedValue(seedTemperature, temperatureMap.Item1, temperatureMap.Item2,
                        temperatureMap.Item3);
                    if (seedTemperature != seedLight)
                        break;
                }

                var seedHumidity = seedTemperature;
                foreach (var humidityMap in humidityMaps)
                {
                    seedHumidity = GetMappedValue(seedHumidity, humidityMap.Item1, humidityMap.Item2, humidityMap.Item3);
                    if (seedHumidity != seedTemperature)
                        break;
                }

                var seedLocation = seedHumidity;
                foreach (var locationMap in locationMaps)
                {
                    seedLocation = GetMappedValue(seedLocation, locationMap.Item1, locationMap.Item2, locationMap.Item3);
                    if (seedLocation != seedHumidity)
                        break;
                }

                if (seedLocation < lowestLocation)
                    lowestLocation = seedLocation;
            }
        }

        return lowestLocation;
    }

    private static long GetMappedValue(long value, long destinationStart, long sourceStart, long range)
    {
        if(value < sourceStart || value > sourceStart + range)
            return value;
        
        return value - sourceStart + destinationStart;
    }

    private static IEnumerable<long> GetMappedSeedReverse(IEnumerable<long> values, List<Tuple<long,long,long>> maps)
    {
        foreach (var value in values)
        {
            if(foundLowestLocation)
                yield break;
            
            var currentValue = value;
            foreach (var map in maps)
            {
                if(foundLowestLocation)
                    yield break;
                
                if (value < map.Item1 || value >= map.Item1 + map.Item3)
                    continue;

                currentValue = value - map.Item1 + map.Item2;
                break;
            }

            yield return currentValue;
        }
    }

    private static IEnumerable<long> GetCurrentLocation()
    {
        for (var i = 0L; i < long.MaxValue; i++)
        {
            if(foundLowestLocation)
                yield break;
            
            currentLocation = i;
            yield return i;
        }
    }

    private enum Map
    {
        Seeds,
        Soil,
        Fertilizers,
        Water,
        Light,
        Temperature,
        Humidity,
        Location
    }
}