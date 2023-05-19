using System.Text.Json;
using System.Text.RegularExpressions;

namespace oriwotw_save_tool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            PersistentAppData = new();
            InitializeComponent();
        }

        /// <summary> Removes the .uberstate from a string</summary>
        private static string RemoveExt(string s) => s.Replace(".uberstate", "");

        /// <summary>
        /// Upon loading the form, the application will reset all the initial/replacement fields to their default
        /// state and also read the persistent data. If the persistent data exists it will select their
        /// last-chosen replacement file (usually saveFile[0-9].uberstate) and fill the replacementFile field with this
        /// information.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            ResetInitialFields();
            ResetReplacementFileFields();

            // If there is an error reading then PersistentAppData will remain unchanged. Once WritePersistentData() is called this
            // should be fixed (unless there are other underlying issues such as no local folder or file perms)
            ReadPersistentData();

            if (!string.IsNullOrEmpty(PersistentAppData.LastReplacementFileFullPath))
            {
                SetReplacementFile(PersistentAppData.LastReplacementFileFullPath);
            }

            chooseFileDialog.Filter = "Ori Save Files (*.uberstate)|*.uberstate";
        }

        /// <summary>This is used for choosing both the initial file and the replacement file. The directory that it will
        /// open up to depends on whether the initial or replacement file is being chosen. The appliation will remember the
        /// users' directory preference for each file type (e.g usually Local/Ori for replacement file and usually 
        /// StreamingAssets/Saves for initial file) and store this preference inside PersistentData.json to be used.
        /// </summary>
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

        /// <summary>%Appdata%/Local/ORIWOTW Save Tool</summary>
        private static readonly string appFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ORIWOTW Save Tool");

        /// <summary>ORIWOTW Save Tool/PersistentData.json </summary>
        private static readonly string appSaveFilePath = Path.Combine(appFolderPath, "PersistentData.json");

        /// <summary>
        /// This method makes sure that there exists a JSON file: %AppData%/Local/ORIWOTW Save Tool/PersistentData.json
        /// If either the folder or file do not exist it will attempt to create the folder/file. If this cannot be done,
        /// whether it be from a security exception or if the %AppData%/Local folder cannot be found, then it will 
        /// change the StatusText to an error message and return false.
        /// </summary>
        /// <returns></returns>
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
                    StatusText = "Error creating directory for persistent data (run as admin)";
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
                    StatusText = "Error creating file for persistent data (run as admin)";
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// This is an object in memory that is a representation of the JSON file stored at
        /// "../AppData/Local/ORIWOTW Save Tool". This JSON file stores information about where
        /// the user normally selects their saves from for the initial/replacement files for the FileChoosers.
        /// </summary>
        public class PersistentData
        {
            public string LastInitialFileDir { get; set; }
            public string LastReplacementFileDir { get; set; }
            public string LastReplacementFileFullPath { get; set; }

            public PersistentData()
            {
                LastInitialFileDir = "";
                LastReplacementFileDir = "";
                LastReplacementFileFullPath = "";
            }
        }

        /// At the start of the app and during other times this field will get refreshed with the 
        /// contents of the JSON file. If for some reason the JSON file cannot be accessed or loaded 
        /// properly, this field won't get refreshed. This is so that even if the persistent file system 
        /// stuff doesn't work, the app will still be able to store information (within memory) about what folders the 
        /// FileChoosers should open up to (at least until it closes)
        private PersistentData PersistentAppData;

        /// <summary>
        /// Attempts to read the PersistentData.json file stored at appSaveFilePath. After reading it, the PersistentAppData field will be refreshed.
        /// If the SaveFileCheck fails or if the JSON file cannot be read/parsed, the PersistentAppData field 
        /// simply will not be updated and the method will return false.
        /// </summary>
        private bool ReadPersistentData()
        {
            if (!SaveFileCheck())
            {
                return false;
            }

            using StreamReader r = new(appSaveFilePath);
            string json = r.ReadToEnd();

            try
            {
                PersistentData? tryAppData = JsonSerializer.Deserialize<PersistentData>(json);

                // If this statement is false the ERR handling code will run
                if (tryAppData is PersistentData appData)
                {
                    PersistentAppData = appData;
                    return true;
                }
            }
            catch (Exception) { /* If we run into a parsing exception, the ERR handling code will run */ }

            // If we run into error reading PersistentData and replacing the contents of PersistentAppData field, we won't update the field.
            // Instead we will continue to use the one in memory so it will at least be persistent until they close the app
            return false;
        }

        /// <summary>
        /// Attempts to write the PersistentAppData field to PersistentData.json file. If the SaveFileCheck
        /// fails or if the JSON file cannot be read/parsed it will return false
        /// </summary>
        private bool WritePersistentData()
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
                StatusText = "Error clearing persistent data save file";
                return false;
            }

            try
            {
                string jsonString = JsonSerializer.Serialize(PersistentAppData, new JsonSerializerOptions() { WriteIndented = true });
                using StreamWriter outputFile = new(appSaveFilePath);
                outputFile.WriteLine(jsonString);
                return true;
            }
            catch (Exception)
            {
                StatusText = "Error writing persistent data save file";
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

            // These 3 buttons require both replacement and initial file to be set
            // so we reset them here when the initial fields reset (even though 
            // resetting the initial fields has no change on the replacement fields)
            this.replaceButton.Enabled = false;
            this.insertButton.Enabled = false;
            this.addButton.Enabled = false;
            // ^ at the end of SetInitialFile, SetReplacementFile() will be called if there
            // is text in its cooresponding field, so these buttons will be reactivated

            // Excluded things below
            // TODO instead of resetting these we just check validity (i.e make sure file still exists)
            // this.replacementFileFullPath (not changed)
            // this.replacementFileName (not changed)
            // this.replacementFileDir (not changed)
            // this.readonlyReplacementFileTextBox.Text (not changed)
        }

        private void ChooseInitialFileButton_Click(object sender, EventArgs e)
        {
            ReadPersistentData();

            if (!string.IsNullOrEmpty(PersistentAppData.LastInitialFileDir))
            {
                chooseFileDialog.InitialDirectory = PersistentAppData.LastInitialFileDir;
            }

            if (this.chooseFileDialog.ShowDialog() == DialogResult.OK)
            {
                // If we get no errors from setting the initial file to this location...
                if (SetInitialFile(chooseFileDialog.FileName))
                {
                    // We know directoryPath was just updated and also not null if SetInitialFile returned true
                    PersistentAppData.LastInitialFileDir = directoryPath;
                    WritePersistentData();
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

            this.StatusText = "Choose an action or select replacement for more actions";

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
            ReadPersistentData();

            if (!string.IsNullOrEmpty(PersistentAppData.LastReplacementFileDir))
            {
                chooseFileDialog.InitialDirectory = PersistentAppData.LastReplacementFileDir;
            }

            if (this.chooseFileDialog.ShowDialog() == DialogResult.OK)
            {
                // If we get no errors from setting the initial file to this location...
                if (SetReplacementFile(chooseFileDialog.FileName))
                {
                    // We know replacementFileDir was JUST updated and also not null if SetReplacementFile returned true
                    PersistentAppData.LastReplacementFileDir = this.replacementFileDir;
                    PersistentAppData.LastReplacementFileFullPath = this.replacementFileFullPath;
                    WritePersistentData();
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
                this.StatusText = "Choose initial file";
                return true;
            }

            this.replaceButton.Enabled = true;
            this.insertButton.Enabled = true;
            this.addButton.Enabled = true;

            this.StatusText = "Choose an action";
            return true;
        }


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