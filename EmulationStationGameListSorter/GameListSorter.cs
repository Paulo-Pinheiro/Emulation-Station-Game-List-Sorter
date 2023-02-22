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
        public GameList GameList { get; set; } = new GameList();

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

        public int SaveCollectionByReleaseYears(string filename, string pathROMs, int start, int end)
        {
            List<Game> games = GameList.GetGamesByReleaseYear(start, end);

            return SaveCollection(games, filename, pathROMs);
        }

        public int SaveCollectionByGenre(string filename, string pathROMs, string genre)
        {
            List<Game> games = GameList.GetGamesByGenre(genre);

            return SaveCollection(games, filename, pathROMs);
        }

        public int SaveCollectionByDeveloper(string filename, string pathROMs, string developer)
        {
            List<Game> games = GameList.GetGamesByDeveloper(developer);

            return SaveCollection(games, filename, pathROMs);
        }

        public int SaveCollectionByPublisher(string filename, string pathROMs, string publisher)
        {
            List<Game> games = GameList.GetGamesByPublisher(publisher);

            return SaveCollection(games, filename, pathROMs);
        }

        static public GameListSorter? DeserializeXml<T>(string gameListFilename)
        {
            // Read the file as one long string.
            string gameListXml = System.IO.File.ReadAllText(gameListFilename);
            
            // Remove <? xml version = "1.0" ?> if present on the xml. Otherwsie, invalid XML is created.
            gameListXml = Regex.Replace(gameListXml, @"<\?xml[^;]*\?>", string.Empty, RegexOptions.IgnoreCase);
            
            // Create dummy root element in case it is an XML fragment adn therefore no root element is present. XML must have a root element.
            gameListXml = "<root>" + gameListXml + "</root>";
            
            
            // Create the GameList
            using (XmlReader reader = XmlReader.Create(new StringReader(gameListXml)))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                GameListSorter? result = xmlSerializer.Deserialize(reader) as GameListSorter;
                return result;
            }
        }
    }
}
