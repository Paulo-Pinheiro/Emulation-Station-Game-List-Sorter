using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmulationStationGameListSorter;

namespace EmulationStationGameListSorter.Tests
{
    [TestClass()]
    public class GameListSorterTests
    {
        readonly private string gameList = @"./../../../TestFiles/Gamelist.xml";
        readonly private string gameListclean = @"./../../../TestFiles/Gamelist-clean.xml";
        readonly private string outputFile = @"./../../../TestFiles/Filtered.cfg";
        readonly private string romPath = "@C:/mame/";
        readonly private string romPathFiles = @"./../../../TestFiles";


        [TestMethod()]
        public void SortByGenreTest()
        {
            // Filter by genre "sports"
            int? gamesFiltered = GameListSorter.SortByGenre(gameList, "sports", outputFile, romPath, true);
            // Load new filtered game list
            int? gamesLoaded = GameListSorter.SortByGenre(Path.ChangeExtension(outputFile, ".xml"), "sports", outputFile, romPath, true);
            // Number of filtered games must be the same
            Assert.AreEqual(gamesFiltered, gamesLoaded);
            // Number of games written must be the same i.e. one game one line in file
            int lineCount = File.ReadLines(outputFile).Count();
            Assert.AreEqual(gamesFiltered, lineCount);
        }

        [TestMethod()]
        public void SortByPublisherTest()
        {
            // Filter by Publisher "CAP"
            int? gamesFiltered = GameListSorter.SortByPublisher(gameList, "CAP", outputFile, romPath, true);
            // Load new filtered game list
            int? gamesLoaded = GameListSorter.SortByPublisher(Path.ChangeExtension(outputFile, ".xml"), "CAP", outputFile, romPath, true);
            // Number of filtered games must be the same
            Assert.AreEqual(gamesFiltered, gamesLoaded);
            // Number of games written must be the same i.e. one game one line in file
            int lineCount = File.ReadLines(outputFile).Count();
            Assert.AreEqual(gamesFiltered, lineCount);
        }

        [TestMethod()]
        public void SortByDeveloperTest()
        {
            // Filter by Developer "CAP"
            int? gamesFiltered = GameListSorter.SortByDeveloper(gameList, "CAP", outputFile, romPath, true);
            // Load new filtered game list
            int? gamesLoaded = GameListSorter.SortByDeveloper(Path.ChangeExtension(outputFile, ".xml"), "CAP", outputFile, romPath, true);
            // Number of filtered games must be the same
            Assert.AreEqual(gamesFiltered, gamesLoaded);
            // Number of games written must be the same i.e. one game one line in file
            int lineCount = File.ReadLines(outputFile).Count();
            Assert.AreEqual(gamesFiltered, lineCount);
        }

        [TestMethod()]
        public void SortByRatingTest()
        {
            // Filter by Rating
            int? gamesFiltered = GameListSorter.SortByRating(gameList, 0.5, 0.8, outputFile, romPath, true);
            // Load new filtered game list
            int? gamesLoaded = GameListSorter.SortByRating(Path.ChangeExtension(outputFile, ".xml"), 0.5, 0.8, outputFile, romPath, true);
            // Load new filtered game list, outputFile, romPath, true);
            // Number of filtered games must be the same
            Assert.AreEqual(gamesFiltered, gamesLoaded);
            // Number of games written must be the same i.e. one game one line in file
            int lineCount = File.ReadLines(outputFile).Count();
            Assert.AreEqual(gamesFiltered, lineCount);
        }

        [TestMethod()]
        public void SortByReleaseYearsTest()
        {
            // Filter by Developer "CAP"
            int? gamesFiltered = GameListSorter.SortByReleaseYears(gameList, 1970, 1980, outputFile, romPath, true);
            // Load new filtered game list
            int? gamesLoaded = GameListSorter.SortByReleaseYears(Path.ChangeExtension(outputFile, ".xml"), 1970, 1980, outputFile, romPath, true);
            // Number of filtered games must be the same
            Assert.AreEqual(gamesFiltered, gamesLoaded);
            // Number of games written must be the same i.e. one game one line in file
            int lineCount = File.ReadLines(outputFile).Count();
            Assert.AreEqual(gamesFiltered, lineCount);
        }

        [TestMethod()]
        public void SortByRomsTest()
        {
            // Filter by exisitng Roms
            int? gamesFiltered = GameListSorter.SortByRoms(gameList, romPathFiles);
            // Load new filtered game list
            int? gamesLoaded = GameListSorter.SortByRoms(gameListclean, romPathFiles);

            Assert.AreEqual(gamesFiltered, 2);
        }
    }
}