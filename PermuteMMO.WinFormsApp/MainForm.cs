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
            ApiPermuter.NoRes += NoRes;
            ApiPermuter.Done += Done;
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
            PermuteMeta.SatisfyCriteria = (result, advances) => result.IsShiny;
            SpawnGenerator.MaxShinyRolls = spawn.Type is SpawnType.MMO ? 19 : 32;
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
            PermuteMeta.SatisfyCriteria = (result, advances) => result.IsShiny;
            SpawnGenerator.MaxShinyRolls = spawn.Type is SpawnType.MMO ? 19 : 32;
            ApiPermuter.PermuteSingle(spawn, spawner.GetSeed(), spawner.Species);
        }

        private static void NoRes(string extra)
        {
            MessageBox.Show($@"No results found{extra}\nThis means that your (M)MO will not have a shiny in any permutation.",
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
    }
}