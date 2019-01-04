using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.System.Domain;

namespace AirTrafficMonitoring.System.Interfaces
{
    public interface ITrack
    {
        string Tag { get; set; }
        Point Position { get; set; }
        DateTime Timestamp { get; set; }
        double Velocity { get; set; }
        double Course { get; set; }
    }
}
