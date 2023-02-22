// Licensed under Apache Licence v3.0
// 2023 Paulo Pinheiro
using EmulationStationGameListSorter;

Console.WriteLine("Emulation Station Geme List Sorter");

GameListSorter? MameGameList = GameListSorter.DeserializeXml<GameListSorter>(@"C:\SoftwareDev\EmulationStationGameListSorter\GameList.xml");

int? Games = 0;

List<Game>? list = MameGameList?.GameList.GetGamesByRating(0.9);

List<Game>? xlist = MameGameList?.GameList.GetGamesByGenre("fight");

Games = MameGameList?.SaveCollectionByReleaseYears("custom-70s.cfg", @"C:/ROMS/mame/", 1970, 1980);
Console.WriteLine($"Games written: {Games} ");

Games = MameGameList?.SaveCollectionByReleaseYears("custom-80s.cfg", @"C:/ROMS/mame/", 1980, 1990);
Console.WriteLine($"Games written: {Games} ");

Games = MameGameList?.SaveCollectionByReleaseYears("custom-90s.cfg", @"C:/ROMS/mame/", 1990, 2000);
Console.WriteLine($"Games written: {Games} ");

Games = MameGameList?.SaveCollectionByReleaseYears("custom-00s.cfg", @"C:/ROMS/mame/", 2000, 3000);
Console.WriteLine($"Games written: {Games} ");