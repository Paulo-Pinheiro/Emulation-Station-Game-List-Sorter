using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Text.RegularExpressions;

namespace EmulationStationGameListSorter
{
    [XmlRoot("root")]
    public class GameListSorter
    {
        [XmlElement("gameList")]
        public GameList GameList { get; set; } = new GameList();

        public int SaveCollectionForYears(string filename, string path, int start, int end)
        {
            List<Game> games = GameList.GetReleaseYearGames(start, end);

            using (StreamWriter file = new(filename, append: false))
            {
                foreach (Game game in games)
                {
                    file.WriteLine(path + Path.GetFileName(game.Path));
                }
            }
         
            return games.Count;
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
