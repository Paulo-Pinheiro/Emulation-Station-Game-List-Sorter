// Licensed under Apache Licence v3.0
// 2023 Paulo Pinheiro
using System.Xml.Serialization;
using System.Xml;
using System.Globalization;

namespace EmulationStationGameListSorter
{
    /// <summary>
    /// 
    /// </summary>
    public class GameList
    {
        [XmlElement("game")]
        public List<Game> Games { get; set; } = new List<Game>();
       
        public List<string> GetUniqueGenres()
        {
            return Games.Select(o => o.Genre).Distinct().ToList();
        }
       
        public List<string> GetUniquePublishers()
        {
            return Games.Select(o => o.Publisher).Distinct().ToList();
        }

        public List<string> GetUniqueDevelopers()
        {
            return Games.Select(o => o.Developer).Distinct().ToList();
        }

        public List<Game> GetGamesByGenre(string value)
        {
            // Look for a substring match only
            return Games.FindAll(o => o.Genre.Contains(value, StringComparison.CurrentCultureIgnoreCase));
        }

        public List<Game> GetGamesByPublisher(string value)
        {
            // Look for a substring match only
            return Games.FindAll(o => o.Publisher.Contains(value, StringComparison.CurrentCultureIgnoreCase));
        }

        public List<Game> GetGamesByDeveloper(string value)
        {
            // Look for a substring match only
            return Games.FindAll(o => o.Developer.Contains(value, StringComparison.CurrentCultureIgnoreCase));
        }

        public List<Game> GetGamesByRating(double low, double high)
        {
            return Games.FindAll(o => (o.Rating >= low && o.Rating <= high));
        }

        public List<Game> GetGamesByReleaseYear(int start, int end)
        {
            return Games.FindAll(o => (o.ReleaseYear >= start && o.ReleaseYear < end));
        }
    }

    public class Game
    {
        [XmlElement("path")]
        public string? Path { get; set; }
        
        [XmlElement("name")]
        public string Name { get; set; } = string.Empty;
        
        [XmlElement("desc")]
        public string Description { get; set; } = string.Empty;

        [XmlElement("image")]
        public string Image { get; set; } = string.Empty;

        [XmlElement("video")]
        public string Video { get; set; } = string.Empty;

        [XmlElement("rating")]
        public double Rating { get; set; }   

        [XmlElement("releasedate")]
        public string ReleaseDate { get; set; } = string.Empty;

        [XmlElement("developer")]
        public string Developer { get; set; } = string.Empty;

        [XmlElement("publisher")]
        public string Publisher { get; set; } = string.Empty;

        [XmlElement("genre")]
        public string Genre { get; set; } = string.Empty;

        [XmlElement("players")]
        public string Players { get; set; } = string.Empty;

        [XmlElement("playcount")]
        public int PlayCount { get; set; }

        [XmlElement("lastplayed")]
        public string LastPlayed { get; set; } = string.Empty;

        public int ReleaseYear
        { 
            get 
            {
                DateTime date;

                if (DateTime.TryParseExact(ReleaseDate, "yyyyMMddTHHmmss",
                                           CultureInfo.InvariantCulture,
                                           DateTimeStyles.None,
                                           out date))
                {
                    return date.Year;
                }
                else
                {
                    return 0;
                }
            }  
        }   
    }
}


