using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.System.Interfaces;

namespace AirTrafficMonitoring.System.Domain
{
    public class SeparationModule : ISeparationModule
    {
        private readonly ILog _log;
        private List<ICollision> _oldSeparationsTrack;
        public event EventHandler<EventSeparations> TracksSeparated;

        public SeparationModule(IUpdateModule updateModule, ILog log)
        {
            _log = log;
            _oldSeparationsTrack = new List<ICollision>();
            updateModule.TracksUpdated += SeparationTracks;
        }

        private void SeparationTracks(object sender, EventTracks e)
        {
            var updatedSeparationsTracks = new List<ICollision>();

            foreach (var separationTrack in e.Data)
            {
                foreach (var separationTrackTwo in e.Data)
                {
                    //If conflict occurs, do calculate, change positions from old to a new
                    //update it.
                    if (separationTrack.Tag != separationTrackTwo.Tag)
                    {
                        if (CalculateSeparationTracks(separationTrack.Position, separationTrackTwo.Position))
                        {
                            var oldSeparationTrack = updatedSeparationsTracks.FirstOrDefault(t =>
                                t.TagOne == separationTrack.Tag || t.TagTwo == separationTrack.Tag);
                            if (oldSeparationTrack == null)
                            {
                                updatedSeparationsTracks.Add(new Collision(separationTrack.Tag, separationTrackTwo.Tag,
                                    separationTrack.Timestamp));
                            }
                        }
                    }
                }
            }

            foreach (var separation in updatedSeparationsTracks)
            {
                var oldSeparationsTracks = _oldSeparationsTrack.FirstOrDefault(t =>
                    t.TagOne == separation.TagOne && t.TagTwo == separation.TagTwo);
                if (oldSeparationsTracks == null)
                {
                    _log.Logging("TagOne: " + separation.TagOne + ";" + " TagTwo: " + separation.TagTwo + ";" + " Time: " + separation.Timestamp);
                }
            }

            _oldSeparationsTrack = updatedSeparationsTracks;
            UpdatedSeparationsTracksEvent(new EventSeparations(updatedSeparationsTracks));
        }

        protected virtual void UpdatedSeparationsTracksEvent(EventSeparations e)
        {
            TracksSeparated?.Invoke(this, e);
        }

        //Calculates the tracks distance, if this is triggered it alerts the conflict
        private bool CalculateSeparationTracks(Point trackOne, Point trackTwo)
        {
            //If return true, conflict occured
            //If return false, only one or none of the two statements are valid.
            //Vertical separation
            if ((trackOne.Altitude - trackTwo.Altitude) < 300)
            {
                //Horizontal separation
                double xcoord;
                double ycoord;

                xcoord = Math.Pow(trackOne.X - trackTwo.X, 2);
                ycoord = Math.Pow(trackOne.Y - trackTwo.Y, 2);

                if (Math.Sqrt(xcoord + ycoord) < 5000)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
