using Common;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace EU.CreateDemoDataStatus
{
    enum DatabaseInitiationStatus
    {
        IfOutOfDate,
        OnStartup,
        NeverRecreate,
        Off
    }

    [Utility(UtilityName)]
    public class CreateDemoDataStatus : UtilityBase
    {
        const string UtilityName = "Demo data creation status";
        DatabaseInitiationStatus _demoDataCreationStatus;
        string _fileContent;
        string _filePath;
        string _tagName;

        public CreateDemoDataStatus()
        {
            _filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Eagle", "config.json");
            _fileContent = string.Empty;
            _tagName = "CreateDatabaseInitiation";
        }

        public override string Run()
        {
            try
            {
                LoadFileContent();
            }
            catch (FileNotFoundException fnfex)
            {
                return $"Can't read the file for some reason. {fnfex.Message}";
            }
            catch (Exception ex)
            {
                return $"Can't read the file for some reason.\n{ex.Message}";
            }

            try
            {
                GetDatabaseInitiationStatusFromFile();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            ChangeStatus();
            try
            {
                SaveFileContent();
            }
            catch (Exception ex)
            {
                return $"Can't save the file for some reason.\n{ex.Message}";
            }

            return $"DemoData creation status changed to '{_demoDataCreationStatus}'";
        }
        public override string Help()
        {
            return $"DemoData creation status will be changed";
        }

        private void ChangeStatus()
        {
            _fileContent = _fileContent.Replace($"<{_tagName}>{_demoDataCreationStatus}</{_tagName}>", $"<{_tagName}>{ChangingStatus()}</{_tagName}>");
            _demoDataCreationStatus = ChangingStatus();
        }

        private DatabaseInitiationStatus ChangingStatus()
        {
            return _demoDataCreationStatus == DatabaseInitiationStatus.Off ? DatabaseInitiationStatus.IfOutOfDate : DatabaseInitiationStatus.Off;
        }

        private void GetDatabaseInitiationStatusFromFile()
        {
            string pattern = "<" + _tagName + "(.*?)>(.*?)<\\/" + _tagName + ">";

            MatchCollection matches = Regex.Matches(_fileContent, pattern);
            var matchesCount = matches.Count;
            if (matchesCount >= 1)
            {
                if (matchesCount == 1)
                {
                    var status = matches[0].Groups[2].ToString();
                    if (!Enum.TryParse(status, out _demoDataCreationStatus)) throw new Exception ($"Wrong option inside '{_tagName}' XML-tag");
                }
                else
                    throw new Exception($"The file structure is not correct, but it has '{_tagName}' XML-tags");
            }
            else
                throw new Exception($"Can't find '{_tagName}' XML-tag");
        }
        private void LoadFileContent()
        {

            _fileContent = File.ReadAllText(_filePath);
            if (string.IsNullOrEmpty(_fileContent))
            {
                throw new Exception("The file is empty");
            }
        }
        private void SaveFileContent()
        {
            File.WriteAllText(_filePath, _fileContent);
        }
    }
}
