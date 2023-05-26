namespace EVDiss
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            noOfAgentLabel = new Label();
            observationsLabel = new Label();
            initialise = new CheckBox();
            gridSearchCheck = new CheckBox();
            trainBtn = new Button();
            noOfAgentsInput = new NumericUpDown();
            observationsInput = new NumericUpDown();
            label2 = new Label();
            config = new TextBox();
            label3 = new Label();
            wait = new CheckBox();
            resetOnGoal = new CheckBox();
            label1 = new Label();
            tensorboardBtn = new Button();
            finalCmd = new TextBox();
            generate = new Button();
            colorDialog1 = new ColorDialog();
            versionInput = new NumericUpDown();
            versionBox = new CheckBox();
            button1 = new Button();
            label4 = new Label();
            initOpts = new ComboBox();
            refreshBtn = new Button();
            buildFolderOpts = new ComboBox();
            envOpts = new ComboBox();
            label5 = new Label();
            paddingDropDown = new ComboBox();
            paddingCheck = new CheckBox();
            label6 = new Label();
            runIDBox = new TextBox();
            breadcrumbCheck = new CheckBox();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            rewardPenaltyBox = new NumericUpDown();
            evaporationDelayBox = new NumericUpDown();
            penaltyDelayBox = new NumericUpDown();
            timeScaleCheck = new CheckBox();
            timeScaleNum = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)noOfAgentsInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)observationsInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)versionInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)rewardPenaltyBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)evaporationDelayBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)penaltyDelayBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)timeScaleNum).BeginInit();
            SuspendLayout();
            // 
            // noOfAgentLabel
            // 
            noOfAgentLabel.AutoSize = true;
            noOfAgentLabel.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            noOfAgentLabel.Location = new Point(6, 8);
            noOfAgentLabel.Name = "noOfAgentLabel";
            noOfAgentLabel.Size = new Size(167, 25);
            noOfAgentLabel.TabIndex = 1;
            noOfAgentLabel.Text = "Number of Agents";
            // 
            // observationsLabel
            // 
            observationsLabel.Location = new Point(0, 0);
            observationsLabel.Name = "observationsLabel";
            observationsLabel.Size = new Size(100, 23);
            observationsLabel.TabIndex = 0;
            // 
            // initialise
            // 
            initialise.AutoSize = true;
            initialise.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            initialise.Location = new Point(14, 216);
            initialise.Name = "initialise";
            initialise.Size = new Size(156, 29);
            initialise.TabIndex = 6;
            initialise.Text = "Initialise From";
            initialise.UseVisualStyleBackColor = true;
            // 
            // gridSearchCheck
            // 
            gridSearchCheck.AutoSize = true;
            gridSearchCheck.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            gridSearchCheck.Location = new Point(13, 84);
            gridSearchCheck.Name = "gridSearchCheck";
            gridSearchCheck.Size = new Size(133, 29);
            gridSearchCheck.TabIndex = 7;
            gridSearchCheck.Text = "Grid Search";
            gridSearchCheck.UseVisualStyleBackColor = true;
            gridSearchCheck.CheckedChanged += gridSearchCheck_CheckedChanged;
            // 
            // trainBtn
            // 
            trainBtn.BackColor = Color.Lime;
            trainBtn.FlatStyle = FlatStyle.System;
            trainBtn.ForeColor = SystemColors.ControlText;
            trainBtn.Location = new Point(322, 715);
            trainBtn.Name = "trainBtn";
            trainBtn.Size = new Size(298, 45);
            trainBtn.TabIndex = 8;
            trainBtn.Text = "TRAIN";
            trainBtn.UseVisualStyleBackColor = false;
            trainBtn.Click += trainBtn_Click;
            // 
            // noOfAgentsInput
            // 
            noOfAgentsInput.Location = new Point(11, 37);
            noOfAgentsInput.Name = "noOfAgentsInput";
            noOfAgentsInput.Size = new Size(180, 31);
            noOfAgentsInput.TabIndex = 9;
            noOfAgentsInput.ValueChanged += noOfAgentsInput_ValueChanged;
            // 
            // observationsInput
            // 
            observationsInput.AutoSize = true;
            observationsInput.Location = new Point(440, 36);
            observationsInput.Name = "observationsInput";
            observationsInput.Size = new Size(180, 31);
            observationsInput.TabIndex = 10;
            observationsInput.ValueChanged += observationsInput_ValueChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(9, 297);
            label2.Name = "label2";
            label2.Size = new Size(113, 25);
            label2.TabIndex = 11;
            label2.Text = "Build Folder";
            // 
            // config
            // 
            config.Location = new Point(437, 170);
            config.Name = "config";
            config.Size = new Size(183, 31);
            config.TabIndex = 13;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(436, 137);
            label3.Name = "label3";
            label3.Size = new Size(67, 25);
            label3.TabIndex = 14;
            label3.Text = "Config";
            // 
            // wait
            // 
            wait.AutoSize = true;
            wait.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            wait.Location = new Point(147, 84);
            wait.Name = "wait";
            wait.Size = new Size(75, 29);
            wait.TabIndex = 15;
            wait.Text = "Wait";
            wait.UseVisualStyleBackColor = true;
            wait.CheckedChanged += wait_CheckedChanged;
            // 
            // resetOnGoal
            // 
            resetOnGoal.AutoSize = true;
            resetOnGoal.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            resetOnGoal.Location = new Point(226, 84);
            resetOnGoal.Name = "resetOnGoal";
            resetOnGoal.Size = new Size(157, 29);
            resetOnGoal.TabIndex = 16;
            resetOnGoal.Text = "Reset On Goal";
            resetOnGoal.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(437, 8);
            label1.Name = "label1";
            label1.Size = new Size(123, 25);
            label1.TabIndex = 17;
            label1.Text = "Observations";
            // 
            // tensorboardBtn
            // 
            tensorboardBtn.BackColor = Color.Lime;
            tensorboardBtn.FlatStyle = FlatStyle.System;
            tensorboardBtn.Location = new Point(6, 816);
            tensorboardBtn.Name = "tensorboardBtn";
            tensorboardBtn.Size = new Size(302, 45);
            tensorboardBtn.TabIndex = 18;
            tensorboardBtn.Text = "LAUNCH TENSORBOARD";
            tensorboardBtn.UseVisualStyleBackColor = false;
            tensorboardBtn.Click += tensorboardBtn_Click;
            // 
            // finalCmd
            // 
            finalCmd.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            finalCmd.Location = new Point(13, 599);
            finalCmd.Multiline = true;
            finalCmd.Name = "finalCmd";
            finalCmd.ScrollBars = ScrollBars.Vertical;
            finalCmd.Size = new Size(607, 106);
            finalCmd.TabIndex = 19;
            // 
            // generate
            // 
            generate.BackColor = Color.Lime;
            generate.FlatStyle = FlatStyle.System;
            generate.ForeColor = SystemColors.ControlText;
            generate.Location = new Point(9, 714);
            generate.Name = "generate";
            generate.Size = new Size(307, 45);
            generate.TabIndex = 20;
            generate.Text = "GENERATE";
            generate.UseVisualStyleBackColor = false;
            generate.Click += generate_Click;
            // 
            // versionInput
            // 
            versionInput.Location = new Point(214, 37);
            versionInput.Name = "versionInput";
            versionInput.Size = new Size(194, 31);
            versionInput.TabIndex = 22;
            versionInput.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // versionBox
            // 
            versionBox.AutoSize = true;
            versionBox.BackColor = Color.Transparent;
            versionBox.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            versionBox.Location = new Point(215, 7);
            versionBox.Name = "versionBox";
            versionBox.Size = new Size(101, 29);
            versionBox.TabIndex = 23;
            versionBox.Text = "Version";
            versionBox.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ButtonFace;
            button1.FlatAppearance.BorderColor = Color.DarkOliveGreen;
            button1.FlatAppearance.BorderSize = 5;
            button1.FlatStyle = FlatStyle.System;
            button1.ForeColor = SystemColors.ControlText;
            button1.Location = new Point(9, 765);
            button1.Name = "button1";
            button1.Size = new Size(611, 45);
            button1.TabIndex = 24;
            button1.Text = "GENERATE + TRAIN";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(11, 570);
            label4.Name = "label4";
            label4.Size = new Size(97, 25);
            label4.TabIndex = 25;
            label4.Text = "Command";
            // 
            // initOpts
            // 
            initOpts.FormattingEnabled = true;
            initOpts.Location = new Point(13, 251);
            initOpts.Name = "initOpts";
            initOpts.Size = new Size(607, 33);
            initOpts.TabIndex = 26;
            initOpts.SelectedIndexChanged += initOpts_SelectedIndexChanged;
            // 
            // refreshBtn
            // 
            refreshBtn.BackColor = Color.Lime;
            refreshBtn.FlatStyle = FlatStyle.System;
            refreshBtn.Location = new Point(322, 816);
            refreshBtn.Name = "refreshBtn";
            refreshBtn.Size = new Size(298, 45);
            refreshBtn.TabIndex = 27;
            refreshBtn.Text = "REFRESH DIRECTORIES";
            refreshBtn.UseVisualStyleBackColor = false;
            refreshBtn.Click += refreshBtn_Click;
            // 
            // buildFolderOpts
            // 
            buildFolderOpts.FormattingEnabled = true;
            buildFolderOpts.Location = new Point(13, 327);
            buildFolderOpts.Name = "buildFolderOpts";
            buildFolderOpts.Size = new Size(178, 33);
            buildFolderOpts.TabIndex = 28;
            buildFolderOpts.SelectedIndexChanged += buildFolderOpts_SelectedIndexChanged;
            // 
            // envOpts
            // 
            envOpts.FormattingEnabled = true;
            envOpts.Location = new Point(210, 327);
            envOpts.Name = "envOpts";
            envOpts.Size = new Size(410, 33);
            envOpts.TabIndex = 31;
            envOpts.SelectedIndexChanged += envOpts_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(210, 299);
            label5.Name = "label5";
            label5.Size = new Size(119, 25);
            label5.TabIndex = 30;
            label5.Text = "Environment";
            // 
            // paddingDropDown
            // 
            paddingDropDown.FormattingEnabled = true;
            paddingDropDown.Items.AddRange(new object[] { "Distance", "Zero" });
            paddingDropDown.Location = new Point(13, 168);
            paddingDropDown.Name = "paddingDropDown";
            paddingDropDown.Size = new Size(178, 33);
            paddingDropDown.TabIndex = 33;
            // 
            // paddingCheck
            // 
            paddingCheck.AutoSize = true;
            paddingCheck.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            paddingCheck.Location = new Point(14, 133);
            paddingCheck.Name = "paddingCheck";
            paddingCheck.Size = new Size(106, 29);
            paddingCheck.TabIndex = 34;
            paddingCheck.Text = "Padding";
            paddingCheck.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(9, 491);
            label6.Name = "label6";
            label6.Size = new Size(68, 25);
            label6.TabIndex = 36;
            label6.Text = "Run ID";
            // 
            // runIDBox
            // 
            runIDBox.Location = new Point(13, 520);
            runIDBox.Name = "runIDBox";
            runIDBox.Size = new Size(607, 31);
            runIDBox.TabIndex = 35;
            runIDBox.TextChanged += runIDBox_TextChanged;
            // 
            // breadcrumbCheck
            // 
            breadcrumbCheck.AutoSize = true;
            breadcrumbCheck.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            breadcrumbCheck.Location = new Point(14, 379);
            breadcrumbCheck.Name = "breadcrumbCheck";
            breadcrumbCheck.Size = new Size(147, 29);
            breadcrumbCheck.TabIndex = 37;
            breadcrumbCheck.Text = "Breadcrumbs";
            breadcrumbCheck.UseVisualStyleBackColor = true;
            breadcrumbCheck.CheckedChanged += breadcrumbCheck_CheckedChanged;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label7.Location = new Point(11, 422);
            label7.Name = "label7";
            label7.Size = new Size(140, 25);
            label7.TabIndex = 41;
            label7.Text = "Reward Penalty";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label8.Location = new Point(210, 422);
            label8.Name = "label8";
            label8.Size = new Size(162, 25);
            label8.TabIndex = 42;
            label8.Text = "Evaporation Delay";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label9.Location = new Point(436, 422);
            label9.Name = "label9";
            label9.Size = new Size(124, 25);
            label9.TabIndex = 43;
            label9.Text = "Penalty Delay";
            // 
            // rewardPenaltyBox
            // 
            rewardPenaltyBox.DecimalPlaces = 4;
            rewardPenaltyBox.Increment = new decimal(new int[] { 5, 0, 0, 262144 });
            rewardPenaltyBox.Location = new Point(14, 450);
            rewardPenaltyBox.Minimum = new decimal(new int[] { 1, 0, 0, int.MinValue });
            rewardPenaltyBox.Name = "rewardPenaltyBox";
            rewardPenaltyBox.Size = new Size(177, 31);
            rewardPenaltyBox.TabIndex = 44;
            rewardPenaltyBox.Value = new decimal(new int[] { 25, 0, 0, -2147221504 });
            // 
            // evaporationDelayBox
            // 
            evaporationDelayBox.Location = new Point(214, 450);
            evaporationDelayBox.Name = "evaporationDelayBox";
            evaporationDelayBox.Size = new Size(194, 31);
            evaporationDelayBox.TabIndex = 45;
            evaporationDelayBox.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // penaltyDelayBox
            // 
            penaltyDelayBox.DecimalPlaces = 2;
            penaltyDelayBox.Increment = new decimal(new int[] { 25, 0, 0, 131072 });
            penaltyDelayBox.Location = new Point(437, 450);
            penaltyDelayBox.Name = "penaltyDelayBox";
            penaltyDelayBox.Size = new Size(177, 31);
            penaltyDelayBox.TabIndex = 46;
            penaltyDelayBox.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // timeScaleCheck
            // 
            timeScaleCheck.AutoSize = true;
            timeScaleCheck.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            timeScaleCheck.Location = new Point(215, 133);
            timeScaleCheck.Name = "timeScaleCheck";
            timeScaleCheck.Size = new Size(126, 29);
            timeScaleCheck.TabIndex = 47;
            timeScaleCheck.Text = "Time Scale";
            timeScaleCheck.UseVisualStyleBackColor = true;
            // 
            // timeScaleNum
            // 
            timeScaleNum.Location = new Point(215, 168);
            timeScaleNum.Name = "timeScaleNum";
            timeScaleNum.Size = new Size(194, 31);
            timeScaleNum.TabIndex = 48;
            timeScaleNum.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(632, 879);
            Controls.Add(timeScaleNum);
            Controls.Add(timeScaleCheck);
            Controls.Add(penaltyDelayBox);
            Controls.Add(evaporationDelayBox);
            Controls.Add(rewardPenaltyBox);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(breadcrumbCheck);
            Controls.Add(label6);
            Controls.Add(runIDBox);
            Controls.Add(paddingCheck);
            Controls.Add(paddingDropDown);
            Controls.Add(envOpts);
            Controls.Add(label5);
            Controls.Add(buildFolderOpts);
            Controls.Add(refreshBtn);
            Controls.Add(initOpts);
            Controls.Add(label4);
            Controls.Add(button1);
            Controls.Add(versionBox);
            Controls.Add(versionInput);
            Controls.Add(generate);
            Controls.Add(finalCmd);
            Controls.Add(tensorboardBtn);
            Controls.Add(label1);
            Controls.Add(resetOnGoal);
            Controls.Add(wait);
            Controls.Add(label3);
            Controls.Add(config);
            Controls.Add(label2);
            Controls.Add(noOfAgentsInput);
            Controls.Add(trainBtn);
            Controls.Add(gridSearchCheck);
            Controls.Add(initialise);
            Controls.Add(observationsInput);
            Controls.Add(noOfAgentLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "RL Agent Toolkit";
            FormClosed += Form1_FormClose;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)noOfAgentsInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)observationsInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)versionInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)rewardPenaltyBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)evaporationDelayBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)penaltyDelayBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)timeScaleNum).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label noOfAgentLabel;
        private Label observationsLabel;
        private CheckBox initialise;
        private CheckBox gridSearchCheck;
        private Button trainBtn;
        private NumericUpDown noOfAgentsInput;
        private NumericUpDown observationsInput;
        private Label label2;
        private TextBox config;
        private Label label3;
        private CheckBox wait;
        private CheckBox resetOnGoal;
        private Label label1;
        private Button tensorboardBtn;
        private TextBox finalCmd;
        private Button generate;
        private ColorDialog colorDialog1;
        private NumericUpDown versionInput;
        private CheckBox versionBox;
        private Button button1;
        private Label label4;
        private ComboBox initOpts;
        private Button refreshBtn;
        private ComboBox buildFolderOpts;
        private ComboBox envOpts;
        private Label label5;
        private ComboBox paddingDropDown;
        private CheckBox paddingCheck;
        private Label label6;
        private TextBox runIDBox;
        private CheckBox breadcrumbCheck;
        private Label label7;
        private Label label8;
        private Label label9;
        private NumericUpDown rewardPenaltyBox;
        private NumericUpDown evaporationDelayBox;
        private NumericUpDown penaltyDelayBox;
        private CheckBox timeScaleCheck;
        private NumericUpDown timeScaleNum;
    }
}