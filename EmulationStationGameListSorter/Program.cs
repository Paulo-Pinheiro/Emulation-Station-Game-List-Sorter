// See https://aka.ms/new-console-template for more information
using EmulationStationGameListSorter;
using System.Globalization;

Console.WriteLine("Emulation Station Geme List Sorter");

GameListSorter? MameGameList = GameListSorter.DeserializeXml<GameListSorter>(@"C:\SoftwareDev\EmulationStationGameListSorter\GameList.xml");

int? Games = 0;

Games = MameGameList?.SaveCollectionForYears("custom-70s.cfg", @"C:/ROMS/mame/", 1970, 1980);
Console.WriteLine($"Games written: {Games} ");

Games = MameGameList?.SaveCollectionForYears("custom-80s.cfg", @"C:/ROMS/mame/", 1980, 1990);
Console.WriteLine($"Games written: {Games} ");

Games = MameGameList?.SaveCollectionForYears("custom-90s.cfg", @"C:/ROMS/mame/", 1990, 2000);
Console.WriteLine($"Games written: {Games} ");

Games = MameGameList?.SaveCollectionForYears("custom-00s.cfg", @"C:/ROMS/mame/", 2000, 3000);
Console.WriteLine($"Games written: {Games} ");