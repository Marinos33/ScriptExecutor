using CsvHelper; //third part library to use CSV
using CsvHelper.Configuration;
using GameSaveBackup.Interfaces;
using GameSaveBackup.Model;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

//the file to manage the csv with the games'list
namespace GameSaveBackup.Services
{
    public class CSVManager : ICSVManager
    {
        private const string CSV_PATH = "Data.csv"; //the path to the CSV which contains all the game (use to do the save system)
        internal List<Game> ListOfGame { get; set; } = new List<Game>();
        internal Game CurrentGame { get; set; } = new Game();

        public CSVManager()
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
            WriteCsv();
        }

        public void RemoveGame(int index)
        {
            ListOfGame.RemoveAt(index); //add the game to the list
            ListOfGame.Sort((x, y) => x.Name.CompareTo(y.Name)); //alphabetical sort
            WriteCsv();
        }

        public List<Game> GetListOfGame()
        {
            return ListOfGame;
        }

        public Game GetCurrentGame()
        {
            return CurrentGame;
        }

        public void SetCurrentGame(Game currentGame)
        {
            CurrentGame = currentGame;
        }

        //(re)write the entire csv
        public void WriteCsv()
        {
            var records = ListOfGame;
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };
            using (var writer = new StreamWriter(CSV_PATH))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(records);
            }
        }

        /*read the whole CSV*/
        private void ReadCsv()
        {
            if (File.Exists(CSV_PATH))
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                };

                using (var reader = new StreamReader(CSV_PATH))
                using (var csv = new CsvReader(reader, config))
                {
                    var records = csv.GetRecords<Game>(); // a record contains the content of the CSV
                    ListOfGame = records.ToList(); //pass all record to the list
                }
            }
        }
    }
}