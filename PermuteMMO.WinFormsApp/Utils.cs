using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermuteMMO.WinFormsApp
{
    internal class Utils
    {
        public static Array ShinyValues = new[]
        {
            new {Text = "☆/■", Value = ShinyState.Undetermined },
            new {Text = "☆", Value = ShinyState.Shiny },
            new {Text = "■", Value = ShinyState.Square }
        };

        public static Array GenderValues = new[]
        {
            new {Text = "ANY", Value = Gender.Undetermined },
            new {Text = "M", Value = Gender.Male },
            new {Text = "F", Value = Gender.Female },
            new {Text = "/", Value = Gender.Genderless },
        };

        public static Array AlphaValues = new[]
        {
            new {Text = "ANY", Value = State.Undetermined },
            new {Text = "YES", Value = State.Yes },
            new {Text = "NO", Value = State.No },
        };
    }

    internal enum ShinyState
    {
        Shiny,
        Square,
        Undetermined,
    };

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
}
