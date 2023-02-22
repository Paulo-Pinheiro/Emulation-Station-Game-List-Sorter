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

        static protected int SaveCollection(List<Game> games, string filename, string pathROMs)
        {
            int result = 0;
           
            // check validity of input
            if (Path.GetFileName(filename) != null)
            {
                using (StreamWriter file = new StreamWriter(filename, append: false))
                {
                    foreach (Game game in games)
                    {
                        string? romFilename = Path.GetFileName(game.Path);

                        if (romFilename is not null)
                        {
                            string romLocation = Path.Combine(pathROMs, romFilename);
                            file.WriteLine(romLocation);
                            result++;
                        }
                    }
                }
            }    

            return result;
        }

        public int SaveCollectionByGenre(string filename, string pathROMs, string genre, bool xml)
        {
            List<Game> games = Games.GetGamesByGenre(genre);

            if (xml) SaveCollectionToXml(filename, games);

            return SaveCollection(games, filename, pathROMs);
        }

        public int SaveCollectionByDeveloper(string filename, string pathROMs, string developer, bool xml)
        {
            List<Game> games = Games.GetGamesByDeveloper(developer);

            if(xml) SaveCollectionToXml(filename, games);

            return SaveCollection(games, filename, pathROMs);
        }

      
        public int SaveCollectionByPublisher(string filename, string pathROMs, string publisher, bool xml)
        {
            List<Game> games = Games.GetGamesByPublisher(publisher);

            if (xml) SaveCollectionToXml(filename, games);

            return SaveCollection(games, filename, pathROMs);
        }

        public int SaveCollectionByRating(string filename, string pathROMs, double start, double end, bool xml)
        {
            List<Game> games = Games.GetGamesByRating(start, end);

            if (xml) SaveCollectionToXml(filename, games);

            return SaveCollection(games, filename, pathROMs);
        }

        public int SaveCollectionByReleaseYears(string filename, string pathROMs, int start, int end, bool xml)
        {
            List<Game> games = Games.GetGamesByReleaseYear(start, end);

            if (xml) SaveCollectionToXml(filename, games);

            return SaveCollection(games, filename, pathROMs);
        }

        static public GameListSorter? DeserializeXml(string gameListFilename)
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

        private static void SaveCollectionToXml(string filename, List<Game> games)
        {
            GameListSorter sorted = new GameListSorter();
            GameList gameList = new GameList();
            gameList.Games = games;
            sorted.Games = gameList;
            SerializeToXml(sorted, Path.ChangeExtension(filename, ".xml"));
        }

        private static void SerializeToXml(GameListSorter anyobject, string xmlFilePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(anyobject.GetType());

            using (StreamWriter writer = new StreamWriter(xmlFilePath))
            {
                xmlSerializer.Serialize(writer, anyobject);
            }
        }
    }
}
