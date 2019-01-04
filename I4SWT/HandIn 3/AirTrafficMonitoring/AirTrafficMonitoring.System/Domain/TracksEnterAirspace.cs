using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using AirTrafficMonitoring.System.Interfaces;

namespace AirTrafficMonitoring.System.Domain
{
    public class TracksEnterAirspace : ITracksEnterAirspace
    {
        private IAirspace _airspace;
        private List<ITrack> _currentTracks;
        public event EventHandler<EventTracks> TracksEnteredAirspace;

        public TracksEnterAirspace(IAirspace airspace, IUpdateModule updateModule)
        {
            _airspace = airspace;
            _currentTracks = new List<ITrack>();
            updateModule.TracksUpdated += TrackEnterAirspace;
        }

        private void TrackEnterAirspace(object sender, EventTracks e)
        {
            var enteredTracks = new List<ITrack>();
            foreach (var track in e.Data)
            {
                //Check if within airspace, if within airspace add to list and printout on render
                //Else nothing.

                if (CheckIfTrackEntersAirspace(track.Timestamp, track.Position))
                {
                    enteredTracks.Add(track);
                }   
            }
            // Update the list.
            _currentTracks = enteredTracks;
            TracksEnteredAirspaceEvent(new EventTracks(enteredTracks));
        }

        protected virtual void TracksEnteredAirspaceEvent(EventTracks e)
        {
            TracksEnteredAirspace?.Invoke(this, e);
        }

        //Checks if a track enters airspace
        private bool CheckIfTrackEntersAirspace(DateTime timestamp, Point point)
        {
            var southWestCorner = new Point(10000, 10000, 500);
            var northEastCorner = new Point(90000, 90000, 20000);
            var timer = new Timer();

            if (point.X > southWestCorner.X && point.Y > southWestCorner.Y && point.X < northEastCorner.X &&
                point.Y < northEastCorner.Y)
            {
                //Console.WriteLine("*Track Entered Airspace*" + " - Tag: " + _currentTracks.FirstOrDefault()?.Tag + ", Time: " + timestamp);
                //timer.Enabled = true;
                //timer.Interval = 5000;
                return true;
            }
            return false;
        }
    }
}
