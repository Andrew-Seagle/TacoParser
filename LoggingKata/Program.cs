using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            logger.LogInfo("Log initialized");

            var lines = File.ReadAllLines(csvPath);

            if (lines.Length == 0)
                logger.LogError("Zero lines found in file");
            if (lines.Length == 1)
                logger.LogWarning("Only one like found in file");

            logger.LogInfo($"First line: {lines[0]}");

            var parser = new TacoParser();

            var locations = lines.Select(parser.Parse).ToArray();

            ITrackable locA = null;
            ITrackable locB = null;
            double distance = 0;
            var locNum = 1;

            foreach (ITrackable location in locations)
            {
                var corA = new GeoCoordinate(location.Location.Latitude, location.Location.Longitude);

                if (distance > 0)
                    logger.LogInfo($"Current longest distant: {distance}");
                if (locNum < locations.Length)
                    logger.LogInfo($"Finding remaining distances from {location.Name}");
                
                for (int i = locNum; i < locations.Length; i++)
                {
                    var corB = new GeoCoordinate(locations[i].Location.Latitude, locations[i].Location.Longitude);

                    logger.LogInfo($"    to {locations[i].Name, -30} : {corA.GetDistanceTo(corB)}");

                    if (corA.GetDistanceTo(corB) > distance)
                    {
                        distance = corA.GetDistanceTo(corB);
                        locA = location;
                        locB = locations[i];
                    }
                }

                locNum++;
            }

            logger.LogInfo($"**Furthest TacoBells: {locA.Name} and {locB.Name}**");
            logger.LogInfo($"**Distant: {distance}**");
        }
    }
}
