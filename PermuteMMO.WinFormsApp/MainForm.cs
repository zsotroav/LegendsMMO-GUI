using PermuteMMO.Lib;

namespace PermuteMMO.WinFormsApp
{
    public partial class MainForm : Form
    {
        public UserEnteredSpawnInfo spawner = new();
        public MainForm()
        {
            InitializeComponent();
            // TODO: Message
            // TODO: MMOSpawner
            // TODO: MOSpawner
            ApiPermuter.NoRes += NoRes;
            ApiPermuter.Done += Done;
        }

        private void checkBoxMMO_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMMO.Checked)
            {
                checkBoxAlpha.Enabled = true;
                comboBoxSpecies2.Enabled = true;
                numericSpawns2.Enabled = true;
            }
            else
            {
                checkBoxAlpha.Enabled = false;
                checkBoxAlpha.Checked = false;
                comboBoxSpecies2.Enabled = false;
                comboBoxSpecies2.Text = "";
                numericSpawns2.Enabled = false;
                numericSpawns2.Value = 0;
            }
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
            spawner.Species = 0;
            spawner.BaseCount = (int)numericSpawns.Value;
            spawner.BaseTable = "";
            spawner.BonusCount = (int)numericSpawns2.Value;
            spawner.BonusTable = "";

            ApiPermuter.PermuteSingle(spawner.GetSpawn(), spawner.GetSeed(), spawner.Species);
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
    }
}