using AHIFventory.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHIFventory.ViewModel
{
    public class ProductViewModel
    {
        public ObservableCollection<Product> Products { get; set; }

        public ProductViewModel() { }

        //private void LoadProducts()
        //{
        //    using (var connection = new SQLiteConnection(_connectionString))
        //    {
        //        connection.Open();
        //        string query = "SELECT * FROM Fragen";

        //        using (var command = new SQLiteCommand(query, connection))
        //        using (var reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                var frage = new Frage
        //                {
        //                    ID = reader.GetInt32(reader.GetOrdinal("ID")),
        //                    FrageText = reader.GetString(reader.GetOrdinal("Frage")),
        //                    AntwortA = reader.GetString(reader.GetOrdinal("AntwortA")),
        //                    AntwortB = reader.GetString(reader.GetOrdinal("AntwortB")),
        //                    AntwortC = reader.GetString(reader.GetOrdinal("AntwortC")),
        //                    AntwortD = reader.GetString(reader.GetOrdinal("AntwortD")),
        //                    Richtige = reader.GetString(reader.GetOrdinal("Richtige")),
        //                    CountSolvedCorrect = reader.GetInt32(reader.GetOrdinal("countSolvedCorrect")),
        //                    CountSolvedIncorrect = reader.GetInt32(reader.GetOrdinal("countSolvedIncorrect"))
        //                };

        //                this.Add(frage);
        //            }
        //        }
        //    }
        //}
    }
}
