using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.System.Interfaces;

namespace AirTrafficMonitoring.System.Domain
{
    public class Airspace : IAirspace
    {
        public Point SouthWestCorner { get; set; }
        public Point NorthEastCorner { get; set; }

        public Airspace()
        {
            //Sets minimum coordination - limits
            SouthWestCorner = new Point(10000, 10000, 500);

            //Sets maximum coordination - limits
            NorthEastCorner = new Point(90000, 90000, 20000);
        }

        //Checks if the track is within the airspace
        public bool CheckIfWithinAirspace(Point point)
        {
            if (point.X < SouthWestCorner.X || point.Y < SouthWestCorner.Y)
                return false;
            if (point.X > NorthEastCorner.X || point.Y > NorthEastCorner.Y)
                return false;
            if (point.Altitude < SouthWestCorner.Altitude || point.Altitude > NorthEastCorner.Altitude)
                return false;

            return true;
        }
    }
}
