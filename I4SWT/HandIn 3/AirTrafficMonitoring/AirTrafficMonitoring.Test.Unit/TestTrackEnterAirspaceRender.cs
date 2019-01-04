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
using NUnit.Framework.Internal;

namespace AirTrafficMonitoring.Test.Unit
{
    [TestFixture]
    [Author("SWTGruppe12")]
    public class TestTrackEnterAirspaceRender
    {
        //Demand
        private ITracksEnterAirspace _tracksEnterAirspace;
        private IDisplay _display;
        //uut
        private ITracksEnterAirspaceRender _uut;
        //Required
        private Point _trackOne;
        private Point _trackTwo;

        [SetUp]
        public void Setup()
        {
            //Arrange
            _tracksEnterAirspace = Substitute.For<ITracksEnterAirspace>();
            _display = Substitute.For<IDisplay>();

            _uut = new TracksEnterAirspaceRender(_tracksEnterAirspace, _display);

            _trackOne = new Point(25000, 25000, 2500);
            _trackTwo = new Point(35000, 35000, 3500);
        }

        [Test]
        public void TrackEnterAirspaceRender_ChecksIfTracksWithinAirspace_ReturnsMessage()
        {
            var data = new List<ITrack>();
            var args = new EventTracks(data);

            var trackOne = new Track("TagOne", _trackOne, DateTime.Now);
            var trackTwo = new Track("TagTwo", _trackTwo, DateTime.Now);

            data.Add(trackOne);
            data.Add(trackTwo); 

            _tracksEnterAirspace.TracksEnteredAirspace += Raise.EventWith(args);

            _display.Received(1).Write("*TRACKS ENTERED AIRSPACE*\n");
            _display.Received(1).Write("Tag: " + trackOne.Tag + ", Time: " + DateTime.Now);
            _display.Received(1).Write("Tag: " + trackTwo.Tag + ", Time: " + DateTime.Now);
        }
    }
    
}
