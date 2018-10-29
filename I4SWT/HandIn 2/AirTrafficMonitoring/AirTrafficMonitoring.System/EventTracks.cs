using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.System.Interfaces;

namespace AirTrafficMonitoring.System
{
    public class EventTracks : EventArgs
    {
        public List<ITrack> Data;

        public EventTracks(List<ITrack> data)
        {
            Data = data;
        }
    }
}
