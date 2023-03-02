// Licensed under Apache Licence v3.0
// 2023 Paulo Pinheiro
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace EmulationStationGameListSorter
{
    [XmlRoot("gameList")]
    public class GameList
    {
        public enum Selection
        {
            Genre, Publisher, Developer, Rating, ReleaseYear
        }

        [XmlElement("game")]
        public List<Game> Games { get; set; } = new List<Game>();

        public List<string> GetUniqueBy(Selection sel)
        {
            switch (sel)
            {
                case Selection.Genre:
                    return Games.Select(o => o.Genre).Distinct().ToList();

                case Selection.Publisher:
                    return Games.Select(o => o.Publisher).Distinct().ToList();

                case Selection.Developer:
                    return Games.Select(o => o.Developer).Distinct().ToList();

                default:
                    return new List<string>();
            }
        }

        public GameList GetGamesBy(Selection sel, string value)
        {
            GameList newGameList = new GameList();

            switch(sel)
            {
                case Selection.Genre:
                    newGameList.Games = Games.FindAll(o => o.Genre.Contains(value, StringComparison.CurrentCultureIgnoreCase));
                    break;

                case Selection.Publisher:
                    newGameList.Games = Games.FindAll(o => o.Publisher.Contains(value, StringComparison.CurrentCultureIgnoreCase));
                    break;

                case Selection.Developer:
                    newGameList.Games = Games.FindAll(o => o.Developer.Contains(value, StringComparison.CurrentCultureIgnoreCase));
                    break;

                default:
                    newGameList.Games = new List<Game>();
                    break;
            }

            return newGameList;
        }

        public GameList GetGamesByRange(Selection sel, double low, double high)
        {
            GameList newGameList = new GameList();

            switch (sel)
            {
                case Selection.Rating:
                    newGameList.Games = Games.FindAll(o => (o.Rating >= low && o.Rating <= high));
                    break;

                case Selection.ReleaseYear:
                    newGameList.Games = Games.FindAll(o => (o.ReleaseYear >= (int)low && o.ReleaseYear < (int)high));
                    break;

                default:
                    newGameList.Games = new List<Game>();
                    break;
            }

            return newGameList;
        }

        public GameList GetGamesByRom(string rompath)
        {
            GameList newGameList = new GameList();

            foreach(Game game in Games) 
            {
                string? gamePath = Path.GetFileName(game.Path);

                if (gamePath != null)
                {
                    string path = Path.Combine(rompath, gamePath);
                    if (File.Exists(path))
                    {
                        newGameList.Games.Add(game);
                    }
                }
            }

            return newGameList;
        }

        public void SaveToXml(string xmlFilePath)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());

            using (StreamWriter writer = new StreamWriter(xmlFilePath))
            {
                xmlSerializer.Serialize(writer, this);
            }
        }

        public int SaveToCollection(string filename, string pathROMs)
        {
            int result = 0;

            using (StreamWriter file = new StreamWriter(filename, append: false))
            {
                foreach (Game game in Games)
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

            return result;
        }
    }
}


