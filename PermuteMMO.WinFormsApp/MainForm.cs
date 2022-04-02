using PermuteMMO.Lib;

namespace PermuteMMO.WinFormsApp
{
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

            PermuteMeta.SatisfyCriteria = (result, advances) => result.IsShiny;
        }

        private void checkBoxMMO_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMMO.Checked)
            {
                textBoxWarn.ForeColor = Color.DarkRed;
                textBoxBase.Enabled = true;
                textBoxBonus.Enabled = true;
                //
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
            SpawnGenerator.MaxShinyRolls = spawn.Type is SpawnType.MMO ? 19 : 32;
            panelFound.Controls.Clear();
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
            spawner.Species = (ushort)PKHeX.Core.SpeciesName.GetSpeciesID((string)comboBoxSpecies.SelectedValue);
            spawner.BaseCount = (int)numericSpawns.Value;
            spawner.BaseTable = checkBoxMMO.Enabled ? textBoxBase.Text : "0x0000000000000000";
            spawner.BonusCount = (int)numericSpawns2.Value;
            spawner.BonusTable = checkBoxMMO.Enabled ? textBoxBonus.Text : "0x0000000000000000";

            var spawn = spawner.GetSpawn();
            SpawnGenerator.MaxShinyRolls = spawn.Type is SpawnType.MMO ? 19 : 32;
            panelFound.Controls.Clear();
            ApiPermuter.PermuteSingle(spawn, spawner.GetSeed(), spawner.Species);
        }

        private static void NoRes(string extra)
        {
            MessageBox.Show($@"No results found{extra}
This means that your (M)MO will not have a desired pokemon any permutation.",
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

        private void checkWant_CheckedChanged(object sender, EventArgs e)
        {
            PermuteMeta.SatisfyCriteria = (result, advances) => 
                checkWantShiny.Checked ? result.IsShiny : !result.IsShiny && 
                checkBoxAlpha.Checked ? result.IsAlpha : !result.IsAlpha;
        }
        
        private void Result(PermuteResult permute, EntityResult entity)
        {
            // Container Panel
            var pan = new Panel();
            pan.Location = new Point(3, panelFound.Controls.Count * 36 + 5);
            pan.Size = new Size(600, 31);
            pan.BackColor = SystemColors.ControlLight;

            // Name
            var nameBox = new TextBox();
            nameBox.ReadOnly = true;
            nameBox.TextAlign = HorizontalAlignment.Center;
            nameBox.Location = new Point(4, 4);
            nameBox.Size = new Size(131, 23);
            nameBox.Text = entity.Name;
            pan.Controls.Add(nameBox);

            // Gender
            var genderBox = new TextBox();
            genderBox.ReadOnly = true;
            genderBox.TextAlign = HorizontalAlignment.Center;
            genderBox.Location = new Point(139, 4);
            genderBox.Size = new Size(23, 23);
            genderBox.Text = entity.Gender switch
            {
                2 => "/",
                1 => "F",
                _ => "M",
            };
            genderBox.BackColor = entity.Gender switch
            {
                2 => SystemColors.Control,
                1 => Color.FromArgb(255, 186, 225),
                _ => Color.FromArgb(186, 225, 255),
            };
            pan.Controls.Add(genderBox);

            // Alpha
            var alphaBox = new TextBox();
            alphaBox.ReadOnly = true;
            alphaBox.TextAlign = HorizontalAlignment.Center;
            alphaBox.Location = new Point(166, 4);
            alphaBox.Size = new Size(23, 23);
            alphaBox.Text = entity.IsAlpha ? "A" : "-";
            alphaBox.BackColor = entity.IsAlpha ? Color.FromArgb(255, 179, 186) : SystemColors.Control;
            pan.Controls.Add(alphaBox);

            // Shiny odds
            var shinyBox = new TextBox();
            shinyBox.ReadOnly = true;
            shinyBox.TextAlign = HorizontalAlignment.Center;
            shinyBox.Location = new Point(193, 4);
            shinyBox.Size = new Size(23, 23);
            shinyBox.Text = entity.IsShiny ? entity.RollCountUsed.ToString() : "-";
            pan.Controls.Add(shinyBox);

            // Shiny XOR
            var XORBox = new TextBox();
            XORBox.ReadOnly = true;
            XORBox.TextAlign = HorizontalAlignment.Center;
            XORBox.Location = new Point(220, 4);
            XORBox.Size = new Size(23, 23);
            XORBox.Text = entity.IsShiny ? entity.ShinyXor == 0 ? "\u25FC" : "*" : "-";
            pan.Controls.Add(XORBox);

            // Steps
            var i = 9;
            foreach (var advance in permute.Advances)
            {
                var ad = new TextBox();
                ad.BackColor = advance switch
                {
                    Advance.A1 => Color.FromArgb(186, 255, 201),
                    Advance.A2 or Advance.A3 or Advance.A4 => Color.FromArgb(255, 223, 186),
                    Advance.G1 or Advance.G2 or Advance.G3 => Color.FromArgb(255, 179, 186),
                    _ => SystemColors.Control
                };

                ad.ReadOnly = true;
                ad.TextAlign = HorizontalAlignment.Center;
                ad.Size = new Size(23, 23);
                ad.Location = new Point(i * 27 + 4, 4);
                ad.Text = advance.ToString();
                pan.Controls.Add(ad);
                i++;
            }
            panelFound.Controls.Add(pan);
        }
    }
}