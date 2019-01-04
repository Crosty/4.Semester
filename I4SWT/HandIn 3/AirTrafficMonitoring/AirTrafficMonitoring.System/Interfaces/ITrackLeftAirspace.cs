using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficMonitoring.System.Interfaces
{
    public interface ITrackLeftAirspace
    {
        event EventHandler<EventTracks> TracksleftAirspace;

    }
}
