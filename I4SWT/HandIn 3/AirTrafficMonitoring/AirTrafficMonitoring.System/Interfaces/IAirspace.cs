using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.System.Domain;

namespace AirTrafficMonitoring.System.Interfaces
{
    public interface IAirspace
    {
        bool CheckIfWithinAirspace (Point point);
    }
}
