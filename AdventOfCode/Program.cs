using _2022._1;
using _2022._4;
using _2022._5;
using _2022._10;
using _2022._11;
using _2022._12;
using _2022._13;
using _2022._14;
using _2022._2;
using _2022._3;
using _2022._6;
using _2022._7;
using _2022._8;
using _2022._9;
using _2023._1;
using _2023._2;
using _2023._3;
using _2023._4;
using _2023._5;
using _2023._6;
using _2023._7;

// Run2022();
Run2023();

#region 2023
void Run2023()
{
    Console.WriteLine($"Trebuchet, sum of calibrations: {Trebuchet.SumCalibrations()}");
    Console.WriteLine($"Trebuchet, sum of real calibrations: {Trebuchet.SumRealCalibrations()}");
    Console.WriteLine($"Cube Conundrum, sum of game ids: {CubeConundrum.SumGameIds()}");
    Console.WriteLine($"Cube Conundrum, sum of cube powers: {CubeConundrum.SumCubePowers()}");
    Console.WriteLine($"Gear Ratios, sum of valid parts: {GearRatios.SumPartNumbers().Item1}");
    Console.WriteLine($"Gear Ratios, sum of valid gears: {GearRatios.SumPartNumbers().Item2}");
    Console.WriteLine($"Scratchcards, sum of winning scratchcards: {Scratchcards.SumScratchcards()}");
    Console.WriteLine($"Scratchcards, number of total scratchcards: {Scratchcards.SumTotalNumberOfScratchcards()}");
    Console.WriteLine($"If You Give A Seed A Fertilizer, lowest location number of initial seed: {IfYouGiveASeedAFertilizer.LowestLocationNumberOfSeed()}");
    //SLOW Console.WriteLine($"If You Give A Seed A Fertilizer, lowest location number of initial seed ranges: {IfYouGiveASeedAFertilizer.LowestLocationNumberOfSeed(isPart2: true)}");
    Console.WriteLine($"Wait for it, number of ways the record in each race can be beaten: {WaitForIt.GetNumberOfWaysToBeatRecord()}");
    Console.WriteLine($"Wait for it, number of ways the record in the big race can be beaten: {WaitForIt.GetNumberOfWaysToBeatBigRecord()}");
    Console.WriteLine($"Camel Cards, Calculate total winnings: {CamelCards.CalculateTotalWinnings()}");
    Console.WriteLine($"Camel Cards, Calculate total winnings with jokers: {CamelCards.CalculateTotalWinnings(withJokers: true)}");
}
#endregion

#region 2022
void Run2022()
{
    Console.WriteLine($"Calorie Counting, Highest calorie item: {CalorieCounting.GetHighestCalorieCount()}");
    Console.WriteLine($"Calorie Counting, Highest sum of calories: {CalorieCounting.GetSumOfThreeHighestCalorieCounts()}");
    Console.WriteLine($"Rock Paper Scissors total score: {RockPaperScissors.CalculateScore()}");
    Console.WriteLine($"Rock Paper Scissors actual score: {RockPaperScissors.CalculateActualScore()}");
    Console.WriteLine($"Rucksack Organization priority score: {RucksackOrganization.CalculatePriorities()}");
    Console.WriteLine($"Rucksack Organization Badge priority score: {RucksackOrganization.CalculateBadgePriority()}");
    Console.WriteLine($"Camp Cleanup Overlapped pairs: {CampCleanup.CalculateCleanupOverlap()}");
    Console.WriteLine($"Camp Cleanup Partially Overlapped pairs: {CampCleanup.CalculateCleanupPartialOverlap()}");
    Console.WriteLine($"Supply Stacks Final crate on each stack: {SupplyStacks.CalculateCargoStack()}");
    Console.WriteLine($"Supply Stacks Final crate on each stack CrateMover9001: {SupplyStacks.CrateMover9001()}");
    Console.WriteLine($"Number of characters to process before finding packet marker: {TuningTrouble.DetectMarkerCharacters()}");
    Console.WriteLine($"Number of characters to process before finding message marker: {TuningTrouble.DetectMarkerCharacters(14)}");
    Console.WriteLine($"Total size of directories with individual size below 100 000: {NoSpaceLeftOnDevice.CalculateDirectorySize()}");
    Console.WriteLine($"Size of smallest relevant directory to delete: {NoSpaceLeftOnDevice.GetRelevantDirectorySize()}");
    Console.WriteLine($"Total amount of visible trees from outside grid: {TreetopTreeHouse.TotalVisibleTrees()}");
    Console.WriteLine($"Greatest scenic score for treehouse placed on tree: {TreetopTreeHouse.CalculateScenicScore()}");
    Console.WriteLine($"Number of positions the tail of the rope will visit at least once: {RopeBridge.CalculateRopeTailPositions()}");
    Console.WriteLine($"Number of positions the tail of the long rope will visit at least once: {RopeBridge.CalculateLongRopeTailPositions()}");
    Console.WriteLine($"Sum of signal strength from cathode ray tube: {CathodeRayTube.GetSumSignalStrengths()}");
    Console.WriteLine("Image printed on CRT Screen:"); CathodeRayTube.DrawCRTScreen();
    Console.WriteLine($"The level of monkey business after 20 rounds of stuff-slinging simian shenanigans: {MonkeyInTheMiddle.CalculateMonkeyBusiness()}");
    Console.WriteLine($"The level of monkey business after 10.000 rounds of extreme stuff-slinging simian shenanigans: {MonkeyInTheMiddle.CalculateExtremeMonkeyBusiness()}");
    Console.WriteLine($"The fewest steps required to move from your current position to the location that should get the best signal: {HillClimbingAlgorithm.CalculateFewestSteps()}");
    Console.WriteLine($"The fewest steps required to move from any tile to the location that should get the best signal: {HillClimbingAlgorithm.CalculateFewestStepsAllTiles()}");
    Console.WriteLine($"Sum of packet pairs indices that are already in the right order: {DistressSignal.CheckPacketPairs()}");
    Console.WriteLine($"Decoder key of the distress signal: {DistressSignal.GetDecoderKey()}");
    Console.Write("Units of sand that come to rest before sand starts flowing into the abyss below: "); RegolithReservoir.SimulateFallingSand();
    //Console.Write("Units of sand that come to rest before sand blocks the source: "); RegolithReservoir.SimulateSandFilling();
    //Console.WriteLine($"Positions that cannot contain a beacon in the row where y=2000000: {BeaconExclusionZone.CountIneligibleBeaconPositions()}");
    //Console.WriteLine($"Distress signal location: {BeaconExclusionZone.FindDistressSignalLocation()}"); UNFINISHED
    //Console.WriteLine($"The most most pressure that can be released in 30 minutes: {ProboscideaVolcanium.MaximizePressureRelease()}");
    //Console.WriteLine($"The height of the tower after 2022 rocks have fallen down: {PyroclasticFlow.CalculateTowerHeight()}");
}
#endregion