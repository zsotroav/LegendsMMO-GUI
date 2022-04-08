using PermuteMMO.Lib;
using EtumrepMMO.Lib;

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

    #region Etumrep

    private void buttonEtumrep_Click(object sender, EventArgs e)
    {
        folderBrowserDialog.ShowDialog();
        if (string.IsNullOrEmpty(folderBrowserDialog.SelectedPath)) return;

        var inputs = GroupSeedFinder.GetInputs(folderBrowserDialog.SelectedPath);
        if (inputs.Count < 2)
        {
            MessageBox.Show(@"Insufficient inputs found in folder. 
Two (2) or more dumped files are required, four (4) recommended.", @"Error", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }
        var t = new Thread(() => SeedThreaded(inputs));
        t.Start();
        MessageBox.Show(@"Started looking for a seed. 
This may take a while...", @"Started seed thread", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void SeedThreaded(IReadOnlyList<PKHeX.Core.PKM> inputs)
    {
        var result = GroupSeedFinder.FindSeed(inputs);
        if (result is default(ulong))
        {
            MessageBox.Show(
                $@"No group seeds found with the input data. Double check your inputs (valid inputs: {inputs.Count}).",
                @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else
        {
            SetSeed(result.ToString());
            MessageBox.Show($@"Found seed! ({result})", @"Seed found", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void SetSeed(string seed)
    {
        if (InvokeRequired)
        {
            Invoke(new Action<string>(SetSeed), seed);
            return;
        }
        textBoxSeed.Text += seed;
    }

    #endregion

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
        openFileDialog.ShowDialog();
        var json = openFileDialog.FileName;
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

    private static void ResultClick(object sender, EventArgs e, PermuteResult permute, EntityResult entity)
    {
        var form = new DetailsForm(permute, entity);
        form.Show();
    }

    private void Result(PermuteResult permute, EntityResult entity)
    {
        // Container Panel
        var pan = new Panel
        {
            Location = new Point(3, panelFound.Controls.Count * 36 + 5),
            Size = new Size(602, 31),
            BackColor = SystemColors.ControlLight
        };

        // Name
        var name = GenBox(3, 0, entity.Name);
        name.Click += (sender, e) => ResultClick(sender, e, permute, entity);
        pan.Controls.Add(name);

        // Spawn
        pan.Controls.Add(GenBox(2, 3, (permute.IsBonus ? "BS" : "SP") + permute.SpawnIndex, 
            tooltip: (permute.IsBonus ? "Bonus Spawn" : "Spawn") + permute.SpawnIndex));

        // Gender
        pan.Controls.Add(GenBox(1, 5,
            entity.Gender switch
            {
                2 => "/",
                1 => "F",
                _ => "M"
            }, entity.Gender switch
            {
                2 => SystemColors.Control,
                1 => Color.FromArgb(255, 186, 225),
                _ => Color.FromArgb(186, 225, 255)
            }, entity.Gender switch
            {
                2 => "Genderless",
                1 => "Female",
                _ => "Male"
            }));

        // Alpha
        pan.Controls.Add(GenBox(1, 6, entity.IsAlpha ? "A" : "-",
            entity.IsAlpha ? Color.FromArgb(255, 179, 186) : SystemColors.Control,
            entity.IsAlpha ? @"Alpha" : "NOT Alpha"));

        // Shiny odds
        pan.Controls.Add(GenBox(1,7, entity.IsShiny ? entity.RollCountUsed.ToString() : "-",
            tooltip: entity.IsShiny ? "Shiny rolls needed to get shiny." : "NOT Shiny."));

        // Shiny XOR
        pan.Controls.Add(GenBox(1,8, entity.IsShiny ? entity.ShinyXor == 0 ? "\u25FC" : "\u2606" : "-",
            tooltip: entity.IsShiny ? entity.ShinyXor == 0 ? "Will be a square shiny in future games that support it" :
                "Shiny pokemon" : "NOT Shiny"));

        // Steps
        var i = 9;
        foreach (var advance in permute.Advances)
        {
            pan.Controls.Add(GenBox(1, i, advance.ToString(),
                advance switch
                {
                    Advance.A1 => Color.FromArgb(186, 255, 201),
                    Advance.A2 or Advance.A3 or Advance.A4 => Color.FromArgb(255, 223, 186),
                    Advance.G1 or Advance.G2 or Advance.G3 => Color.FromArgb(255, 179, 186),
                    _ => SystemColors.Control
                }));
            i++;
        }

        // Feasibility
        var (show, good, desc, tooltip) = GetFeasibility(permute.Advances, entity.IsSkittish,
            SpawnGenerator.IsSkittish(spawner.GetSpawn().BaseTable));
        if (show)
        {
            pan.Controls.Add(GenBox(3, i, desc, 
                good ? Color.FromArgb(186, 255, 201) : Color.FromArgb(255, 80, 72), 
                tooltip));
        }

        panelFound.Controls.Add(pan);
    }
    
    private static (bool show, bool good, string descShort, string descLong) GetFeasibility(ReadOnlySpan<Advance> advances, bool skittishBase, bool skittishBonus)
    {
        if (!advances.IsAnyMulti())
            return (true,true, "Single", 
                @"Single advances only, can be completed with catching pokemon one by one.");

        if (!skittishBase && !skittishBonus)
            return (false, true, string.Empty, string.Empty);

        var skittishMulti = false;
        var bonusIndex = EntityResult.GetBonusStartIndex(advances);
        if (bonusIndex != -1)
        {
            skittishMulti |= skittishBase && advances[..bonusIndex].IsAnyMulti();
            skittishMulti |= skittishBonus && advances[bonusIndex..].IsAnyMulti();
        }
        else
        {
            skittishMulti |= skittishBase && advances.IsAnyMulti();
        }

        return (true, false, skittishMulti ? "SK: Agg" : "SK: Single",
                skittishMulti ? @"Pokemon is Skittish, path only possible with other aggressive pokemon." :
                    @"Pokemon is skittish, path is possible by catching all skittish pokemon one by one.");
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
    private TextBox GenBox(int width, int loc, string text, Color backColor = default, string tooltip = "")
    {
        TextBox box = new()
        {
            ReadOnly = true,
            TextAlign = HorizontalAlignment.Center,
            Location = new Point(loc * 27 + 4, 4),
            Size = new Size(width * 23 + (width - 1) * 4, 23),
            Text = text,
            BackColor = backColor
        };
        box.MouseMove += (sender, e) => toolTip.SetToolTip(box, tooltip);
        return box;
    }

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