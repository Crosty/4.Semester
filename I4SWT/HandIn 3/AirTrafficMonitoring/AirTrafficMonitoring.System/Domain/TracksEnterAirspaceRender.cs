using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using AirTrafficMonitoring.System.Interfaces;

namespace AirTrafficMonitoring.System.Domain
{
    public class TracksEnterAirspaceRender : ITracksEnterAirspaceRender
    {
        private readonly IDisplay _display;

        public TracksEnterAirspaceRender(ITracksEnterAirspace tracksEnterAirspace, IDisplay display)
        {
            _display = display;
            tracksEnterAirspace.TracksEnteredAirspace += RenderEnteredTracks;
        }

        private void RenderEnteredTracks(object sender, EventTracks e)
        {
            _display.Write("*TRACKS ENTERED AIRSPACE*\n");
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
