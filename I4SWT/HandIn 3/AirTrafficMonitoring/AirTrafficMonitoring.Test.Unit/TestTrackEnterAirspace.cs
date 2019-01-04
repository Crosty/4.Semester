using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.System;
using AirTrafficMonitoring.System.Domain;
using AirTrafficMonitoring.System.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace AirTrafficMonitoring.Test.Unit
{
    [TestFixture]
    [Author("SWTGruppe12")]
    public class TestTrackEnterAirspace
    {
        //Demand
        private IUpdateModule _updateModule;
        private IAirspace _airspace;
        private List<ITrack> _tracks;
        //uut
        private ITracksEnterAirspace _uut;
        //Required
        private Point _trackEntersAirspace;

        [SetUp]
        public void Setup()
        {
            //Arrange
            _updateModule = Substitute.For<IUpdateModule>();
            _airspace = Substitute.For<IAirspace>();

            _uut = new TracksEnterAirspace(_airspace, _updateModule);
            _uut.TracksEnteredAirspace += (sender, args) =>
            {
                _tracks = args.Data;
            };

            //Border is (10000, 10000, 500) to (90000, 90000, 20000).
            _trackEntersAirspace = new Point(50000, 50000, 10000);
        }

        [Test]
        public void TracksEnterAirspace_ChecksWhenTrackEntersAirspace()
        {
            var data = new List<ITrack>();
            var args = new EventTracks(data);

            var trackInsideAirspace = new Track("TagOne", _trackEntersAirspace, DateTime.Now);

            data.Add(trackInsideAirspace);

            _updateModule.TracksUpdated += Raise.EventWith(args);

            Assert.That(_tracks.Contains(trackInsideAirspace));
        }
    }
}
