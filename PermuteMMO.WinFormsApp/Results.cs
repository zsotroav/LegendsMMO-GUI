using PermuteMMO.Lib;

namespace PermuteMMO.WinFormsApp;

internal class Results
{
    public PermuteResult Permute { get; set; }

    public EntityResult Entity { get; set; }

    public bool Qualifies { get; set; }

    /// <summary>
    /// Checks if the result qualifies as a wanted pokemon.
    /// </summary>
    /// <param name="shiny">Whether a pokemon is shiny or not.</param>
    /// <param name="square">Whether a pokemon is a square shiny (0 XOR) or not.</param>
    /// <param name="alpha">Whether a pokemon is an alpha or not.</param>
    /// <param name="gender">Gender of the pokemon.</param>
    /// <param name="rolls">Shiny rolls needed for the pokemon.</param>
    /// <returns></returns>
    public bool Qualify(ShinyState shiny, State alpha, Gender gender, int rolls)
    {
        var s = shiny switch
        {
            ShinyState.Shiny => Entity.IsShiny,
            ShinyState.Square => Entity.ShinyXor == 0 && Entity.IsShiny,
            _ => shiny == ShinyState.Undetermined
        };

        var a = alpha switch
        {
            State.Yes => Entity.IsAlpha,
            State.No => Entity.IsAlpha == false,
            _ => alpha == State.Undetermined
        };

        var g = gender == Gender.Undetermined || Entity.Gender == (byte) gender;
        var r = Entity.RollCountUsed <= rolls;
        
        var final = s && a && g && r;
        Qualifies = final;
        return final;
    }
}
