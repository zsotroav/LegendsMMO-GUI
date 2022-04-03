using PermuteMMO.Lib;

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

        textBoxSHN.Text = (entity.IsShiny ? (entity.ShinyXor == 0 ? $"\u25FC ({entity.RollCountUsed} rolls)" : $"\u2606 ({entity.RollCountUsed} rolls)") : "-");
        textBoxALP.Text = entity.IsAlpha ? "Alpha" : "NOT Alpha"; 

        (textBoxHP.Text, textBoxATK.Text, textBoxDEF.Text, textBoxSPE.Text, textBoxSPA.Text, textBoxSPD.Text) =
            (entity.IVs[0].ToString(), entity.IVs[1].ToString(), entity.IVs[2].ToString(), entity.IVs[3].ToString(), 
                entity.IVs[4].ToString(), entity.IVs[5].ToString());

        (textBoxABIL.Text, textBoxNAT.Text, textBoxHEI.Text, textBoxWEI.Text) =
            (((PKHeX.Core.Ability)entity.Ability).ToString(), 
                ((PKHeX.Core.Nature)entity.Nature).ToString(), 
                entity.Height.ToString(), 
                entity.Weight.ToString());
        
        var i = 0;
        foreach (var advance in permute.Advances)
        {
            panelSTP.Controls.Add(new TextBox
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
            });
            i++;
        }
    }
}

