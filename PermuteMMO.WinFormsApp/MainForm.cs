using PermuteMMO.Lib;

namespace PermuteMMO.WinFormsApp;

public partial class MainForm : Form
{
    public UserEnteredSpawnInfo spawner = new();
    public MainForm()
    {
        InitializeComponent();
        // TODO: MMOSpawner
        // TODO: MOSpawner
        ApiPermuter.Result += Result;
        ApiPermuter.NoRes += NoRes;
        ApiPermuter.Done += Done;

        comboBoxSpecies.DataSource = new BindingSource(Lib.PokemonLocationUtil.Pokemon, null);
        comboBoxSpecies.DisplayMember = "Text";
        comboBoxSpecies.ValueMember = "Value";
    }

    private void Criteria()
    {
        PermuteMeta.SatisfyCriteria = (result, advances) =>
            (checkWantShiny.Checked ? result.IsShiny : !result.IsShiny) &&
            (checkWantSquare.Checked ? (result.ShinyXor == 0 && result.IsShiny) : result.ShinyXor != 0) &&
            (checkWantAlpha.Checked ? result.IsAlpha : !result.IsAlpha) &&
            (comboWantGender.Text switch
            {
                "M" => result.Gender == 0,
                "F" => result.Gender == 1,
                "/" => result.Gender == 2,
                _ => result.Gender != 9
            });
        SpawnGenerator.MaxShinyRolls = (int) numericWantMax.Value;
    }

    private void checkBoxMMO_CheckedChanged(object sender, EventArgs e)
    {
        if (checkBoxMMO.Checked)
        {
            textBoxWarn.ForeColor = Color.DarkRed;
            textBoxBase.Enabled = true;
            textBoxBonus.Enabled = true;
            //
            numericWantMax.Maximum = 19;
            numericWantMax.Value = 19;
            comboBoxLoc.Enabled = true;
            //checkBoxAlpha.Enabled = true;
            //comboBoxSpecies2.Enabled = true;
            numericSpawns2.Enabled = true;

            var x = comboBoxLoc.Text switch
            {
                "Crimson Mirelands" => PokemonLocationUtil.CrimsonMirelandsAPokemon,
                "Cobalt Coastlands" => PokemonLocationUtil.CobaltCoastlandsAPokemon,
                "Coronet Highlands" => PokemonLocationUtil.CoronetHighlandsAPokemon,
                "Alabaster Icelands" => PokemonLocationUtil.AlabasterIcelandsAPokemon,
                _ => PokemonLocationUtil.ObsidianFieldlandsAPokemon
            };
            comboBoxSpecies.DataSource = null;
            comboBoxSpecies.DataSource = new BindingSource(x, null);
            comboBoxSpecies.DisplayMember = "Text";
            comboBoxSpecies.ValueMember = "Value";

            x = comboBoxLoc.Text switch
            {
                "Crimson Mirelands" => PokemonLocationUtil.CrimsonMirelandsBPokemon,
                "Cobalt Coastlands" => PokemonLocationUtil.CobaltCoastlandsBPokemon,
                "Coronet Highlands" => PokemonLocationUtil.CoronetHighlandsBPokemon,
                "Alabaster Icelands" => PokemonLocationUtil.AlabasterIcelandsBPokemon,
                _ => PokemonLocationUtil.ObsidianFieldlandsBPokemon
            };
            comboBoxSpecies2.DataSource = null;
            comboBoxSpecies2.DataSource = new BindingSource(x, null);
            comboBoxSpecies2.DisplayMember = "Text";
            comboBoxSpecies2.ValueMember = "Value";

        }
        else
        {
            textBoxWarn.ForeColor = SystemColors.Control;
            textBoxBase.Enabled = false;
            textBoxBonus.Enabled = false;
            //
            numericWantMax.Maximum = 32;
            numericWantMax.Value = 32;
            comboBoxLoc.Enabled = false;
            checkBoxAlpha.Enabled = false;
            checkBoxAlpha.Checked = false;
            comboBoxSpecies2.Enabled = false;
            comboBoxSpecies2.Text = "";
            numericSpawns2.Enabled = false;
            numericSpawns2.Value = 0;

            comboBoxSpecies2.DataSource = null;
            comboBoxSpecies.DataSource = null;
            comboBoxSpecies.DataSource = new BindingSource(PokemonLocationUtil.Pokemon, null);
            comboBoxSpecies.DisplayMember = "Text";
            comboBoxSpecies.ValueMember = "Value";
        }
    }

    private void buttonJSON_Click(object sender, EventArgs e)
    {
        openFileDialog1.ShowDialog();
        var json = openFileDialog1.FileName;
        if (string.IsNullOrEmpty(json) || !File.Exists(json)) return;

        spawner = JsonDecoder.Deserialize<UserEnteredSpawnInfo>(File.ReadAllText(json));

        var spawn = spawner.GetSpawn();
        panelFound.Controls.Clear();
        Criteria();
        ApiPermuter.PermuteSingle(spawn, spawner.GetSeed(), spawner.Species);
    }

