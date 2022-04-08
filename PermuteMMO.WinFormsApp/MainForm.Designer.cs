namespace PermuteMMO.WinFormsApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonEtumrep = new System.Windows.Forms.Button();
            this.checkBoxMMO = new System.Windows.Forms.CheckBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numericWantMax = new System.Windows.Forms.NumericUpDown();
            this.comboWantGender = new System.Windows.Forms.ComboBox();
            this.checkWantSquare = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checkWantAlpha = new System.Windows.Forms.CheckBox();
            this.checkWantShiny = new System.Windows.Forms.CheckBox();
            this.buttonJSON = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxWarn = new System.Windows.Forms.TextBox();
            this.buttonPermutate = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBoxBonus = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxAlpha = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxBase = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numericSpawns2 = new System.Windows.Forms.NumericUpDown();
            this.comboBoxSpecies2 = new System.Windows.Forms.ComboBox();
            this.comboBoxLoc = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericSpawns = new System.Windows.Forms.NumericUpDown();
            this.comboBoxSpecies = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSeed = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panelFound = new System.Windows.Forms.Panel();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericWantMax)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSpawns2)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSpawns)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.buttonEtumrep);
            this.panel1.Controls.Add(this.checkBoxMMO);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.buttonJSON);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.textBoxWarn);
            this.panel1.Controls.Add(this.buttonPermutate);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.textBoxSeed);
            this.panel1.Location = new System.Drawing.Point(14, 16);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(736, 259);
            this.panel1.TabIndex = 0;
            // 
            // buttonEtumrep
            // 
            this.buttonEtumrep.Location = new System.Drawing.Point(239, 8);
            this.buttonEtumrep.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonEtumrep.Name = "buttonEtumrep";
            this.buttonEtumrep.Size = new System.Drawing.Size(51, 31);
            this.buttonEtumrep.TabIndex = 10;
            this.buttonEtumrep.Text = "FIND";
            this.buttonEtumrep.UseVisualStyleBackColor = true;
            this.buttonEtumrep.Click += new System.EventHandler(this.buttonEtumrep_Click);
            // 
            // checkBoxMMO
            // 
            this.checkBoxMMO.AutoSize = true;
            this.checkBoxMMO.Location = new System.Drawing.Point(7, 48);
            this.checkBoxMMO.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBoxMMO.Name = "checkBoxMMO";
            this.checkBoxMMO.Size = new System.Drawing.Size(279, 24);
            this.checkBoxMMO.TabIndex = 0;
            this.checkBoxMMO.Text = "Massive Mass Outbreak (MMO) mode";
            this.checkBoxMMO.UseVisualStyleBackColor = true;
            this.checkBoxMMO.CheckedChanged += new System.EventHandler(this.checkBoxMMO_CheckedChanged);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.Control;
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.numericWantMax);
            this.panel5.Controls.Add(this.comboWantGender);
            this.panel5.Controls.Add(this.checkWantSquare);
            this.panel5.Controls.Add(this.textBox1);
            this.panel5.Controls.Add(this.button1);
            this.panel5.Controls.Add(this.checkWantAlpha);
            this.panel5.Controls.Add(this.checkWantShiny);
            this.panel5.Location = new System.Drawing.Point(3, 209);
            this.panel5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(727, 41);
            this.panel5.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(355, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 20);
            this.label9.TabIndex = 18;
            this.label9.Text = "Gender:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(487, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 20);
            this.label8.TabIndex = 17;
            this.label8.Text = "Max rolls:";
            // 
            // numericWantMax
            // 
            this.numericWantMax.Location = new System.Drawing.Point(560, 7);
            this.numericWantMax.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericWantMax.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericWantMax.Name = "numericWantMax";
            this.numericWantMax.Size = new System.Drawing.Size(57, 27);
            this.numericWantMax.TabIndex = 16;
            this.numericWantMax.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // comboWantGender
            // 
            this.comboWantGender.FormattingEnabled = true;
            this.comboWantGender.Items.AddRange(new object[] {
            "ANY",
            "M",
            "F",
            "/"});
            this.comboWantGender.Location = new System.Drawing.Point(417, 5);
            this.comboWantGender.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboWantGender.Name = "comboWantGender";
            this.comboWantGender.Size = new System.Drawing.Size(62, 28);
            this.comboWantGender.TabIndex = 15;
            this.comboWantGender.Text = "ANY";
            // 
            // checkWantSquare
            // 
            this.checkWantSquare.AutoSize = true;
            this.checkWantSquare.Location = new System.Drawing.Point(206, 8);
            this.checkWantSquare.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkWantSquare.Name = "checkWantSquare";
            this.checkWantSquare.Size = new System.Drawing.Size(77, 24);
            this.checkWantSquare.TabIndex = 14;
            this.checkWantSquare.Text = "Square";
            this.checkWantSquare.UseVisualStyleBackColor = true;
            this.checkWantSquare.CheckedChanged += new System.EventHandler(this.checkWantSquare_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox1.Location = new System.Drawing.Point(3, 5);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(125, 27);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "Wanted pokémon";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(683, 5);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(39, 31);
            this.button1.TabIndex = 13;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // checkWantAlpha
            // 
            this.checkWantAlpha.AutoSize = true;
            this.checkWantAlpha.Location = new System.Drawing.Point(283, 8);
            this.checkWantAlpha.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkWantAlpha.Name = "checkWantAlpha";
            this.checkWantAlpha.Size = new System.Drawing.Size(70, 24);
            this.checkWantAlpha.TabIndex = 12;
            this.checkWantAlpha.Text = "Alpha";
            this.checkWantAlpha.UseVisualStyleBackColor = true;
            // 
            // checkWantShiny
            // 
            this.checkWantShiny.AutoSize = true;
            this.checkWantShiny.Checked = true;
            this.checkWantShiny.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkWantShiny.Location = new System.Drawing.Point(136, 8);
            this.checkWantShiny.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkWantShiny.Name = "checkWantShiny";
            this.checkWantShiny.Size = new System.Drawing.Size(66, 24);
            this.checkWantShiny.TabIndex = 11;
            this.checkWantShiny.Text = "Shiny";
            this.checkWantShiny.UseVisualStyleBackColor = true;
            // 
            // buttonJSON
            // 
            this.buttonJSON.Location = new System.Drawing.Point(591, 127);
            this.buttonJSON.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonJSON.Name = "buttonJSON";
            this.buttonJSON.Size = new System.Drawing.Size(139, 31);
            this.buttonJSON.TabIndex = 1;
            this.buttonJSON.Text = "Use JSON data";
            this.buttonJSON.UseVisualStyleBackColor = true;
            this.buttonJSON.Click += new System.EventHandler(this.buttonJSON_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 20);
            this.label7.TabIndex = 8;
            this.label7.Text = "Seed:";
            // 
            // textBoxWarn
            // 
            this.textBoxWarn.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxWarn.ForeColor = System.Drawing.SystemColors.Control;
            this.textBoxWarn.Location = new System.Drawing.Point(591, 8);
            this.textBoxWarn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxWarn.Multiline = true;
            this.textBoxWarn.Name = "textBoxWarn";
            this.textBoxWarn.ReadOnly = true;
            this.textBoxWarn.Size = new System.Drawing.Size(139, 113);
            this.textBoxWarn.TabIndex = 9;
            this.textBoxWarn.Text = "MMOs currently not fully operational in GUI mode. Use the website to get the spaw" +
    "n tables.";
            this.textBoxWarn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonPermutate
            // 
            this.buttonPermutate.Location = new System.Drawing.Point(591, 167);
            this.buttonPermutate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonPermutate.Name = "buttonPermutate";
            this.buttonPermutate.Size = new System.Drawing.Size(139, 31);
            this.buttonPermutate.TabIndex = 6;
            this.buttonPermutate.Text = "Permutate!";
            this.buttonPermutate.UseVisualStyleBackColor = true;
            this.buttonPermutate.Click += new System.EventHandler(this.buttonPermutate_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.textBoxBonus);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.checkBoxAlpha);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.textBoxBase);
            this.panel3.Controls.Add(this.textBox3);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.numericSpawns2);
            this.panel3.Controls.Add(this.comboBoxSpecies2);
            this.panel3.Controls.Add(this.comboBoxLoc);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Location = new System.Drawing.Point(297, 4);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(287, 197);
            this.panel3.TabIndex = 5;
            // 
            // textBoxBonus
            // 
            this.textBoxBonus.Enabled = false;
            this.textBoxBonus.Location = new System.Drawing.Point(3, 124);
            this.textBoxBonus.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxBonus.Name = "textBoxBonus";
            this.textBoxBonus.PlaceholderText = "Bonus Table";
            this.textBoxBonus.Size = new System.Drawing.Size(279, 27);
            this.textBoxBonus.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Alphas only?";
            this.label5.Visible = false;
            // 
            // checkBoxAlpha
            // 
            this.checkBoxAlpha.AutoSize = true;
            this.checkBoxAlpha.Enabled = false;
            this.checkBoxAlpha.Location = new System.Drawing.Point(136, 97);
            this.checkBoxAlpha.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBoxAlpha.Name = "checkBoxAlpha";
            this.checkBoxAlpha.Size = new System.Drawing.Size(18, 17);
            this.checkBoxAlpha.TabIndex = 5;
            this.checkBoxAlpha.UseVisualStyleBackColor = true;
            this.checkBoxAlpha.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(131, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "Outbreak location:";
            // 
            // textBoxBase
            // 
            this.textBoxBase.Enabled = false;
            this.textBoxBase.Location = new System.Drawing.Point(3, 85);
            this.textBoxBase.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxBase.Name = "textBoxBase";
            this.textBoxBase.PlaceholderText = "Base Table";
            this.textBoxBase.Size = new System.Drawing.Size(279, 27);
            this.textBoxBase.TabIndex = 10;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox3.Location = new System.Drawing.Point(3, 4);
            this.textBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(279, 27);
            this.textBox3.TabIndex = 4;
            this.textBox3.Text = "MMO & second wave info";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Number of spawns:\r\n";
            // 
            // numericSpawns2
            // 
            this.numericSpawns2.Enabled = false;
            this.numericSpawns2.Location = new System.Drawing.Point(136, 163);
            this.numericSpawns2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericSpawns2.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numericSpawns2.Name = "numericSpawns2";
            this.numericSpawns2.Size = new System.Drawing.Size(147, 27);
            this.numericSpawns2.TabIndex = 2;
            // 
            // comboBoxSpecies2
            // 
            this.comboBoxSpecies2.Enabled = false;
            this.comboBoxSpecies2.FormattingEnabled = true;
            this.comboBoxSpecies2.Location = new System.Drawing.Point(136, 124);
            this.comboBoxSpecies2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxSpecies2.Name = "comboBoxSpecies2";
            this.comboBoxSpecies2.Size = new System.Drawing.Size(147, 28);
            this.comboBoxSpecies2.TabIndex = 1;
            this.comboBoxSpecies2.Visible = false;
            // 
            // comboBoxLoc
            // 
            this.comboBoxLoc.Enabled = false;
            this.comboBoxLoc.FormattingEnabled = true;
            this.comboBoxLoc.Items.AddRange(new object[] {
            "Obsidian Fieldlands",
            "Crimson Mirelands",
            "Colbalt Coastlands",
            "Coronet Highlands",
            "Alabaster Icelands"});
            this.comboBoxLoc.Location = new System.Drawing.Point(136, 47);
            this.comboBoxLoc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxLoc.Name = "comboBoxLoc";
            this.comboBoxLoc.Size = new System.Drawing.Size(147, 28);
            this.comboBoxLoc.TabIndex = 2;
            this.comboBoxLoc.Text = "Obsidian Fieldlands";
            this.comboBoxLoc.SelectedIndexChanged += new System.EventHandler(this.comboBoxLoc_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Pokémon species:";
            this.label4.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.textBox2);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.numericSpawns);
            this.panel2.Controls.Add(this.comboBoxSpecies);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(3, 81);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(287, 120);
            this.panel2.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox2.Location = new System.Drawing.Point(3, 4);
            this.textBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(279, 27);
            this.textBox2.TabIndex = 4;
            this.textBox2.Text = "Outbreak info";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Number of spawns:\r\n";
            // 
            // numericSpawns
            // 
            this.numericSpawns.Location = new System.Drawing.Point(136, 85);
            this.numericSpawns.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numericSpawns.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericSpawns.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericSpawns.Name = "numericSpawns";
            this.numericSpawns.Size = new System.Drawing.Size(147, 27);
            this.numericSpawns.TabIndex = 2;
            this.numericSpawns.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // comboBoxSpecies
            // 
            this.comboBoxSpecies.DisplayMember = "Text";
            this.comboBoxSpecies.FormattingEnabled = true;
            this.comboBoxSpecies.Location = new System.Drawing.Point(136, 47);
            this.comboBoxSpecies.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxSpecies.Name = "comboBoxSpecies";
            this.comboBoxSpecies.Size = new System.Drawing.Size(147, 28);
            this.comboBoxSpecies.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pokémon species:";
            // 
            // textBoxSeed
            // 
            this.textBoxSeed.Location = new System.Drawing.Point(57, 8);
            this.textBoxSeed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxSeed.Name = "textBoxSeed";
            this.textBoxSeed.PlaceholderText = "16045686375800883966";
            this.textBoxSeed.Size = new System.Drawing.Size(174, 27);
            this.textBoxSeed.TabIndex = 1;
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "json";
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel4.Controls.Add(this.panelFound);
            this.panel4.Controls.Add(this.textBox4);
            this.panel4.Location = new System.Drawing.Point(14, 284);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(736, 300);
            this.panel4.TabIndex = 1;
            // 
            // panelFound
            // 
            this.panelFound.AutoScroll = true;
            this.panelFound.BackColor = System.Drawing.SystemColors.Control;
            this.panelFound.Location = new System.Drawing.Point(3, 43);
            this.panelFound.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelFound.Name = "panelFound";
            this.panelFound.Size = new System.Drawing.Size(729, 253);
            this.panelFound.TabIndex = 15;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBox4.Location = new System.Drawing.Point(3, 4);
            this.textBox4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(729, 27);
            this.textBox4.TabIndex = 14;
            this.textBox4.Text = "Found Pokémon:";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 600);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Legends outbreak RNG: Permute and Etumrep (M)MO";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericWantMax)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSpawns2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericSpawns)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private ComboBox comboBoxLoc;
        private TextBox textBoxSeed;
        private CheckBox checkBoxMMO;
        private Panel panel2;
        private TextBox textBox2;
        private Label label2;
        private NumericUpDown numericSpawns;
        private ComboBox comboBoxSpecies;
        private Label label1;
        private Panel panel3;
        private Label label5;
        private CheckBox checkBoxAlpha;
        private TextBox textBox3;
        private Label label3;
        private NumericUpDown numericSpawns2;
        private ComboBox comboBoxSpecies2;
        private Label label4;
        private Button buttonPermutate;
        private Label label7;
        private Label label6;
        private TextBox textBoxWarn;
        private TextBox textBoxBonus;
        private TextBox textBoxBase;
        private Button buttonJSON;
        private OpenFileDialog openFileDialog;
        private Panel panel4;
        private CheckBox checkWantAlpha;
        private CheckBox checkWantShiny;
        private Button button1;
        private Panel panel5;
        private TextBox textBox1;
        private TextBox textBox4;
        private Panel panelFound;
        private CheckBox checkWantSquare;
        private ComboBox comboWantGender;
        private Label label9;
        private Label label8;
        private NumericUpDown numericWantMax;
        private Button buttonEtumrep;
        private FolderBrowserDialog folderBrowserDialog;
        private ToolTip toolTip;
    }
}