// Licensed under Apache Licence v3.0
// 2023 Paulo Pinheiro
using System.CommandLine;
using System.CommandLine.Invocation;
using EmulationStationGameListSorter;

var gameListOption = new Option<FileInfo>(
            name: "--gamelist",
            description: "The game list XML to read from.");
gameListOption.IsRequired = true;


var outputFileOption = new Option<FileInfo>(
           name: "--outputfile",
           description: "The name of the custom collection file to create.");
outputFileOption.IsRequired = true;


var romPathOption = new Option<string>(
           name: "--rompath",
           description: "The path to the ROMs to be captured in the output file.");
romPathOption.IsRequired = true;

RootCommand rootCommand = new RootCommand(description: "Creates a custom collection cfg file from an emulation station game list XML file.");


//===============================================

var genreOption = new Option<string>(
           name: "--genre",
           description: "The genre name.");
genreOption.IsRequired = true;

Command genreCommand = new Command("genre", "Filter by genre.")
        {
            genreOption,
            gameListOption,
            outputFileOption,
            romPathOption
        };

rootCommand.AddCommand(genreCommand);

genreCommand.SetHandler((genre, gamelist, outputfile, rompath) =>
    {
        ESGameListSorterByGenre(genre, gamelist, outputfile, rompath);
    },
    genreOption,
    gameListOption,
    outputFileOption,
    romPathOption);


//===========================================

var yearLowOption = new Option<int>(
           name: "--low",
           description: "The lower year limit (greater or equal).");
yearLowOption.IsRequired = true;

var yearHighOption = new Option<int>(
           name: "--high",
           description: "The high year limit (less than).");
yearHighOption.IsRequired = true;

Command yearsCommand = new Command("years", "Filter by year range.")
        {
            yearLowOption,
            yearHighOption,
            gameListOption,
            outputFileOption,
            romPathOption
        };

rootCommand.AddCommand(yearsCommand);

yearsCommand.SetHandler((yearsLow, yearsHigh, gamelist, outputfile, rompath) =>
    {
        ESGameListSorterByYears(yearsLow, yearsHigh, gamelist, outputfile, rompath);
    },
    yearLowOption,
    yearHighOption,
    gameListOption,
    outputFileOption,
    romPathOption);

return rootCommand.Invoke(args);


static void ESGameListSorterByYears(int yearsLow, int yearsHigh, FileInfo gameList, FileInfo outputFile, string romPath)
{
    GameListSorter? MameGameList = GameListSorter.DeserializeXml<GameListSorter>(gameList.FullName);

    int? gamesSaved = MameGameList?.SaveCollectionByReleaseYears(outputFile.FullName, romPath, yearsLow, yearsHigh);

    Console.WriteLine($"Games written: {gamesSaved}");
}

static void ESGameListSorterByGenre(string genre, FileInfo gameList, FileInfo outputFile, string romPath)
{
    GameListSorter? MameGameList = GameListSorter.DeserializeXml<GameListSorter>(gameList.FullName);

    int? gamesSaved = MameGameList?.SaveCollectionByGenre(outputFile.FullName, romPath, genre);

    Console.WriteLine($"Games written: {gamesSaved} ");
}