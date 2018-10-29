using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.System.Interfaces;
using TransponderReceiver;

namespace AirTrafficMonitoring.System.Domain
{
    public class ObjectifyingModule : IObjectifyingModule
    {
        //Events occuring from the transponder
        public event EventHandler<EventTracks> TracksObjectified;

        public ObjectifyingModule(ITransponderReceiver transponderReceiver)
        {
            transponderReceiver.TransponderDataReady += CreateTracks;
        }

        //Receives data from TransponderReceiver
        //Creates tracks
        //Splits the information with ";"
        private void CreateTracks(object sender, RawTransponderDataEventArgs e)
        {
            var trackList = new List<ITrack>();
            foreach (var str in e.TransponderData)
            {
                var separate = str.Split(';');
                var track = new Track(separate[0], new Point(Int32.Parse(separate[1]), Int32.Parse(separate[2]), Int32.Parse(separate[3])),
                    DateTime.ParseExact(separate[4], "yyyyMMddHHmmssfff", null));
                trackList.Add(track);
            }
            NewTrackEvent(new EventTracks(trackList));
        }

        protected virtual void NewTrackEvent(EventTracks e)
        {
            TracksObjectified?.Invoke(this, e);
        }
    }
}
