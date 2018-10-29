using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.System.Interfaces;
using TransponderReceiver;

namespace AirTrafficMonitoring.System.Domain
{
    public class FilterModule : IFilterModule
    {
        private IAirspace _airspace;
        public event EventHandler<EventTracks> TracksFiltered;

        public FilterModule(IObjectifyingModule objectifyingModule, IAirspace airspace)
        {
            _airspace = airspace;
            objectifyingModule.TracksObjectified += FilterTracks;
        }

        //Filter tracks - focuses only on tracks within the airspace
        //Ignores irrelevant airplanes
        private void FilterTracks(object sender, EventTracks e)
        {
            var filtered = new List<ITrack>();
            foreach (var track in e.Data)
            {
                if (_airspace.CheckIfWithinAirspace(track.Position))
                    filtered.Add(track);
            }

            NewFiltered(new EventTracks(filtered));
        }

        protected virtual void NewFiltered(EventTracks e)
        {
            TracksFiltered?.Invoke(this, e);
        }
    }
}
