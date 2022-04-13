using System.Diagnostics;
using PKHeX.Core;

namespace PermuteMMO.Lib;

/// <summary>
/// Logic to permute spawner data.
/// </summary>
public static class ApiPermuter
{

    // Mainly for debugging and detailed logging
    public delegate void MessageDel(string message = @"==========");
    public static event MessageDel? Message;

    // Return values for MMO spawners
    public delegate void MMOSpawnerDel(int number, float x, float y, float z, string species);
    public static event MMOSpawnerDel? MMOSpawner;

    // Return values for MO spawners
    public delegate void MOSpawnerDel(float x, float y, float z, string species);
    public static event MOSpawnerDel? MOSpawner;

    // Return results
    public delegate void ResultDel(PermuteResult permute);
    public static event ResultDel? Result;

    // $@"No results found{extra}"
    public delegate void NoResDel(string extra = ".");
    public static event NoResDel? NoRes;

    // $@"Done permutating{extra}"
    public delegate void DoneDel(string extra = ".");
    public static event DoneDel? Done;

    /// <summary>
    /// Permutes all the areas to print out all possible spawns.
    /// </summary>
    public static void PermuteMassiveMassOutbreak(Span<byte> data)
    {
        var block = new MassiveOutbreakSet8a(data);
        for (int i = 0; i < MassiveOutbreakSet8a.AreaCount; i++)
        {
            var area = block[i];
            var areaName = AreaUtil.AreaTable[area.AreaHash];
            if (!area.IsActive)
            {
                Message?.Invoke($@"No outbreak in {areaName}.");
                continue;
            }
            Debug.Assert(area.IsValid);

            var hasPrintedAreaMMO = false;
            for (int j = 0; j < MassiveOutbreakArea8a.SpawnerCount; j++)
            {
                var spawner = area[j];
                if (spawner.Status is MassiveOutbreakSpawnerStatus.None)
                    continue;

                Debug.Assert(spawner.HasBase);
                var seed = spawner.SpawnSeed;
                var spawn = new SpawnInfo(spawner);

                var result = Permuter.Permute(spawn, seed);
                if (!result.HasResults)
                    continue;

                if (!hasPrintedAreaMMO)
                {
                    Message?.Invoke($@"Found paths for Massive Mass Outbreaks in {areaName}.");
                    Message?.Invoke();
                    hasPrintedAreaMMO = true;
                }

                MMOSpawner?.Invoke(j+1,spawner.X, spawner.Y, spawner.Z, SpeciesName.GetSpeciesName(spawner.DisplaySpecies, 2));
                // Console.WriteLine($@"Spawner {j + 1} at ({spawner.X:F1}, {spawner.Y:F1}, {spawner.Z}) shows {SpeciesName.GetSpeciesName(spawner.DisplaySpecies, 2)}");
                Message?.Invoke($@"Parameters: {spawn} \n");
                foreach (var permuteResult in result.Results)
                {
                    Result?.Invoke(permuteResult);
                }
            }

            if (!hasPrintedAreaMMO)
            {
                NoRes?.Invoke($@" for any Massive Mass Outbreak in {areaName}.");
            }
            else
            {
                Done?.Invoke("area.");
                Message?.Invoke();
            }
        }
    }

    /// <summary>
    /// Permutes all the Mass Outbreaks to print out all possible spawns.
    /// </summary>
    public static void PermuteBlockMassOutbreak(Span<byte> data)
    {
        Message?.Invoke(@"Permuting Mass Outbreaks...");
        var block = new MassOutbreakSet8a(data);
        for (int i = 0; i < MassOutbreakSet8a.AreaCount; i++)
        {
            var spawner = block[i];
            var areaName = AreaUtil.AreaTable[spawner.AreaHash];
            if (!spawner.HasOutbreak)
            {
                Message?.Invoke($@"No outbreak in {areaName}.");
                continue;
            }
            Debug.Assert(spawner.IsValid);

            var seed = spawner.SpawnSeed;
            var spawn = new SpawnInfo(spawner);

            var result = Permuter.Permute(spawn, seed);
            if (!result.HasResults)
            {
                Message?.Invoke($@"Found no paths for {(Species)spawner.DisplaySpecies} Mass Outbreak in {areaName}.");
                continue;
            }

            Message?.Invoke($@"Found paths for {(Species)spawner.DisplaySpecies} Mass Outbreak in {areaName}:");
            Message?.Invoke();
            MOSpawner?.Invoke(spawner.X, spawner.Y, spawner.Z, SpeciesName.GetSpeciesName(spawner.DisplaySpecies, 2));
            // Console.WriteLine($@"Spawner at ({spawner.X:F1}, {spawner.Y:F1}, {spawner.Z}) shows {SpeciesName.GetSpeciesName(spawner.DisplaySpecies, 2)}");
            Message?.Invoke($@"Parameters: {spawn} \n");
            foreach (var permuteResult in result.Results)
            {
                Result?.Invoke(permuteResult);
            }
        }
        Done?.Invoke("Mass Outbreaks.");
        Message?.Invoke();
    }

    /// <summary>
    /// Permutes a single spawn with simple info.
    /// </summary>
    public static void PermuteSingle(SpawnInfo spawn, ulong seed, ushort species)
    {
        Message?.Invoke($@"Permuting all possible paths for {seed:X16}.");
        Message?.Invoke($@"Parameters: {spawn} \n");

        var result = Permuter.Permute(spawn, seed);
        if (!result.HasResults)
            NoRes?.Invoke();
        else
            foreach (var permuteResult in result.Results)
                Result?.Invoke(permuteResult);

        Done?.Invoke();
        Message?.Invoke();
    }
}
