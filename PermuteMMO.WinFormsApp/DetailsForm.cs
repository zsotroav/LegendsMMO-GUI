using PermuteMMO.Lib;
using PKHeX.Core;

namespace PermuteMMO.WinFormsApp;

public partial class DetailsForm : Form
{
    public DetailsForm(PermuteResult permute, EntityResult entity, MainForm mainForm)
    {
        InitializeComponent();
        textBoxPKM.Text = $@"{entity.Name}";
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
        textBoxGEN.MouseMove += (_, _) => toolTip.SetToolTip(textBoxGEN, entity.Gender switch
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

        (textBoxHP.BackColor, textBoxATK.BackColor, textBoxDEF.BackColor, textBoxSPE.BackColor, textBoxSPA.BackColor,
                textBoxSPD.BackColor) =
            (RatingColor(entity.IVs[0]), RatingColor(entity.IVs[1]), RatingColor(entity.IVs[2]),
                RatingColor(entity.IVs[3]), RatingColor(entity.IVs[4]), RatingColor(entity.IVs[5]));

        (textBoxABIL.Text, textBoxNAT.Text, textBoxHEI.Text, textBoxWEI.Text) =
            (GameInfo.GetStrings(1).Ability[entity.Ability], 
                GameInfo.GetStrings(1).Natures[entity.Nature], 
                entity.Height.ToString(), 
                entity.Weight.ToString());
        
        var i = 0;
        AdvanceExtensions.Raw = false;
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
                        Advance.S2 or Advance.S3 or Advance.S4 => Color.FromArgb(255, 186, 225),
                        Advance.G1 or Advance.G2 or Advance.G3 => Color.FromArgb(255, 179, 186),
                        _ => SystemColors.Control
                    }
            };
            box.MouseMove += (_,_) => toolTip.SetToolTip(box, advance.GetName());
            panelSTP.Controls.Add(box);
            i++;
        }

        // Feasibility
        var (show, good, desc, tooltip) = MainForm.GetFeasibility(permute.Advances, entity.IsSkittish,
            SpawnGenerator.IsSkittish(mainForm.Spawner.GetSpawn().Set.Table));
        if (show)
        {
            textBoxRemarks.BackColor = good ? Color.FromArgb(186, 255, 201) : Color.FromArgb(255, 80, 72);
            textBoxRemarks.Text = desc;
            toolTip.SetToolTip(textBoxRemarks, tooltip);
        }
    }

    public Color RatingColor(byte iv) => iv switch
        {
            0 => Color.FromArgb(255, 154, 162),
            >= 1 and <= 10 => Color.FromArgb(254, 183, 177),
            >= 11 and <= 20 =>Color.FromArgb(255, 218, 192),
            >= 21 and <= 29 =>Color.FromArgb(226, 240, 204),
            30 => Color.FromArgb(181, 234, 214),
            31 => Color.FromArgb(199, 206, 234),
            _ => default
        };
}

