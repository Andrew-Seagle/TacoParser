using System;

namespace LoggingKata
{
    class TacoBell : ITrackable
    {
        public string Name { get; set; }
        public Point Location { get; set; }

        public TacoBell(double lat, double lon, string name)
        {
            this.Name = name;
            this.Location = new Point() { Latitude = lat, Longitude = lon };
        }
    }
}
