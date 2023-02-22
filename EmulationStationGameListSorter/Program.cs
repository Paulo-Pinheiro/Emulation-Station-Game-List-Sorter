// Licensed under Apache Licence v3.0
// 2023 Paulo Pinheiro
using System.CommandLine;
using System.CommandLine.Invocation;
using EmulationStationGameListSorter;

internal class Program
{
    private static int Main(string[] args)
    {
        //-------------------------------------------------------------------------------------------------------------------//
        // Options
        //-------------------------------------------------------------------------------------------------------------------//
        var gameListOption = new Option<string>(name: "--gamelist",
            description: "The game list XML file to read from.",
            parseArgument: result =>
            {
                string filePath = result.Tokens.Single().Value;
                if (!File.Exists(filePath))
                {
                    result.ErrorMessage = "File does not exist.";
                    return string.Empty;
                }
                else
                {
                    return filePath;
                }
            }) { IsRequired = true };

        var outputFileOption = new Option<string>(name: "--outputfile",
            description: "The name of the custom collection file to create.",
            parseArgument: result =>
            {
                string filePath = result.Tokens.Single().Value;
                if (Path.GetExtension(filePath) == string.Empty)
                {
                    result.ErrorMessage = "A file must be specified.";
                    return string.Empty;
                }
                else
                {
                    return filePath;
                }
            }) { IsRequired = true };

        var romPathOption = new Option<string>(name: "--rompath", description: "The path to the ROMs to be captured in the output file.") { IsRequired = true };
        var filterOption = new Option<string>(name: "--filter", description: "The substring to match according to the command.") { IsRequired = true };
        var lowOption = new Option<double>(name: "--low", description: "The lower year limit (greater or equal).") { IsRequired = true };
        var highOption = new Option<double>(name: "--high", description: "The high year limit (less than).") { IsRequired = true };
        var xmlOption = new Option<bool>(name: "--xml", description: "Create xml file. Use it as the game list xml file if you would like to filter the already filtered collections.", getDefaultValue: () => false) { IsRequired = false };


        //-------------------------------------------------------------------------------------------------------------------//
        // Commands
        //-------------------------------------------------------------------------------------------------------------------//
        Command genreCommand = new Command("genre", "Filter by genre.")
        {
            filterOption,
            gameListOption,
            outputFileOption,
            romPathOption,
            xmlOption
        };

        genreCommand.SetHandler((filter, gamelist, outputfile, rompath, xmlOption) =>
            {
                ESGameListSorterByGenre(filter, gamelist, outputfile, rompath, xmlOption);
            },
           filterOption,
           gameListOption,
           outputFileOption,
           romPathOption,
           xmlOption);

        //-------------------------------------------------------------------------------------------------------------------//
        Command publisherCommand = new Command("publisher", "Filter by genre.")
        {
            filterOption,
            gameListOption,
            outputFileOption,
            romPathOption, 
            xmlOption
        };

        publisherCommand.SetHandler((filter, gamelist, outputfile, rompath, xmlOption) =>
            {
                ESGameListSorterByPublisher(filter, gamelist, outputfile, rompath, xmlOption);
            },
           filterOption,
           gameListOption,
           outputFileOption,
           romPathOption,
           xmlOption);

        //-------------------------------------------------------------------------------------------------------------------//
        Command developerCommand = new Command("developer", "Filter by publisher.")
        {
            filterOption,
            gameListOption,
            outputFileOption,
            romPathOption, 
            xmlOption
        };

        developerCommand.SetHandler((filter, gamelist, outputfile, rompath, xmlOption) =>
            {
                ESGameListSorterByDeveloper(filter, gamelist, outputfile, rompath, xmlOption);
            },
            filterOption,
            gameListOption,
            outputFileOption,
            romPathOption,
            xmlOption);

        //-------------------------------------------------------------------------------------------------------------------//
        Command ratingCommand = new Command("rating", "Filter by rating range.")
        {
            lowOption,
            highOption,
            gameListOption,
            outputFileOption,
            romPathOption, 
            xmlOption
        };

        ratingCommand.SetHandler((low, high, gamelist, outputfile, rompath, xml) =>
            {
                ESGameListSorterByRating(low, high, gamelist, outputfile, rompath, xml);
            },
            lowOption,
            highOption,
            gameListOption,
            outputFileOption,
            romPathOption,
            xmlOption);

        //-------------------------------------------------------------------------------------------------------------------//
        Command yearsCommand = new Command("years", "Filter by year range.")
        {
            lowOption,
            highOption,
            gameListOption,
            outputFileOption,
            romPathOption, 
            xmlOption
        };

        yearsCommand.SetHandler((low, high, gamelist, outputfile, rompath, xml) =>
            {
                ESGameListSorterByYears((int)low, (int)high, gamelist, outputfile, rompath, xml);
            },
            lowOption,
            highOption,
            gameListOption,
            outputFileOption,
            romPathOption,
            xmlOption);

        //-------------------------------------------------------------------------------------------------------------------//
        // Set Commands
        //-------------------------------------------------------------------------------------------------------------------//
        RootCommand rootCommand = new RootCommand(description: "Creates a custom collection cfg file from an emulation station game list XML file.");

        rootCommand.AddCommand(genreCommand);
        rootCommand.AddCommand(publisherCommand);
        rootCommand.AddCommand(developerCommand);
        rootCommand.AddCommand(ratingCommand);
        rootCommand.AddCommand(yearsCommand);

        return rootCommand.Invoke(args);
    }

    static void ESGameListSorterByGenre(string genre, string gameList, string outputFile, string romPath, bool xml)
    {
        GameListSorter? MameGameList = GameListSorter.DeserializeXml(gameList);

        int? gamesSaved = MameGameList?.SaveCollectionByGenre(outputFile, romPath, genre, xml);

        Console.WriteLine($"Games written: {gamesSaved}");
    }

    static void ESGameListSorterByPublisher(string publisher, string gameList, string outputFile, string romPath, bool xml)
    {
        GameListSorter? MameGameList = GameListSorter.DeserializeXml(gameList);

        int? gamesSaved = MameGameList?.SaveCollectionByPublisher(outputFile, romPath, publisher, xml);

        Console.WriteLine($"Games written: {gamesSaved}");
    }

    static void ESGameListSorterByDeveloper(string developer, string gameList, string outputFile, string romPath, bool xml)
    {
        GameListSorter? MameGameList = GameListSorter.DeserializeXml(gameList);

        int? gamesSaved = MameGameList?.SaveCollectionByDeveloper(outputFile, romPath, developer, xml);

        Console.WriteLine($"Games written: {gamesSaved}");
    }

    static void ESGameListSorterByRating(double low, double high, string gameList, string outputFile, string romPath, bool xml)
    {
        GameListSorter? MameGameList = GameListSorter.DeserializeXml(gameList);

        int? gamesSaved = MameGameList?.SaveCollectionByRating(outputFile, romPath, low, high, xml);

        Console.WriteLine($"Games written: {gamesSaved}");
    }

    static void ESGameListSorterByYears(int low, int high, string gameList, string outputFile, string romPath, bool xml)
    {
        GameListSorter? MameGameList = GameListSorter.DeserializeXml(gameList);

        int? gamesSaved = MameGameList?.SaveCollectionByReleaseYears(outputFile, romPath, low, high, xml);

        Console.WriteLine($"Games written: {gamesSaved}");
    }
}