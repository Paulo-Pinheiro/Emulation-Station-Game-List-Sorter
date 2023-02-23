
// Licensed under Apache Licence v3.0
// 2023 Paulo Pinheiro
using System.Globalization;
using System.Xml.Serialization;

namespace EmulationStationGameListSorter
{
    [XmlRoot("game")]
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
