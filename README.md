# Emulation Station Game List Sorter

**Description**
Creates a custom collection cfg file from an emulation station game list XML file.

**Usage**

`EmulationStationGameListSorter [command] [options]`

**Options**

  --version       Show version information
  
  -?, -h, --help  Show help and usage information

**Commands**
 <p>genre      Filter by genre.
 <p>publisher  Filter by genre.
 <p>developer  Filter by publisher.
 <p>rating     Filter by rating range.
 <p>years      Filter by year range.
 
 **Example**
 
 Create a custom collection filtered by sports genre
 
` .\EmulationStationGameListSorter.exe genre --filter sport --gamelist mygamelist.xml --outputfile custom-sport.cfg --rompath ./ROMS/mame --xml true`

To filter by release date 1970 (inclusive) to 1980 (exclusive) and sports genre then reuse the xml file created from the previous command

` .\EmulationStationGameListSorter.exe years --low 1970 --high 1980 --gamelist custom-sport.xml --outputfile custom-sport-70s.cfg --rompath ./ROMS/mame --xml true`
 
