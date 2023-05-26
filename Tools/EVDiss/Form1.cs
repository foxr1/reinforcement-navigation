using EVDiss.Properties;
using System.Diagnostics;

namespace EVDiss
{
    public partial class Form1 : Form
    {
        private bool loading = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void trainBtn_Click(object sender, EventArgs e)
        {
            train();
        }

        private void train()
        {
            string strCmdText;
            strCmdText = $"/C C:\\Users\\Ollie\\anaconda3\\Scripts\\activate.bat&F:&cd Unity\\ev_reinforcement_dissertation&venv\\Scripts\\activate&{this.finalCmd.Text}&PAUSE";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
        }

        private void Form1_FormClose(object sender, FormClosedEventArgs e)
        {
            // Copy window location to app settings
            Settings.Default.WindowLocation = this.Location;

            // Save settings
            Settings.Default.Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadApp();

            try
            {
                noOfAgentsInput.Value = (int)Properties.Settings.Default["AgentNo"];
                observationsInput.Value = (int)Properties.Settings.Default["Observations"];
                buildFolderOpts.SelectedIndex = (int)Properties.Settings.Default["BuildIndex"];
                envOpts.SelectedIndex = (int)Properties.Settings.Default["EnvIndex"];
                breadcrumbCheck.Checked = (bool)Properties.Settings.Default["BreadcrumbChecked"];
                initOpts.SelectedIndex = (int)Properties.Settings.Default["InitIndex"];
                this.Location = Settings.Default.WindowLocation;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadApp()
        {
            loading = true;

            getResultNames();
            getBuildNames();
            getEnvNames();

            loading = false;
        }

        private void getResultNames()
        {
            this.initOpts.Items.Clear();
            try
            {
                string rootPath = @"F:\Unity\ev_reinforcement_dissertation\results";
                string[] dirs = Directory.GetDirectories(rootPath, "*", SearchOption.TopDirectoryOnly).OrderByDescending(d => new DirectoryInfo(d).LastWriteTime).ToArray();

                foreach (string dir in dirs)
                {
                    this.initOpts.Items.Add(dir.Split("\\")[dir.Split("\\").Length - 1]);
                }
            }
            catch
            {

            }
        }

        private void getBuildNames()
        {
            this.buildFolderOpts.Items.Clear();
            try
            {
                string rootPath = $"F:\\Unity\\ev_reinforcement_dissertation\\Build";
                string[] dirs = Directory.GetDirectories(rootPath, "*", SearchOption.TopDirectoryOnly).OrderBy(d => new DirectoryInfo(d).Name).ToArray();

                foreach (string dir in dirs)
                {
                    this.buildFolderOpts.Items.Add(dir.Split("\\")[dir.Split("\\").Length - 1]);
                }
                this.buildFolderOpts.SelectedItem = this.buildFolderOpts.Items[0];
            }
            catch
            {

            }
        }

        private void getEnvNames()
        {
            this.envOpts.Items.Clear();
            try
            {
                string rootPath = $"F:\\Unity\\ev_reinforcement_dissertation\\Build\\{this.buildFolderOpts.SelectedItem.ToString()}";
                string[] dirs = Directory.GetDirectories(rootPath, "*", SearchOption.TopDirectoryOnly).OrderBy(d => new DirectoryInfo(d).Name).ToArray();

                foreach (string dir in dirs)
                {
                    this.envOpts.Items.Add(dir.Split("\\")[dir.Split("\\").Length - 1]);
                }
            }
            catch
            {

            }
        }

        private void noOfAgentsInput_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default["AgentNo"] = (int)noOfAgentsInput.Value;
            Properties.Settings.Default.Save();

            updateFields();
        }

        private void tensorboardBtn_Click(object sender, EventArgs e)
        {
            string strCmdText;
            strCmdText = $"/C C:\\Users\\Ollie\\anaconda3\\Scripts\\activate.bat&F:&cd Unity\\ev_reinforcement_dissertation&venv\\Scripts\\activate&tensorboard --logdir results&PAUSE";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
        }

        private void generate_Click(object sender, EventArgs e)
        {
            generateText(false);
        }

        private void generateText(bool changeRun)
        {
            string startString = "";
            if (gridSearchCheck.Checked)
            {
                startString = "python Python\\mlagents-learn.py";
            }
            else
            {
                startString = "mlagents-learn";
            }

            string resetString;
            if (resetOnGoal.Checked)
            {
                resetString = "_ResetOnGoal";
            }
            else if (breadcrumbCheck.Checked)
            {
                resetString = "";
            }
            else
            {
                resetString = "_NoResetOnGoal";
            }

            string waitString;
            if (wait.Checked)
            {
                waitString = "_Wait";
            }
            else if (breadcrumbCheck.Checked)
            {
                waitString = "";
            }
            else
            {
                waitString = "_NoWait";
            }

            string optString = "";
            if (gridSearchCheck.Checked)
            {
                optString = "_OPTS";
            }

            string initialiseString = "";
            if (initialise.Checked)
            {
                initialiseString = $"--initialize-from={initOpts.SelectedItem.ToString()}";
            }

            string versionString = "";
            if (versionBox.Checked)
            {
                versionString = $"_v{versionInput.Value}";
            }

            string paddingString = "";
            if (paddingCheck.Checked)
            {
                paddingString = $"_{paddingDropDown.SelectedItem.ToString()}Padding";
            }

            string breadcrumbString = "";
            if (breadcrumbCheck.Checked)
            {
                breadcrumbString = $"_Breadcrumbs={rewardPenaltyBox.Value}RewardPenalty{evaporationDelayBox.Value}EvapDelay{penaltyDelayBox.Value}PenaltyDelay";
            }
            else
            {
                breadcrumbString = "";
            }

            string timeScaleArg = "";
            string timeScaleString = "";
            if (timeScaleCheck.Checked)
            {
                timeScaleArg = $"--time-scale={timeScaleNum.Value}";
                timeScaleString = $"_{timeScaleNum.Value}TimeScale";
            }
            else
            {
                timeScaleArg = "";
                timeScaleString = "";
            }

            string runID = "";
            if (!changeRun)
            {
                runID = $"{noOfAgentsInput.Value}Agents{resetString}{waitString}_{observationsInput.Value}Obs{optString}{versionString}{paddingString}{breadcrumbString}{timeScaleString}";

                this.runIDBox.Text = runID;
            }
            else
            {
                runID = this.runIDBox.Text;
            }

            this.finalCmd.Text = $"{startString} config\\{this.config.Text}.yaml --run-id={runID} " +
                $"--env=Build\\{buildFolderOpts.SelectedItem.ToString()}\\{envOpts.SelectedItem.ToString()}\\reinforcement_dissertation.exe {initialiseString} {timeScaleArg}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            generateText(false);
            train();
        }

        private void observationsInput_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default["Observations"] = (int)observationsInput.Value;
            Properties.Settings.Default.Save();
            getEnvNames();
        }

