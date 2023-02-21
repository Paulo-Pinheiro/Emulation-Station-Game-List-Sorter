using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Globalization;

namespace EmulationStationGameListSorter
{
    public class GameList
    {
        [XmlElement("game")]
        public List<Game> Games { get; set; } = new List<Game>();

        public List<string> GetuniqueGenres()
        {
            return Games.Select(o => o.Genre).Distinct().ToList();
        }

        public List<Game> GetReleaseYearGames(int start, int end)
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
        public float Rating { get; set; }   

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


