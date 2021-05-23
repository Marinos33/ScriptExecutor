using CsvHelper; //third part library to use CSV
using CsvHelper.Configuration;
using GameSaveBackup.Interfaces;
using GameSaveBackup.Model;
using ScriptExecutor.Interfaces;
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

        private readonly IData _data;

        public CSVManager(IData data)
        {
            _data = data;
        }

        //(re)write the entire csv
        public void WriteCsv()
        {
            var records = _data.ListOfGame;
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };
            using var writer = new StreamWriter(CSV_PATH);
            using var csv = new CsvWriter(writer, config);
            csv.WriteRecords(records);
        }

        /*read the whole CSV*/

        public IEnumerable<Game> ReadCsv()
        {
            if (File.Exists(CSV_PATH))
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = false,
                };

                using var reader = new StreamReader(CSV_PATH);
                using var csv = new CsvReader(reader, config);
                var records = csv.GetRecords<Game>(); // a record contains the content of the CSV
                return records.ToList(); //pass all record to the list
            }
            return Enumerable.Empty<Game>();
        }
    }
}