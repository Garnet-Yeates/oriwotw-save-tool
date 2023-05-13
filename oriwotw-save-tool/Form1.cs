using System.Text;
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
        }

        private string initialFilePath = "";
        private string initialFileName = "";
        private string directoryPath = "";
        private int initialFileIndex = -1;
        private int MaxIndex => directoryFileNames.Length - 1;
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
            if (File.Exists(InitialFileSavePath))
            {
                try
                {
                    using StreamReader sr = File.OpenText(InitialFileSavePath);

                    if (sr.ReadLine() is string s)
                    {
                        chooseFileDialog.InitialDirectory = s;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }
            if (this.chooseFileDialog.ShowDialog() == DialogResult.OK)
            {
                SetInitialFile(chooseFileDialog.FileName, true);
            }
        }

        private static string InitialFileSavePath => $"{Path.GetDirectoryName(Application.ExecutablePath)}\\initialFilePath";
        private static string ReplacementFileSavePath => $"{Path.GetDirectoryName(Application.ExecutablePath)}\\replacementFilePath";

        private bool SetInitialFile(string pathToFile, bool saveDir = false)
        {
            ResetFields();

            this.initialFilePath = pathToFile;

            FileInfo info;
            try
            {
                info = new(pathToFile);
                if (info.DirectoryName is null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                ResetFields();
                this.StatusText = "Error getting info for initial file";
                return false;
            }

            this.initialFileName = info.Name;
            this.directoryPath = info.DirectoryName;
            this.directoryFileNames = Directory.GetFiles(directoryPath, "*.uberstate")
                .Select(path => new FileInfo(path).Name).ToArray(); // <-- Map file paths to just file names

            if (saveDir)
            {
                if (File.Exists(InitialFileSavePath))
                    File.Delete(InitialFileSavePath);
                using FileStream fs = File.Create(InitialFileSavePath);
                byte[] dir = new UTF8Encoding(true).GetBytes(directoryPath);
                fs.Write(dir, 0, dir.Length);
            }

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

            if (this.initialFileIndex > 0)
            {
                this.downShiftButton.Enabled = true;
            }

            if (this.initialFileIndex < MaxIndex)
            {
                this.upShiftButton.Enabled = true;
            }

            this.deleteButton.Enabled = true;
            this.readOnlyInitialFileTextBox.Text = initialFileName;

            // TODO call ChooseReplacementFile if the readonlytextfield has text
            if (!string.IsNullOrEmpty(replacementFileFullPath))
            {
                SetReplacementFile(replacementFileFullPath);
            }

            return true;
        }

        private void DownShiftButton_Click(object sender, EventArgs e)
        {
            string oldName = this.initialFileName; // <-- Save this so SwapInitial doesn't mess up our StatusText output
            GetOldAndNewPrefixes(initialFileName, -1, out string _, out string newPrefix, out int _);
            SwapInitial(initialFileIndex - 1);
            this.StatusText = $"'{oldName.Replace(".uberstate", "")}' swapped with {newPrefix}";
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                File.Delete(initialFilePath);

                if (initialFileIndex < MaxIndex)
                {
                    Shift(initialFileIndex + 1, -1);
                }
                this.StatusText = $"'{this.initialFileName.Replace(".uberstate", "")}' deleted";
            }
            catch (Exception)
            {
                this.StatusText = "Error deleting file. App has been reset.";
            }
            finally
            {
                ResetFields();
            }
        }

        private void UpShiftButton_Click(object sender, EventArgs e)
        {
            string oldName = this.initialFileName; // <-- Save this so SwapInitial doesn't mess up our StatusText output
            GetOldAndNewPrefixes(initialFileName, 1, out string _, out string newPrefix, out int _);
            SwapInitial(initialFileIndex + 1);
            this.StatusText = $"'{oldName.Replace(".uberstate", "")}' swapped with {newPrefix}";
        }

        public void SwapInitial(int j)
        {
            int i = initialFileIndex;

            string firstFileName = directoryFileNames[i];
            string oldFirstFullPath = $"{directoryPath}\\{firstFileName}";
            string firstPrefix = rg.Match(firstFileName).Value;

            string secondFileName = directoryFileNames[j];
            string oldSecondFullPath = $"{directoryPath}\\{secondFileName}";
            string secondPrefix = rg.Match(secondFileName).Value;

            string newFirstFileName = firstFileName.Replace(firstPrefix, secondPrefix);
            string newFirstFullPath = $"{directoryPath}\\{newFirstFileName}";

            string newSecondFileName = secondFileName.Replace(secondPrefix, firstPrefix);
            string newSecondFullPath = $"{directoryPath}\\{newSecondFileName}";

            try
            {
                File.Move(oldFirstFullPath, newFirstFullPath);
                File.Move(oldSecondFullPath, newSecondFullPath);
            }
            catch (Exception)
            {
                ResetFields();
                this.StatusText = "Error moving files";
            }

            SetInitialFile(newFirstFullPath);
        }

        public void GetOldAndNewPrefixes(string fileName, int delta, out string oldPrefix, out string newPrefix, out int asInt)
        {
            oldPrefix = rg.Match(fileName).Value;
            asInt = int.Parse(oldPrefix);
            string format = new('0', oldPrefix.Length);
            newPrefix = (asInt + delta).ToString(format);
        }

        public void Shift(int index, int direction)
        {
            // We know matches for sure exist here
            // fileNames is sorted the same way windows explorer sorts by name
            for (int i = 0; i < directoryFileNames.Length; i++)
            {
                string currFileName = directoryFileNames[i];

                if (i >= index)
                {
                    GetOldAndNewPrefixes(currFileName, direction, out string oldPrefix, out string newPrefix, out int _);

                    // Replace the 
                    string newFileName = currFileName.Replace(oldPrefix, newPrefix);

                    string oldFullPath = $"{directoryPath}\\{currFileName}";
                    string newFullPath = $"{directoryPath}\\{newFileName}";

                    try
                    {
                        File.Move(oldFullPath, newFullPath);
                    }
                    catch (Exception)
                    {
                        ResetFields();
                        this.StatusText = "Error moving files. App reset";
                    }
                }
            }

            this.StatusText = $"Done! Another one?";
        }

        private void ChooseReplacementFileButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(ReplacementFileSavePath))
            {
                try
                {
                    using StreamReader sr = File.OpenText(ReplacementFileSavePath);

                    if (sr.ReadLine() is string s)
                    {
                        chooseFileDialog.InitialDirectory = s;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }

            }

            if (this.chooseFileDialog.ShowDialog() == DialogResult.OK)
            {
                SetReplacementFile(chooseFileDialog.FileName, true);
            }
        }

        private string replacementFileFullPath = "";
        private string replacementFileName = "";

        private void SetReplacementFile(string pathToFile, bool saveDir = false)
        {
            FileInfo info;
            try
            {
                info = new(pathToFile);
                if (info.DirectoryName is null)
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                ResetFields();
                this.StatusText = "Error getting info for replacement file";
                return;
            }

            this.replacementFileFullPath = pathToFile;
            this.replacementFileName = info.Name;
            this.readonlyReplacementFileTextBox.Text = info.Name;

            if (saveDir)
            {
                if (File.Exists(ReplacementFileSavePath))
                    File.Delete(ReplacementFileSavePath);
                using FileStream fs = File.Create(ReplacementFileSavePath);
                byte[] dir = new UTF8Encoding(true).GetBytes(info.DirectoryName);
                fs.Write(dir, 0, dir.Length);
            }

            // Stop here so we do not enable the buttons if there is no initial file. When they do set the initial file,
            // it will call this method at the end (if there is text in the replacementFileTextBox) to possibly
            // re-enable the buttons
            if (this.initialFileIndex == -1)
            {
                return;
            }

            this.replaceButton.Enabled = true;
            this.insertButton.Enabled = true;
            this.addButton.Enabled = true;
        }

        private static string RemoveSuffix(string s) => s.Replace(".uberstate", "");

        private void ReplaceButton_Click(object sender, EventArgs e)
        {
            try
            {
                File.Delete(this.initialFilePath);
                File.Copy(this.replacementFileFullPath, this.initialFilePath);
                this.StatusText = $"Replaced '{RemoveSuffix(initialFileName)}' with '{RemoveSuffix(replacementFileName)}'";
                ResetFields(); // I feel like Resetting here gives good aesthetic
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                ResetFields();
                this.StatusText = "Error replacing file";
                return;
            }
        }

        // This is used to find what they use to separate their numbers from their text
        // It simply looks for sequences of characters that are not letters or numbers
        // This is so when they add/insert we can match their pattern. If no pattern is found
        // It will simply add a space between :)
        // ChatGPT generated this for me because I am new to regex :)))
        private readonly Regex rg2 = new(@"[^a-zA-Z0-9]+");

        private void InsertButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.customNameTextBox.Text))
            {
                this.StatusText = "Please choose a name for the new save";
                return;
            }

            string name = this.customNameTextBox.Text;

            // Grab the current prefix for initialFileName to be the prefix for the replacement file (since we are inserting at its position)
            string prefix = rg.Match(initialFileName).Value;

            // use rg2 to copy their separater pattern
            Match match = rg2.Match(directoryFileNames[0]);
            string separater = match.Success ? match.Value : " ";

            try
            {
                File.Copy(replacementFileFullPath, $"{directoryPath}\\{prefix + separater + name}.uberstate");
            }
            catch (Exception)
            {
                ResetFields();
                this.StatusText = "Error inserting file";
                return;
            }

            Shift(this.initialFileIndex, 1);
            this.StatusText = $"Inserted {name} at position {prefix}";

            customNameTextBox.Text = null;
            ResetFields();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.customNameTextBox.Text))
            {
                this.StatusText = "Please choose a name for the new save";
                return;
            }

            string name = this.customNameTextBox.Text;

            // use rg2 to copy their separater pattern
            Match match = rg2.Match(directoryFileNames[0]);
            string separater = match.Success ? match.Value : " ";

            GetOldAndNewPrefixes(this.directoryFileNames[MaxIndex], 1, out string _, out string prefix, out int _);

            try
            {
                File.Copy(replacementFileFullPath, $"{directoryPath}\\{prefix + separater + name}.uberstate");
            }
            catch (Exception)
            {
                ResetFields();
                this.StatusText = "Error adding file";
                return;
            }

            this.StatusText = $"Added {name} at position {prefix}";

            customNameTextBox.Text = null;

            // We don't reset fields - instead, we call this function to refresh our array to the new file structure so they can repeat-add
            SetInitialFile(initialFilePath);
        }
    }
}