    private void buttonPermutate_Click(object sender, EventArgs e)
    {
        // Sanity check
        if (!(numericSpawns2.Value == 0 || numericSpawns2.Value == 6 || numericSpawns2.Value == 7))
        {
            MessageBox.Show(@"MMO second wave spawns must be 0, 6, or 7!", @"Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        spawner.Seed = textBoxSeed.Text;
        spawner.Species = (ushort) PKHeX.Core.SpeciesName.GetSpeciesID((string) comboBoxSpecies.SelectedValue);
        spawner.BaseCount = (int) numericSpawns.Value;
        spawner.BaseTable = checkBoxMMO.Checked ? textBoxBase.Text : "0x0000000000000000";
        spawner.BonusCount = (int) numericSpawns2.Value;
        spawner.BonusTable = checkBoxMMO.Checked ? textBoxBonus.Text : "0x0000000000000000";

        var spawn = spawner.GetSpawn();
        panelFound.Controls.Clear();
        Criteria();
        ApiPermuter.PermuteSingle(spawn, spawner.GetSeed(), spawner.Species);
    }

    private static void NoRes(string extra)
    {
        MessageBox.Show($@"No results found{extra}
This means that you will not have a desired pokemon in any permutation. Try changing the wanted pokemon criteria.",
            @"No results", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    private static void Done(string extra)
    {
        MessageBox.Show($@"Done permutating{extra}", @"Done", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void comboBoxLoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (checkBoxMMO.Checked)
        {
            var x = comboBoxLoc.Text switch
            {
                "Crimson Mirelands" => PokemonLocationUtil.CrimsonMirelandsAPokemon,
                "Cobalt Coastlands" => PokemonLocationUtil.CobaltCoastlandsAPokemon,
                "Coronet Highlands" => PokemonLocationUtil.CoronetHighlandsAPokemon,
                "Alabaster Icelands" => PokemonLocationUtil.AlabasterIcelandsAPokemon,
                _ => PokemonLocationUtil.ObsidianFieldlandsAPokemon
            };
            comboBoxSpecies.DataSource = null;
            comboBoxSpecies.DataSource = new BindingSource(x, null);
            comboBoxSpecies.DisplayMember = "Text";
            comboBoxSpecies.ValueMember = "Value";
            x = comboBoxLoc.Text switch
            {
                "Crimson Mirelands" => PokemonLocationUtil.CrimsonMirelandsBPokemon,
                "Cobalt Coastlands" => PokemonLocationUtil.CobaltCoastlandsBPokemon,
                "Coronet Highlands" => PokemonLocationUtil.CoronetHighlandsBPokemon,
                "Alabaster Icelands" => PokemonLocationUtil.AlabasterIcelandsBPokemon,
                _ => PokemonLocationUtil.ObsidianFieldlandsBPokemon
            };
            comboBoxSpecies2.DataSource = null;
            comboBoxSpecies2.DataSource = new BindingSource(x, null);
            comboBoxSpecies2.DisplayMember = "Text";
            comboBoxSpecies2.ValueMember = "Value";
        }
        else
        {
            comboBoxSpecies2.DataSource = null;
            comboBoxSpecies.DataSource = null;
            comboBoxSpecies.DataSource = new BindingSource(PokemonLocationUtil.Pokemon, null);
            comboBoxSpecies.DisplayMember = "Text";
            comboBoxSpecies.ValueMember = "Value";
        }
    }

    private void Result(PermuteResult permute, EntityResult entity)
    {
        // Container Panel
        var pan = new Panel();
        pan.Location = new Point(3, panelFound.Controls.Count * 36 + 5);
        pan.Size = new Size(600, 31);
        pan.BackColor = SystemColors.ControlLight;

        // Name
        pan.Controls.Add(GenBox(3, 0, entity.Name));

        // Spawn
        pan.Controls.Add(GenBox(2,3, (permute.IsBonus ? "BS" : "SP") + permute.SpawnIndex));

        // Gender
        pan.Controls.Add(GenBox(1, 5,
            entity.Gender switch
            {
                2 => "/",
                1 => "F",
                _ => "M",
            }, entity.Gender switch
            {
                2 => SystemColors.Control,
                1 => Color.FromArgb(255, 186, 225),
                _ => Color.FromArgb(186, 225, 255),
            }));

        // Alpha
        pan.Controls.Add(GenBox(1, 6, entity.IsAlpha ? "A" : "-",
            entity.IsAlpha ? Color.FromArgb(255, 179, 186) : SystemColors.Control));

        // Shiny odds
        pan.Controls.Add(GenBox(1,7, entity.IsShiny ? entity.RollCountUsed.ToString() : "-"));

        // Shiny XOR
        pan.Controls.Add(GenBox(1,8, entity.IsShiny ? entity.ShinyXor == 0 ? "\u25FC" : "*" : "-"));

        // Steps
        var i = 9;
        foreach (var advance in permute.Advances)
        {
            var ad = GenBox(1, i, advance.ToString());
            ad.BackColor = advance switch
            {
                Advance.A1 => Color.FromArgb(186, 255, 201),
                Advance.A2 or Advance.A3 or Advance.A4 => Color.FromArgb(255, 223, 186),
                Advance.G1 or Advance.G2 or Advance.G3 => Color.FromArgb(255, 179, 186),
                _ => SystemColors.Control
            };
            pan.Controls.Add(ad);
            i++;
        }

        panelFound.Controls.Add(pan);
    }

    /// <summary>
    /// Generate TexBox with standard unit width, height, and location.
    /// 1 width = 23*23
    /// </summary>
    /// <param name="width">Width of the box in standard units</param>
    /// <param name="loc">Location of the box in standard units</param>
    /// <param name="text">TextBox.Text property</param>
    /// <param name="backColor">TextBox.BackColor property</param>
    /// <returns>Generated unit size TexBox</returns>
    private static TextBox GenBox(int width, int loc, string text, Color backColor = default) => new()
    {
        ReadOnly = true,
        TextAlign = HorizontalAlignment.Center,
        Location = new Point(loc * 27 + 4, 4),
        Size = new Size(width * 23 + (width - 1) * 4, 23),
        Text = text,
        BackColor = backColor
    };
    
    private void checkWantSquare_CheckedChanged(object sender, EventArgs e)
    {
        if (checkWantSquare.Checked)
        {
            checkWantShiny.Checked = true;
            checkWantShiny.Enabled = false;
        }
        else
            checkWantShiny.Enabled = true;
    }
}