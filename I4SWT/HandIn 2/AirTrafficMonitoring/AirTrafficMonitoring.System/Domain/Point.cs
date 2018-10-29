using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.System.Interfaces;

namespace AirTrafficMonitoring.System.Domain
{
    public class Point : IPoint
    {
        //General creation of coordination and altitude
        public int X { get; set; }
        public int Y { get; set; }
        public int Altitude { get; set; }

        public Point(int x = 0, int y = 0, int altitude = 0)
        {
            X = x;
            Y = y;
            Altitude = altitude;
        }
    }
}
