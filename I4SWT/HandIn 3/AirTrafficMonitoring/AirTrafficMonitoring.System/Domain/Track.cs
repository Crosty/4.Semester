using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.System.Interfaces;

namespace AirTrafficMonitoring.System.Domain
{
    public class Track : ITrack
    {
        //General creation of tracks
        public string Tag { get; set; }
        public Point Position { get; set; }
        public DateTime Timestamp { get; set; }
        public double Velocity { get; set; }
        public double Course { get; set; }

        public Track(string tag, Point position, DateTime timestamp)
        {
            Tag = tag;
            Position = position;
            Timestamp = timestamp;
        }
    }
}
