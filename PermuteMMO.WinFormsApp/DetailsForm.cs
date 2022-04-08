using PermuteMMO.Lib;
using PKHeX.Core;

namespace PermuteMMO.WinFormsApp;

public partial class DetailsForm : Form
{
    public DetailsForm(PermuteResult permute, EntityResult entity)
    {
        InitializeComponent();
        textBoxPKM.Text = $@"{entity.Name} ({entity.Form})";
        textBoxLVL.Text = $@"lvl {entity.Level}";

        textBoxGEN.Text = entity.Gender switch
        {
            2 => "/",
            1 => "F",
            _ => "M"
        };
        textBoxGEN.BackColor = entity.Gender switch
        {
            2 => SystemColors.Control,
            1 => Color.FromArgb(255, 186, 225),
            _ => Color.FromArgb(186, 225, 255)
        };
        textBoxGEN.MouseMove += (sender, e) => toolTip.SetToolTip(textBoxGEN, entity.Gender switch
        {
            2 => "Genderless",
            1 => "Female",
            _ => "Male"
        });

        textBoxSHN.Text = (entity.IsShiny ? (entity.ShinyXor == 0 ? $"\u25FC ({entity.RollCountUsed} rolls)" : $"\u2606 ({entity.RollCountUsed} rolls)") : "-");
        textBoxALP.Text = entity.IsAlpha ? "Alpha" : "NOT Alpha"; 

        (textBoxHP.Text, textBoxATK.Text, textBoxDEF.Text, textBoxSPE.Text, textBoxSPA.Text, textBoxSPD.Text) =
            (entity.IVs[0].ToString(), entity.IVs[1].ToString(), entity.IVs[2].ToString(), entity.IVs[3].ToString(), 
                entity.IVs[4].ToString(), entity.IVs[5].ToString());

        (textBoxABIL.Text, textBoxNAT.Text, textBoxHEI.Text, textBoxWEI.Text) =
            (GameInfo.GetStrings(1).Ability[entity.Ability], 
                GameInfo.GetStrings(1).Natures[entity.Nature], 
                entity.Height.ToString(), 
                entity.Weight.ToString());
        
        var i = 0;
        foreach (var advance in permute.Advances)
        {
            var box = new TextBox
            {
                ReadOnly = true,
                TextAlign = HorizontalAlignment.Center,
                Location = new Point(i * 27 + 4, 4),
                Size = new Size(23, 23),
                Text = advance.ToString(),
                BackColor = advance switch
                    {
                        Advance.A1 => Color.FromArgb(186, 255, 201),
                        Advance.A2 or Advance.A3 or Advance.A4 => Color.FromArgb(255, 223, 186),
                        Advance.G1 or Advance.G2 or Advance.G3 => Color.FromArgb(255, 179, 186),
                        _ => SystemColors.Control
                    }
            };
            box.MouseMove += (sender, e) => toolTip.SetToolTip(box, advance switch
            {
                Advance.A1 => "Catch 1",
                Advance.A2 => "Multi-battle 2, catch or defeat them.",
                Advance.A3 => "Multi-battle 3, catch or defeat them.",
                Advance.A4 => "Multi-battle 4, catch or defeat them.",
                Advance.G1 => "Catch 1, then run away >120m",
                Advance.G2 => "Multi-battle 2, catch or defeat them, run away >120m",
                Advance.G3 => "Multi-battle 3, catch or defeat them, run away >120m",
                Advance.SB => "Start Bonus round",
                _ => "???"
            });
            panelSTP.Controls.Add(box);
            i++;
        }
    }
}

