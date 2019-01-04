using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.System.Interfaces;

namespace AirTrafficMonitoring.System.Domain
{
    public class UpdateModule : IUpdateModule
    {
        private List<ITrack> _oldTracks;
        public event EventHandler<EventTracks> TracksUpdated;

        public UpdateModule(IFilterModule filterModule)
        {
            _oldTracks = new List<ITrack>();
            filterModule.TracksFiltered += UpdateTracks;
        }

        //Updates the filtered tracks and calculates course & velocity
        private void UpdateTracks(object sender, EventTracks e)
        {
            var updatedTracks = new List<ITrack>();
            foreach (var filtered in e.Data)
            {
                //If filtered.tag is on _oldtracks, update course and velocity
                //If not, add filtered
                var oldTracks = _oldTracks.FirstOrDefault(t => t.Tag == filtered.Tag);

                if (oldTracks != null)
                {
                    filtered.Course = CalculateCourse(oldTracks.Position, filtered.Position);
                    filtered.Velocity = CalculateVelocity(oldTracks.Position, filtered.Position,
                        oldTracks.Timestamp, filtered.Timestamp);
                }

                updatedTracks.Add(filtered);
            }
            _oldTracks = updatedTracks;
            UpdatedTracksEvent(new EventTracks(updatedTracks));
        }

        protected virtual void UpdatedTracksEvent(EventTracks e)
        {
            TracksUpdated?.Invoke(this, e);
        }

        //Calculates velocity of the tracks
        private double CalculateVelocity(Point oldPoint, Point newPoint, DateTime oldTime, DateTime newTime)
        {
            //TimeSpan represents a time interval - time interval between the old and new.
            TimeSpan time = newTime - oldTime;
            //Gets the value of the current TimeSpan - represents the total number of seconds.
            var timeDiff = time.TotalSeconds;
            //Square root of two Power values, in this case the coordination of the old and new.
            //This shows the distance it moved during the TimeSpan
            var distance = Math.Sqrt(Math.Pow((newPoint.X - oldPoint.X), 2) + Math.Pow((newPoint.Y - oldPoint.Y), 2));
            //Calculates the speed
            var velocity = distance / timeDiff;

            return (int)velocity;
        }

        //Calculates course of the tracks
        private double CalculateCourse(Point oldPoint, Point newPoint)
        {
            //Math.Abs: Absolute value, in case it goes below 0.
            double xcoord = Math.Abs(newPoint.X - oldPoint.X);
            double ycoord = Math.Abs(newPoint.Y - oldPoint.Y);

            double direction = 0;
            
            if (xcoord == 0 && ycoord == 0)
            {
                return Double.NaN;
            }
            else
            {
                //Returns an angle measured in radian
                double radian = Math.Atan2(ycoord, xcoord);
                direction = radian / Math.PI * 180;

                direction -= 90;
                if (direction < 0)
                {
                    direction += 360;
                }

            }

            return (int)direction;
        }
    }
}
