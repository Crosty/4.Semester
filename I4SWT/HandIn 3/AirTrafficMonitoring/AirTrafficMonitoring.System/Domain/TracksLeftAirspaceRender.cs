using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using AirTrafficMonitoring.System.Interfaces;

namespace AirTrafficMonitoring.System.Domain
{
   public class TracksLeftAirspaceRender : ITracksLeftAirspaceRender
    {
        private readonly IDisplay _display;
        
        public TracksLeftAirspaceRender(ITrackLeftAirspace tracksleftAirspace, IDisplay display)
        {
            _display = display;
            tracksleftAirspace.TracksleftAirspace += RenderLeftTracks;

        }

        private void RenderLeftTracks(object sender, EventTracks e)
        {
            _display.Write("*TRACKS LEFT AIRSPACE*\n");
            foreach (var track in e.Data)
            {
                var timer = new Timer();
                var str = "Tag: " + track.Tag + ", Time: " + track.Timestamp.ToString("G");

                timer.Enabled = true;
                timer.Interval = 5000;
                _display.Write(str);
            }
        }

    }
}
