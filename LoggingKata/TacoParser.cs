namespace LoggingKata
{
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            logger.LogInfo("Begin parsing");

            var cells = line.Split(',');

            if (cells.Length < 3)
            {
                logger.LogError("Array length too short");

                return null; 
            }

            var lat = double.Parse(cells[0]);
            var lon = double.Parse(cells[1]);
            var name = cells[2];

            var tacoBell = new TacoBell(lat, lon, name);

            return tacoBell;
        }
    }
}