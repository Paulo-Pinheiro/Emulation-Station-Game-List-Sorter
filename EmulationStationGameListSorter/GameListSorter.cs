// Licensed under Apache Licence v3.0
// 2023 Paulo Pinheiro

using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace EmulationStationGameListSorter
{
    [XmlRoot("root")]
    public class GameListSorter
    {
        [XmlElement("gameList")]
        public GameList Games { get; set; } = new GameList();

        static public int? SortByGenre(string gameList, string genre, string outputFile, string romPath, bool xml)
        {
            GameListSorter? MameGameList = GameListSorter.LoadGameListXml(gameList);

            GameList? filteredGames = MameGameList?.Games.GetGamesBy(GameList.Selection.Genre, genre);
            int? gamesSaved         = filteredGames?.SaveToCollection(outputFile, romPath);
            
            if (xml) filteredGames?.SaveToXml(Path.ChangeExtension(outputFile, ".xml"));  

            Console.WriteLine($"File written {outputFile} Games written: {gamesSaved}");
            return gamesSaved;
        }

        static public int? SortByPublisher(string gameList, string publisher, string outputFile, string romPath, bool xml)
        {
            GameListSorter? MameGameList = GameListSorter.LoadGameListXml(gameList);

            GameList? filteredGames = MameGameList?.Games.GetGamesBy(GameList.Selection.Publisher, publisher);
            int? gamesSaved         = filteredGames?.SaveToCollection(outputFile, romPath);

            if (xml) filteredGames?.SaveToXml(Path.ChangeExtension(outputFile, ".xml"));

            Console.WriteLine($"File written {outputFile} Games written: {gamesSaved}");
            return gamesSaved;
        }

        static public int?  SortByDeveloper(string gameList, string developer, string outputFile, string romPath, bool xml)
        {
            GameListSorter? MameGameList = GameListSorter.LoadGameListXml(gameList);

            GameList? filteredGames = MameGameList?.Games.GetGamesBy(GameList.Selection.Developer, developer);
            int? gamesSaved = filteredGames?.SaveToCollection(outputFile, romPath);

            if (xml) filteredGames?.SaveToXml(Path.ChangeExtension(outputFile, ".xml"));

            Console.WriteLine($"File written {outputFile} Games written: {gamesSaved}");
            return gamesSaved;
        }

        static public int? SortByRating(string gameList, double low, double high, string outputFile, string romPath, bool xml)
        {
            GameListSorter? MameGameList = GameListSorter.LoadGameListXml(gameList);

            GameList? filteredGames = MameGameList?.Games.GetGamesByRange(GameList.Selection.Rating, low, high);
            int? gamesSaved = filteredGames?.SaveToCollection(outputFile, romPath);

            if (xml) filteredGames?.SaveToXml(Path.ChangeExtension(outputFile, ".xml"));

            Console.WriteLine($"File written {outputFile} Games written: {gamesSaved}");
            return gamesSaved;
        }

        static public int? SortByReleaseYears(string gameList, double low, double high, string outputFile, string romPath, bool xml)
        {
            GameListSorter? MameGameList = GameListSorter.LoadGameListXml(gameList);

            GameList? filteredGames = MameGameList?.Games.GetGamesByRange(GameList.Selection.ReleaseYear, low, high);
            int? gamesSaved = filteredGames?.SaveToCollection(outputFile, romPath);

            if (xml) filteredGames?.SaveToXml(Path.ChangeExtension(outputFile, ".xml"));

            Console.WriteLine($"File written {outputFile} Games written: {gamesSaved}");
            return gamesSaved;
        }

        static public int? SortByRoms(string gameList, string rompath)
        {
            string fDir  = Path.GetDirectoryName(gameList);
            string fName = Path.GetFileNameWithoutExtension(gameList);
            string fExt  = Path.GetExtension(gameList);
            string outputFile =  Path.Combine(fDir, String.Concat(fName, "-clean", fExt));

            GameListSorter? MameGameList = GameListSorter.LoadGameListXml(gameList);

            GameList? filteredGames = MameGameList?.Games.GetGamesByRom(rompath);

            filteredGames?.SaveToXml(Path.ChangeExtension(outputFile, ".xml"));

            int? gamesSaved = filteredGames?.Games.Count;

            Console.WriteLine($"File written {outputFile} Games written: {gamesSaved}");
            return gamesSaved;
        }

        static public GameListSorter? LoadGameListXml(string gameListFilename)
        {
            // Read the file as one long string.
            string gameListXml = System.IO.File.ReadAllText(gameListFilename);
            
            // Remove <? xml version = "1.0" ?> if present on the xml. Otherwsie, invalid XML is created.
            gameListXml = Regex.Replace(gameListXml, @"<\?xml[^;]*\?>", string.Empty, RegexOptions.IgnoreCase);
            
            // Check if root element exist and add one if not
            // Create dummy root element in case it is an XML fragment and therefore no root element is present. XML must have a root element.
            if (gameListXml.Contains("<root") is false) gameListXml = "<root>" + gameListXml + "</root>";
              
            // Create the GameList
            using (XmlReader reader = XmlReader.Create(new StringReader(gameListXml)))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(GameListSorter));
                GameListSorter? result = xmlSerializer.Deserialize(reader) as GameListSorter;
                return result;
            }
        }
    }
}
