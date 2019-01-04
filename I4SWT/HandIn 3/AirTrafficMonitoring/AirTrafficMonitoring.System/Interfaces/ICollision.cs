using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.System.Interfaces
{
    public interface ICollision
    {
        string TagOne { get; set; }
        string TagTwo { get; set; }
        DateTime Timestamp { get; set; }
    }
}
