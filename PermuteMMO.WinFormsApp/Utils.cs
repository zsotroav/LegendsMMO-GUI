using PermuteMMO.Lib;

namespace PermuteMMO.WinFormsApp;

internal class Utils
{
    public static Array ShinyValues = new[]
    {
        new {Text = "☆/■", Value = ShinyState.Undetermined},
        new {Text = "☆", Value = ShinyState.Shiny},
        new {Text = "■", Value = ShinyState.Square}
    };

    public static Array GenderValues = new[]
    {
        new {Text = "ANY", Value = Gender.Undetermined},
        new {Text = "M", Value = Gender.Male},
        new {Text = "F", Value = Gender.Female},
        new {Text = "/", Value = Gender.Genderless},
    };

    public static Array AlphaValues = new[]
    {
        new {Text = "ANY", Value = State.Undetermined},
        new {Text = "YES", Value = State.Yes},
        new {Text = "NO", Value = State.No},
    };
}

internal static class StaticUtils
{
    internal static Color AdvanceColor(Advance advance) =>
        advance switch
        {
            Advance.A1 or Advance.A2 or Advance.A3 or Advance.A4 => Color.FromArgb(186, 255, 201), // Green
            Advance.G1 or Advance.G2 or Advance.G3               => Color.FromArgb(186, 225, 255), // Blue
            Advance.O1 or Advance.O2 or Advance.O3 or Advance.O4 => Color.FromArgb(255, 223, 186), // Yellow
            Advance.B1 or Advance.B2 or Advance.B3 or Advance.B4 => Color.FromArgb(255, 186, 225), // Pink
                          Advance.S2 or Advance.S3 or Advance.S4 => Color.FromArgb(255, 179, 186), // Red
            _ /* including RG and CR */                          => SystemColors.Control 
        };
}

internal enum ShinyState
{
    Shiny,
    Square,
    Undetermined,
}

internal enum State
{
    Yes,
    No,
    Undetermined
}

internal enum Gender : byte
{
    Male = 0,
    Female = 1,
    Genderless = 2,
    Undetermined = 9
}

