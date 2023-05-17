using System.Text.Json;
using System.Text.RegularExpressions;

namespace oriwotw_save_tool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            PersistantAppData = new();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ResetInitialFields();
            ResetReplacementFileFields();

            // If there is an error reading then PersistantAppData will remain unchanged. Once WritePersistantData() is called this
            // should be fixed (unless there are other underlying issues such as no local folder or file perms)
            ReadPersistantData();

            if (!string.IsNullOrEmpty(PersistantAppData.LastReplacementFileFullPath))
            {
                SetReplacementFile(PersistantAppData.LastReplacementFileFullPath);
            }

            chooseFileDialog.Filter = "Ori Save Files (*.uberstate)|*.uberstate";
        }

        private readonly OpenFileDialog chooseFileDialog = new();

        /// <summary>
        /// This regex finds the prefix pattern of the save file names
        /// It works by looking for a continuous sequence of numbers (i.e: [0-9]+) at the beginning (i.e: /A) of the string
        /// We match this pattern on the file names of the uberstate files so that we can find out their index (prefix #) and also do
        /// math on it (i.e increment/decrement the prefix). Using a pattern like this is nice because it gives the end-user
        /// many degrees of freedom on how they set up their file names (as long as there are numbers at the beginning we can parse it).
        /// We will not only be able to parse out their file names to operate on the prefix but we can also use the information from the
        /// match in order to replicate their format for when we add new save files to their directory.
        /// </summary>
        private readonly Regex prefixPattern = new(@"\A[0-9]+");


        /// <summary>
        /// This regex finds the separator pattern of the save file names.
        /// It works by looking for a continuos sequence of non-letter && non-number characters. We use this to find out how the user
        /// separates their prefix from the acutal file name. In my example I use " - " as my separator (e.g, my file name looks like
        /// "000 - Game Start". This allows us to replicate not only their prefix pattern but also their separator pattern. In the end
        /// it allows for the user to not have to be super specific with their file names in the tool (i.e, they don't have to add the
        /// separator themselves).
        /// </summary>
        private readonly Regex separatorPattern = new(@"[^a-zA-Z0-9]+");

        private string StatusText
        {
            set => this.statusTextLabel.Text = value;
        }

        private static readonly string appFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ORIWOTW Save Tool");
        private static readonly string appSaveFilePath = Path.Combine(appFolderPath, "PersistantData.json");

        /// <summary>Makes sure that our application save folder and save file exist and if they dont it will create them</summary>
        private bool SaveFileCheck()
        {
            if (!Directory.Exists(appFolderPath))
            {
                try
                {
                    Directory.CreateDirectory(appFolderPath);
                }
                catch (Exception)
                {
                    StatusText = "Error creating directory for persistant data (run as admin)";
                    return false;
                }
            }

            if (!File.Exists(appSaveFilePath))
            {
                try
                {
                    using FileStream fs = File.Create(appSaveFilePath);
                }
                catch (Exception)
                {
                    StatusText = "Error creating file for persistant data (run as admin)";
                    return false;
                }
            }

            return true;
        }

        public class PersistantData
        {
            public string LastInitialFileDir { get; set; }
            public string LastReplacementFileDir { get; set; }
            public string LastReplacementFileFullPath { get; set; }

            public PersistantData()
            {
                LastInitialFileDir = "";
                LastReplacementFileDir = "";
                LastReplacementFileFullPath = "";
            }
        }

        private PersistantData PersistantAppData;

        private bool ReadPersistantData()
        {
            if (!SaveFileCheck())
            {
                return false;
            }

            using StreamReader r = new(appSaveFilePath);
            string json = r.ReadToEnd();

            try
            {
                PersistantData? tryAppData = JsonSerializer.Deserialize<PersistantData>(json);

                // If this statement is false the ERR handling code will run
                if (tryAppData is PersistantData appData)
                {
                    PersistantAppData = appData;
                    return true;
                }
            }
            catch (Exception) { /* If we run into a parsing exception, the ERR handling code will run */ }

            // If we run into error reading PersistantData and replacing the contents of PersistantAppData field, we won't update the field.
            // Instead we will continue to use the one in memory so it will at least be persistant until they close the app
            return false;
        }

        private bool WritePersistantData()
        {
            if (!SaveFileCheck())
            {
                return false;
            }

            try
            {
                File.Delete(appSaveFilePath);
                using FileStream fs = File.Create(appSaveFilePath);
            }
            catch (Exception)
            {
                StatusText = "Error clearing persistant data save file";
                return false;
            }

            try
            {
                string jsonString = JsonSerializer.Serialize(PersistantAppData, new JsonSerializerOptions() { WriteIndented = true });
                using StreamWriter outputFile = new(appSaveFilePath);
                outputFile.WriteLine(jsonString);
                return true;
            }
            catch (Exception)
            {
                StatusText = "Error writing persistant data save file";
                return false;
            }
        }

        private string initialFilePath = "";
        private string initialFileName = "";
        private string directoryPath = "";
        private int initialFileIndex = -1;

        private int MaxIndex => directoryFileNames.Length - 1;
        private string[] directoryFileNames = Array.Empty<string>();

        /// <summary>Also resets the button fields for replace/insert/add since they rely on initial being set</summary>
        private void ResetInitialFields()
        {
            // Everything below here is set in SetInitialFile
            this.initialFilePath = "";
            this.initialFileName = "";
            this.directoryPath = "";
            this.initialFileIndex = -1;
            this.directoryFileNames = Array.Empty<string>();
            this.readOnlyInitialFileTextBox.Text = "";
            this.downShiftButton.Enabled = false;
            this.deleteButton.Enabled = false;
            this.upShiftButton.Enabled = false;

            // Everything below here is set in SetReplacementFile
            this.replaceButton.Enabled = false;
            this.insertButton.Enabled = false;
            this.addButton.Enabled = false;

            // Excluded things below
            // TODO instead of resetting these we just check validity (i.e make sure file still exists)
            // this.replacementFileFullPath (not changed)
            // this.replacementFileName (not changed)
            // this.replacementFileDir (not changed)
            // this.readonlyReplacementFileTextBox.Text (not changed)
        }

        private void ChooseInitialFileButton_Click(object sender, EventArgs e)
        {
            ReadPersistantData();

            if (!string.IsNullOrEmpty(PersistantAppData.LastInitialFileDir))
            {
                chooseFileDialog.InitialDirectory = PersistantAppData.LastInitialFileDir;
            }

            if (this.chooseFileDialog.ShowDialog() == DialogResult.OK)
            {
                // If we get no errors from setting the initial file to this location...
                if (SetInitialFile(chooseFileDialog.FileName))
                {
                    // We know directoryPath was just updated and also not null if SetInitialFile returned true
                    PersistantAppData.LastInitialFileDir = directoryPath;
                    WritePersistantData();
                }
            }
        }

        private bool SetInitialFile(string pathToFile)
        {
            ResetInitialFields();

            this.initialFilePath = pathToFile;

            FileInfo info;
            try
            {
                info = new(pathToFile);
                if (info.DirectoryName is null)
                    throw new Exception();
            }
            catch (Exception)
            {
                ResetInitialFields();
                this.StatusText = "Error getting info for initial file";
                return false;
            }

            this.initialFileName = info.Name;
            this.directoryPath = info.DirectoryName;
            this.directoryFileNames = Directory.GetFiles(directoryPath, "*.uberstate")
                .Select(path => new FileInfo(path).Name).ToArray(); // <-- Map file paths to just file names

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
                ResetInitialFields();
                this.StatusText = "Could not find any uberstate files here";
                return false;
            }

            // Scan directory and make sure ALL files match the pattern properly
            foreach (string fileName in directoryFileNames)
            {
                Match match = prefixPattern.Match(fileName);
                if (!match.Success)
                {
                    ResetInitialFields();
                    this.StatusText = "ALL files in this directory must have a proper naming pattern";
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
                ResetInitialFields();
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

            if (!string.IsNullOrEmpty(replacementFileFullPath))
            {
                SetReplacementFile(replacementFileFullPath);
            }

            return true;
        }

        private void DownShiftButton_Click(object sender, EventArgs e)
        {
            PerformShift(-1);
        }

        private void UpShiftButton_Click(object sender, EventArgs e)
        {
            PerformShift(1);
        }

        private void PerformShift(int direction)
        {
            string oldName = this.initialFileName; // <-- Save this so SwapInitial doesn't mess up our StatusText output
            GetOldAndNewPrefixes(initialFileName, direction, out string _, out string newPrefix, out int _);
            SwapInitial(initialFileIndex + direction);
            this.StatusText = $"'{oldName.Replace(".uberstate", "")}' swapped with {newPrefix}";
        }

        public void SwapInitial(int j)
        {
            int i = initialFileIndex;

            string firstFileName = directoryFileNames[i];
            string oldFirstFullPath = $"{directoryPath}\\{firstFileName}";
            string firstPrefix = prefixPattern.Match(firstFileName).Value;

            string secondFileName = directoryFileNames[j];
            string oldSecondFullPath = $"{directoryPath}\\{secondFileName}";
            string secondPrefix = prefixPattern.Match(secondFileName).Value;

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
                ResetInitialFields();
                this.StatusText = "Error moving files";
            }

            SetInitialFile(newFirstFullPath);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                File.Delete(initialFilePath);

                // If shift runs into error it returns false, resets fields, and changes StatusText
                if (initialFileIndex < MaxIndex && !Shift(initialFileIndex + 1, -1))
                {
                    return; // We return if shift ran into an error so that we don't change the StatusText below
                }
                this.StatusText = $"'{this.initialFileName.Replace(".uberstate", "")}' deleted";
            }
            catch (Exception)
            {
                this.StatusText = "Error deleting file. App has been reset.";
            }
            finally
            {
                ResetInitialFields();
            }
        }

        public void GetOldAndNewPrefixes(string fileName, int delta, out string oldPrefix, out string newPrefix, out int asInt)
        {
            oldPrefix = prefixPattern.Match(fileName).Value;
            asInt = int.Parse(oldPrefix);
            string format = new('0', oldPrefix.Length);
            newPrefix = (asInt + delta).ToString(format);
        }

        private void ChooseReplacementFileButton_Click(object sender, EventArgs e)
        {
            ReadPersistantData();

            if (!string.IsNullOrEmpty(PersistantAppData.LastReplacementFileDir))
            {
                chooseFileDialog.InitialDirectory = PersistantAppData.LastReplacementFileDir;
            }

            if (this.chooseFileDialog.ShowDialog() == DialogResult.OK)
            {
                // If we get no errors from setting the initial file to this location...
                if (SetReplacementFile(chooseFileDialog.FileName))
                {
                    // We know replacementFileDir was JUST updated and also not null if SetReplacementFile returned true
                    PersistantAppData.LastReplacementFileDir = this.replacementFileDir;
                    PersistantAppData.LastReplacementFileFullPath = this.replacementFileFullPath;
                    WritePersistantData();
                }
            }
        }

        private string replacementFileFullPath = "";
        private string replacementFileName = "";
        private string replacementFileDir = "";

        private void ResetReplacementFileFields()
        {
            this.replaceButton.Enabled = false;
            this.insertButton.Enabled = false;
            this.addButton.Enabled = false;

            this.replacementFileDir = "";
            this.replacementFileFullPath = "";
            this.replacementFileName = "";
            this.readonlyReplacementFileTextBox.Text = "";
        }

        private bool SetReplacementFile(string pathToFile)
        {
            FileInfo info;
            try
            {
                info = new(pathToFile);
                if (info.DirectoryName is null)
                    throw new Exception();
            }
            catch (Exception)
            {
                ResetInitialFields();
                ResetReplacementFileFields();
                this.StatusText = "Error getting info for replacement file";
                return false;
            }

            this.replacementFileFullPath = pathToFile;
            this.replacementFileName = info.Name;
            this.readonlyReplacementFileTextBox.Text = info.Name;
            this.replacementFileDir = info.DirectoryName;

            // Stop here so we do not enable the buttons if there is no initial file. When they do set the initial file,
            // it will call this method at the end (if there is text in the replacementFileTextBox) to possibly
            // re-enable the buttons
            if (this.initialFileIndex == -1)
            {
                return true;
            }

            this.replaceButton.Enabled = true;
            this.insertButton.Enabled = true;
            this.addButton.Enabled = true;

            return true;
        }

        private static string RemoveExt(string s) => s.Replace(".uberstate", "");

        private void ReplaceButton_Click(object sender, EventArgs e)
        {
            try
            {
                File.Delete(this.initialFilePath);
                File.Copy(this.replacementFileFullPath, this.initialFilePath);
                this.StatusText = $"Replaced '{RemoveExt(initialFileName)}' with '{RemoveExt(replacementFileName)}'";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                this.StatusText = "Error replacing file";
            }
            finally
            {
                ResetInitialFields();
            }
        }

        private void InsertButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.customNameTextBox.Text))
            {
                this.StatusText = "Please choose a name for the new save";
                return;
            }

            string name = this.customNameTextBox.Text;

            // Grab the current prefix for initialFileName to be the prefix for the replacement file (since we are inserting at its position)
            string prefix = prefixPattern.Match(initialFileName).Value;

            // use rg2 to copy their separater pattern
            Match match = separatorPattern.Match(directoryFileNames[0]);
            string separater = match.Success ? match.Value : " ";

            try
            {
                File.Copy(replacementFileFullPath, $"{directoryPath}\\{prefix + separater + name}.uberstate");
            }
            catch (Exception)
            {
                ResetInitialFields();
                this.StatusText = "Error inserting file";
                return;
            }

            // If shift runs into error it returns false, resets fields, and changes StatusText
            if (Shift(this.initialFileIndex, 1))
            {
                this.StatusText = $"Inserted {name} at position {prefix}";

                customNameTextBox.Text = null;
                ResetInitialFields();
            }
        }

        public bool Shift(int index, int direction)
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
                        ResetInitialFields();
                        this.StatusText = "Error shifting files. App reset";
                        return false;
                    }
                }
            }

            return true;
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
            Match match = separatorPattern.Match(directoryFileNames[0]);
            string separater = match.Success ? match.Value : " ";

            GetOldAndNewPrefixes(this.directoryFileNames[MaxIndex], 1, out string _, out string prefix, out int _);

            try
            {
                File.Copy(replacementFileFullPath, $"{directoryPath}\\{prefix + separater + name}.uberstate");
            }
            catch (Exception)
            {
                ResetInitialFields();
                this.StatusText = "Error adding file";
                return;
            }

            customNameTextBox.Text = null;

            // We don't reset fields - instead, we call this function to refresh our array to the new file structure so
            // they can repeat-add. SetInitialFile will call ResetFields() anyways
            if (SetInitialFile(initialFilePath))
            {
                // Only update status if our call to SetInitialFile didn't cause an error (we don't want to override error status)
                this.StatusText = $"Added {name} at position {prefix}";
            }

        }
    }
}