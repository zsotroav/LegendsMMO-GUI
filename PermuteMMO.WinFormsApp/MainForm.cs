using PermuteMMO.Lib;
using EtumrepMMO.Lib;
using PKHeX.Core;

namespace PermuteMMO.WinFormsApp;

public partial class MainForm : Form
{
    private List<Results> _resultsList = new();
    public UserEnteredSpawnInfo Spawner = new();
    public MainForm()
    {

        PermuteMeta.SatisfyCriteria = (result, _) => result.IsShiny;
        InitializeComponent();
        // TODO: MMOSpawner
        // TODO: MOSpawner
        ApiPermuter.Result += Result;
        ApiPermuter.NoRes += NoRes;
        ApiPermuter.Done += Done;

        comboBoxSpecies.DataSource = new BindingSource(PokemonLocationUtil.Pokemon, null);
        comboBoxSpecies.DisplayMember = "Text";
        comboBoxSpecies.ValueMember = "Value";

        comboWantShiny.DataSource = new BindingSource(Utils.ShinyValues, null);
        comboWantShiny.DisplayMember = "Text";
        comboWantShiny.ValueMember = "Value";

        comboWantGender.DataSource = new BindingSource(Utils.GenderValues, null);
        comboWantGender.DisplayMember = "Text";
        comboWantGender.ValueMember = "Value";

        comboWantAlpha.DataSource = new BindingSource(Utils.AlphaValues, null);
        comboWantAlpha.DisplayMember = "Text";
        comboWantAlpha.ValueMember = "Value";
    }

    #region Etumrep

