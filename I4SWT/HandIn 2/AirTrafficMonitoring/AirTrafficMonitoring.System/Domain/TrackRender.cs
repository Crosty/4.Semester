using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.System.Interfaces;

namespace AirTrafficMonitoring.System.Domain
{
    public class TrackRender : ITrackRender
    {
        private readonly IDisplay _display;
        //private ILog _log;

        public TrackRender(IUpdateModule updateModule, IDisplay display)
        {
            //_log = log;
            _display = display;
            updateModule.TracksUpdated += RenderTracks;
        }

        //Renders the tracks
        private void RenderTracks(object sender, EventTracks e)
        {
            //Clears the console from history of events.
            _display.Clear();
            _display.Write("*TRACKS*");
            foreach (var track in e.Data)
            {
                var str = "Tag: " + track.Tag + " Current position: " + track.Position.X +
                    " east, " + track.Position.Y + " north, altitude: " + track.Position.Altitude +
                    "m, Velocity: " + track.Velocity + "m/s, Course: " + track.Course + " degree";
                _display.Write(str);
            }
        }
    }
}
