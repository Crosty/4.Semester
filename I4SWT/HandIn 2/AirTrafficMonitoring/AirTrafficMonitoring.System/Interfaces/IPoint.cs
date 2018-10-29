using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.System.Interfaces
{
    public interface IPoint
    {
        int X { get; set; }
        int Y { get; set; }
        int Altitude { get; set; }
    }
}