    private void buttonEtumrep_Click(object sender, EventArgs e)
    {
        folderBrowserDialog.ShowDialog();
        if (string.IsNullOrEmpty(folderBrowserDialog.SelectedPath)) return;

        var inputs = GroupSeedFinder.GetInputs(folderBrowserDialog.SelectedPath);
        switch (inputs.Count)
        {
            case < 2:
                MessageBox.Show(@"Insufficient inputs found in folder. 
Two (2) or more dumped files are required, four (4) recommended.", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            case > 4:
                MessageBox.Show(@"Too many inputs found in folder. 
Only the first four (4) pokemon are required.", 
                    @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
        }

        var t = new Thread(() => SeedThreaded(inputs));
        t.Start();
        MessageBox.Show(@"Started looking for a seed. 
This may take a while...", @"Started seed thread", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void SeedThreaded(IReadOnlyList<PKM> inputs)
    {
        var (result, index) = GroupSeedFinder.FindSeed(inputs);
        if (index == -1)
        {
            MessageBox.Show(
                $@"No group seeds found with the input data. Double check your inputs (valid inputs: {inputs.Count}).",
                @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        else
        {
            SetSeed(result.ToString());
            MessageBox.Show($@"Found seed from input {index+1}/{inputs.Count}! ({result})", @"Seed found", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void SetSeed(string seed)
    {
        if (InvokeRequired)
        {
            Invoke(new Action<string>(SetSeed), seed);
            return;
        }
        textBoxSeed.Text = seed;
    }

    #endregion

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

        Spawner = JsonDecoder.Deserialize<UserEnteredSpawnInfo>(File.ReadAllText(json));

        var spawn = Spawner.GetSpawn();
        panelFound.Controls.Clear();
        _resultsList.Clear();
        ApiPermuter.PermuteSingle(spawn, Spawner.GetSeed(), Spawner.Species);
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

        Spawner.Seed = textBoxSeed.Text;
        Spawner.Species = (ushort) SpeciesName.GetSpeciesID((string) comboBoxSpecies.SelectedValue);
        Spawner.BaseCount = (int) numericSpawns.Value;
        Spawner.BaseTable = checkBoxMMO.Checked ? textBoxBase.Text : "0x0000000000000000";
        Spawner.BonusCount = (int) numericSpawns2.Value;
        Spawner.BonusTable = checkBoxMMO.Checked ? textBoxBonus.Text : "0x0000000000000000";

        var spawn = Spawner.GetSpawn();
        panelFound.Controls.Clear();
        _resultsList.Clear();
        ApiPermuter.PermuteSingle(spawn, Spawner.GetSeed(), Spawner.Species);
    }

    private static void NoRes(string extra)
    {
        MessageBox.Show($@"No results found{extra}
This means that you will not have a desirable pokemon in any permutation.",
            @"No results", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    private void Done(string extra)
    {
        foreach (var res in _resultsList.Where(res => res.Qualifies))
        {
            PrintResult(res.Permute, res.Entity);
        }

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

    private void ResultClick(PermuteResult permute, EntityResult entity)
    {
        var form = new DetailsForm(permute, entity);
        form.Show();
    }

    private void buttonReload_Click(object sender, EventArgs e)
    {
        panelFound.Controls.Clear();
        
        foreach (var res in _resultsList)
        {
            res.Qualifies = res.Qualify((ShinyState)comboWantShiny.SelectedValue, (State)comboWantAlpha.SelectedValue, (Gender)comboWantGender.SelectedValue, (int)numericWantMax.Value);
            if (res.Qualifies)
                PrintResult(res.Permute, res.Entity);
        }
        MessageBox.Show($@"Done updating found pokemon!", @"Done", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void Result(PermuteResult permute)
    {
        var res = new Results()
        {
            Permute = permute,
            Entity = permute.Entity
        };
        res.Qualifies = res.Qualify((ShinyState)comboWantShiny.SelectedValue, (State)comboWantAlpha.SelectedValue, (Gender)comboWantGender.SelectedValue, (int)numericWantMax.Value );
        _resultsList.Add(res);
    }

    private void PrintResult(PermuteResult permute, EntityResult entity)
    {
        // Container Panel
        var pan = new Panel
        {
            Location = new Point(3, panelFound.Controls.Count * 36 + 5),
            Size = new Size(602, 31),
            BackColor = SystemColors.ControlLight
        };

        // Name
        var name = GenBox(3, 0, SpeciesName.GetSpeciesName(entity.Species, 2));
        name.Click += (_, _) => ResultClick(permute, entity);
        pan.Controls.Add(name);

        // Spawn
        var index = permute.Advances.Count(adv => adv == Advance.CR);
        pan.Controls.Add(GenBox(2, 3, (permute.IsBonus ? (index == 1 ? "BS" : $"W{index}-") : "SP") + entity.Index, 
            tooltip: (permute.IsBonus ? (index == 1 ? "Bonus Spawn " : $"Wave{index} ") : "Spawn") + entity.Index));

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
        pan.Controls.Add(GenBox(1,8, 
            entity.IsShiny ? entity.ShinyXor == 0 ? "\u25FC" : "\u2606" : "-",
            tooltip: entity.IsShiny ? entity.ShinyXor == 0 ? 
                "Will be a square shiny in future games that support it" :
                "Shiny pokemon" : "NOT Shiny"));

        // Steps
        var i = 9;
        AdvanceExtensions.Raw = false;
        foreach (var advance in permute.Advances)
        {
            pan.Controls.Add(GenBox(1, i, advance.ToString(),
                StaticUtils.AdvanceColor(advance), advance.GetName()));
            i++;
        }

        // Feasibility
        var (show, good, desc, tooltip) = GetFeasibility(permute.Advances);
        if (show)
        {
            pan.Controls.Add(GenBox(3, i, desc, 
                good ? Color.FromArgb(186, 255, 201) : Color.FromArgb(255, 80, 72), 
                tooltip));
        }

        panelFound.Controls.Add(pan);
    }
    
    public static (bool show, bool good, string descShort, string descLong) GetFeasibility(ReadOnlySpan<Advance> advances)
    {
        if (advances.IsAny(AdvanceExtensions.IsMultiScare))
        {

            return (true, false, advances.IsAny(AdvanceExtensions.IsMultiBeta) ? @"SK: MAgg" : @"SK: Multi",
                advances.IsAny(AdvanceExtensions.IsMultiBeta) ? 
                    @"Pokemon is Multi-scare-Skittish, path only possible by multi scaring with other aggressive pokemon." :
                    @"Pokemon is skittish, path is possible by multi scaring all skittish pokemon.");
        }

        if (advances.IsAny(AdvanceExtensions.IsMultiBeta))
            return (true, false, @"SK: Agg", @"Pokemon is Skittish, path only possible with other aggressive pokemon.");

        if (advances.IsAny(z => z == Advance.B1))
        {
            return !advances.IsAny(AdvanceExtensions.IsMultiAggressive) ?
                (true, true, @"SK: Single", @"Pokemon is skittish, path is possible by catching all skittish pokemon one by one.") : 
                (true, false, @"SK: MoAg", @"Pokemon is skittish, Mostly aggressive!");
        }

        if (advances.IsAny(AdvanceExtensions.IsMultiOblivious))
            return (true, false, @"OB: Agg", @"Pokemon is oblivious, path only possible by multi battling with other aggressive pokemon.");

        if (advances.IsAny(AdvanceExtensions.IsMultiAggressive))
            return (false, true, string.Empty, string.Empty);

        return (true, true, @"Single",
            @"Single advances only, can be completed with catching pokemon one by one.");
    }

    /// <summary>
    /// Generate TexBox with standard unit width, height, and location.
    /// 1 width = 23*23
    /// </summary>
    /// <param name="width">Width of the box in standard units</param>
    /// <param name="loc">Location of the box in standard units</param>
    /// <param name="text">TextBox.Text property</param>
    /// <param name="backColor">TextBox.BackColor property</param>
    /// <param name="tooltip">Tooltip text</param>
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
        box.MouseMove += (_, _) => toolTip.SetToolTip(box, tooltip);
        return box;
    }
}