        private void updateFields()
        {
            int num = (int)noOfAgentsInput.Value;
            if (num == 1)
            {
                if (gridSearchCheck.Checked)
                {
                    this.config.Text = "1AgentConfigOPTS";
                }
                else
                {
                    this.config.Text = "1AgentConfig";
                }
            }
            else if (num == 2)
            {
                if (gridSearchCheck.Checked)
                {
                    this.config.Text = "2AgentConfigOPTS";
                }
                else
                {
                    this.config.Text = "2AgentConfig";
                }
            }
            else if (num == 4)
            {
                if (gridSearchCheck.Checked)
                {
                    this.config.Text = "4AgentConfigOPTS";
                }
                else
                {
                    this.config.Text = "4AgentConfig";
                }
            }
        }

        private void wait_CheckedChanged(object sender, EventArgs e)
        {
            updateFields();
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            LoadApp();
        }

        private void gridSearchCheck_CheckedChanged(object sender, EventArgs e)
        {
            updateFields();
        }

        private void buildFolderOpts_SelectedIndexChanged(object sender, EventArgs e)
        {
            getEnvNames();

            if (!loading)
            {
                Properties.Settings.Default["BuildIndex"] = buildFolderOpts.SelectedIndex;
                Properties.Settings.Default.Save();
            }
        }

        private void runIDBox_TextChanged(object sender, EventArgs e)
        {
            generateText(true);
        }

        private void envOpts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!loading)
            {
                Properties.Settings.Default["EnvIndex"] = envOpts.SelectedIndex;
                Properties.Settings.Default.Save();
            }
        }

        private void breadcrumbCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (!loading)
            {
                Properties.Settings.Default["BreadcrumbChecked"] = breadcrumbCheck.Checked;
                Properties.Settings.Default.Save();
            }
        }

        private void initOpts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!loading)
            {
                Properties.Settings.Default["InitIndex"] = initOpts.SelectedIndex;
                Properties.Settings.Default.Save();
            }
        }
    }
}