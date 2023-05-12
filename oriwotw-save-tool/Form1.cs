using System.IO;
using System.Security;
using System.Text.RegularExpressions;

namespace oriwotw_save_tool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ResetFields();
            chooseFileDialog.Filter = "Ori Save Files (*.uberstate)|*.uberstate";
        }

        private readonly OpenFileDialog chooseFileDialog = new();

        private readonly Regex rg = new(@"\A[0-9]+");

        private string StatusText
        {
            set => this.statusTextLabel.Text = value;
            get => this.statusTextLabel.Text;
        }

        private string initialFilePath = "";
        private string initialFileName = "";
        private string directoryPath = "";
        private int initialFileIndex = -1;
        private string[] directoryFileNames = Array.Empty<string>();

        private void ResetFields()
        {
            this.initialFilePath = "";
            this.initialFileName = "";
            this.directoryPath = "";
            this.initialFileIndex = -1;
            this.directoryFileNames = Array.Empty<string>();

            this.readOnlyInitialFileTextBox.Text = "";
            this.downShiftButton.Enabled = false;
            this.deleteButton.Enabled = false;
            this.upShiftButton.Enabled = false;

            //   this.readonlyReplacementFileTextBox.Text = "";
            this.replaceButton.Enabled = false;
            this.insertButton.Enabled = false;
            this.addButton.Enabled = false;
        }

        private void ChooseInitialFileButton_Click(object sender, EventArgs e)
        {
            if (this.chooseFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    SetInitialFile(chooseFileDialog.FileName);
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private bool SetInitialFile(string pathToFile)
        {
            this.initialFilePath = pathToFile;

            FileInfo info = new(pathToFile);
            if (info.DirectoryName is null)
            {
                // This should never happen, but just in case....
                ResetFields();
                this.StatusText = "Error finding directory name";
                return false;
            }

            this.initialFileName = info.Name;
            this.directoryPath = info.DirectoryName;
            this.directoryFileNames = Directory.GetFiles(directoryPath, "*.uberstate").Select(path => new FileInfo(path).Name).ToArray();

            // fileNames is sorted the same way windows explorer sorts by name
            // When you choose a file to shift up, it will shift itself up, as well as all of the files that come after it
            // (based on how you see the files are ordered in windows explorer list mode, sorted alphabetically)
            // This means that if you have file structure that looks like this:
            //   001 - YStart to Glades
            //   001 - ZStart to Glades
            // And you choose to shift ZStart to glades up, it will NOT shift YStart to glades up with it, since Z start is later
            Array.Sort(this.directoryFileNames, new NumericComparer());

            if (!this.directoryFileNames.Any())
            {
                ResetFields();
                this.StatusText = "Could not find any uberstate files here";
                return false;
            }

            // Scan directory and make sure ALL files match the pattern properly
            foreach (string fileName in directoryFileNames)
            {
                Match match = rg.Match(fileName);
                if (!match.Success)
                {
                    ResetFields();
                    this.StatusText = "ALL uberstate files must match pattern '### - Name'";
                    return false;
                }
            }

            this.StatusText = "Choose a basic control, or select file to replace/insert/add";

            // Find start index
            for (int i = 0; i < directoryFileNames.Length; i++)
                if (directoryFileNames[i].Equals(initialFileName))
                    this.initialFileIndex = i;

            if (this.initialFileIndex == -1)
            {
                ResetFields();
                this.StatusText = $"Could not find start index";
                return false;
            }

            this.upShiftButton.Enabled = true;
            this.downShiftButton.Enabled = true;
            this.deleteButton.Enabled = true;
            this.readOnlyInitialFileTextBox.Text = initialFilePath;

            // TODO call ChooseReplacementFile if the readonlytextfield has text

            return true;
        }

        private void DownShiftButton_Click(object sender, EventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {

        }

        private void UpShiftButton_Click(object sender, EventArgs e)
        {

        }

        public void Shift(int direction)
        {
            // Map full path to just file names
            string[] fileNamesNew = new string[directoryFileNames.Length];

            if (direction == -1 && initialFileIndex == 0 && int.Parse(rg.Match(directoryFileNames[0]).Value) == 0)
            {
                ResetFields();
                this.StatusText = $"Cannot downshift starting 0";
                return;
            }

            // We know matches for sure exist here
            // fileNames is sorted the same way windows explorer sorts by name
            for (int i = 0; i < directoryFileNames.Length; i++)
            {
                string currFileName = directoryFileNames[i];
                fileNamesNew[i] = currFileName;

                Match match = rg.Match(currFileName);

                string oldPrefix = match.Value;
                int prefixAsInt = int.Parse(oldPrefix);

                if (i >= this.initialFileIndex)
                {
                    // Create a formatter that follows the same format they did
                    // (e.g: if they did "123" it will be "000" format, if they did "12" it will be "00" format)
                    string format = new('0', oldPrefix.Length);

                    // Change the prefix to be the old one, but up or down shifted by 1, then formatted the same way they formatted it
                    string newPrefix = (prefixAsInt + direction).ToString(format);

                    // Replace the 
                    string newFileName = currFileName.Replace(oldPrefix, newPrefix);

                    string oldFullPath = $"{directoryPath}\\{currFileName}";
                    string newFullPath = $"{directoryPath}\\{newFileName}";

                    File.Move(oldFullPath, newFullPath);
                }
            }

            ResetFields();
            this.StatusText = $"Done! Another one?";
        }


        private void ChooseReplacementFileButton_Click(object sender, EventArgs e)
        {

        }

        private void ReplaceButton_Click(object sender, EventArgs e)
        {

        }

        private void InsertButton_Click(object sender, EventArgs e)
        {

        }

        private void AddButton_Click(object sender, EventArgs e)
        {

        }

        private void statusTextLabel_Click(object sender, EventArgs e)
        {

        }
    }
}