using CsvHelper; //third part library to use CSV
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

//the model of the pattern MVC
namespace GameSaveBackup
{
    public class Model
    {
        private const string CSV_PATH = "Data.csv"; //the path to the CSV which contains all the game (use to do the save system)
        private const string LOGFILENAME = "log GameSave_Backup.txt"; //the path to the log file

        internal List<Game> ListOfGame { get; set; } = new List<Game>();
        internal Game CurrentGame { get; set; } = new Game();

        public Model()
        {
            ReadCsv();
        }

        /*used to say to the program : there are no program from the list who is running*/

        public void ResetCurrentGame()
        {
            CurrentGame.Name = null;
            CurrentGame.ScriptPath = null;
            CurrentGame.ExecutablePath = null;
            CurrentGame.Update();
        }

        public void AddGame(Game game)
        {
            ListOfGame.Add(game); //add the game to the list
            ListOfGame.Sort((x, y) => x.Name.CompareTo(y.Name)); //alphabetical sort
        }

        /*read the whole CSV*/

        private void ReadCsv()
        {
            if (File.Exists(CSV_PATH))
            {
                using (var reader = new StreamReader(CSV_PATH))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Configuration.HasHeaderRecord = false; //the header is the frist line which contains the name of each property name of the object
                    var records = csv.GetRecords<Game>(); // a record contains the content of the CSV
                    ListOfGame = records.ToList(); //pass all record to the list
                }
            }
        }

        //(re)write the entire csv
        public void WriteCsv()
        {
            var records = ListOfGame;

            using (var writer = new StreamWriter(CSV_PATH))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = false; //the header is the frist line which contains the name of each property name of the object
                csv.WriteRecords(records);
            }
        }

        //read the whole log file
        public string ReadLog()
        {
            if (File.Exists(LOGFILENAME))
            {
                string textFile = File.ReadAllText(LOGFILENAME);
                return textFile;
            }
            return null;
        }

        //(re)write the entire log file
        public void WriteLog(string text)
        {
            string previousText = ReadLog();
            using (StreamWriter sw = File.CreateText(LOGFILENAME))
            {
                sw.WriteLine(text);
                sw.WriteLine(previousText);
            }
        }
    }
}