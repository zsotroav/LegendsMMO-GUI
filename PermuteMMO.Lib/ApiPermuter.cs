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
