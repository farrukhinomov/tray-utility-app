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
            LoadFileContent();
            GetDatabaseInitiationStatusFromFile();
        }

        public override string Run()
        {
            ChangeStatus();
            return $"DemoData creation status changed to '{_demoDataCreationStatus}'";
        }
        public override string Help()
        {
            return $"DemoData creation status will be changed to '{ChangingStatus()}'";
        }

        private void ChangeStatus()
        {
            _fileContent = _fileContent.Replace($"<{_tagName}>{_demoDataCreationStatus}</{_tagName}>", $"<{_tagName}>{ChangingStatus()}</{_tagName}>");
            _demoDataCreationStatus = ChangingStatus();
            SaveFileContent();
        }

        private DatabaseInitiationStatus ChangingStatus()
        {
            return _demoDataCreationStatus == DatabaseInitiationStatus.Off ? DatabaseInitiationStatus.IfOutOfDate : DatabaseInitiationStatus.Off;
        }

        private void GetDatabaseInitiationStatusFromFile()
        {
            //string input = "<div>This is a test</div><div class=\"something\">This is ANOTHER test</div>";
            string pattern = "<" + _tagName + "(.*?)>(.*?)<\\/" + _tagName + ">";

            MatchCollection matches = Regex.Matches(_fileContent, pattern);
            var matchesCount = matches.Count;
            if (matchesCount >= 1)
            {
                if (matchesCount == 1)
                {
                    var status = matches[0].Groups[2].ToString();
                    Enum.TryParse(status, out _demoDataCreationStatus);
                }
                else
                    throw new Exception("It has 'CreateDatabaseInitiation' XML-tags, but the configfile structure is not correct");
            }
            else
                throw new Exception("Can find 'CreateDatabaseInitiation' XML-tag");
        }
        private void LoadFileContent()
        {
            try
            {
                _fileContent = File.ReadAllText(_filePath);
                if (string.IsNullOrEmpty(_fileContent))
                {
                    throw new Exception("The file is empty");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Can't read the file for some reason. ", ex);
            }

        }
        private void SaveFileContent()
        {
            try
            {
                File.WriteAllText(_filePath, _fileContent);
            }
            catch (Exception ex)
            {
                throw new Exception("Can't save the file for some reason. ", ex);
            }
        }
    }
}
