using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using AirTrafficMonitoring.System.Interfaces;


namespace AirTrafficMonitoring.System.Domain
{
    public class TrackLeftAirspace: ITrackLeftAirspace
    {
        private IAirspace _airspace;
        private List<ITrack> _currentTracks;
        public event EventHandler<EventTracks> TracksleftAirspace;

        public TrackLeftAirspace(IAirspace airspace, IUpdateModule updateModule)
        {
            _airspace = airspace;
            _currentTracks = new List<ITrack>();
            updateModule.TracksUpdated += TrackLeftedAirspace;
        }

        private void TrackLeftedAirspace(object sender, EventTracks e)
        {
            var leftTracks = new List<ITrack>();
            foreach (var track in e.Data)
            {
                //Check if within airspace, if within airspace add to list and printout on render
                //Else nothing.

                if (CheckIfTrackleftAirspace(track.Timestamp, track.Position))
                {
                    leftTracks.Add(track);
                }
            }
            // Update the list.
            _currentTracks = leftTracks;
            TracksleftAirspaceEvent(new EventTracks(leftTracks));
        }

        protected virtual void TracksleftAirspaceEvent(EventTracks e)
        {
            TracksleftAirspace?.Invoke(this, e);
        }

        //Checks if a track left airspace
        private bool CheckIfTrackleftAirspace(DateTime timestamp, Point point)
        {
            var southWestCorner = new Point(10000, 10000, 500);
            var northEastCorner = new Point(90000, 90000, 20000);
            var timer = new Timer();

            if (point.X < southWestCorner.X && point.Y < southWestCorner.Y && point.X > northEastCorner.X &&
                point.Y > northEastCorner.Y)
            {
                //Console.WriteLine("*Track Left Airspace*" + " - Tag: " + _currentTracks.FirstOrDefault()?.Tag + ", Time: " + timestamp);
                //timer.Enabled = true;
                //timer.Interval = 5000;
                return true;
            }
            return false;
        }
    }
}
