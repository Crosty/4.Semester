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
    class TestTracksLeftAirspaceRender
    {
        private ITrackLeftAirspace _trackLeftAirspace;
        private IDisplay _display;

        private ITracksLeftAirspaceRender _uut;
        //Required
        private Point _trackOne;
        private Point _trackTwo;
        [SetUp]
        public void Setup()
        {
            _trackLeftAirspace = Substitute.For<ITrackLeftAirspace>();
            _display = Substitute.For<IDisplay>();

            _uut = new TracksLeftAirspaceRender(_trackLeftAirspace, _display);

            _trackOne = new Point(1, 1, 1);
            _trackTwo = new Point(300, 300, 5);
        }

        [Test]
        public void TrackLeftAirspaceRender_ChecksIfTracksWithinAirspace_ReturnsMessage()
        {
            var data = new List<ITrack>();
            var args = new EventTracks(data);

            var trackOne = new Track("TagOne", _trackOne, DateTime.Now);
            var trackTwo = new Track("TagTwo", _trackTwo, DateTime.Now);

            data.Add(trackOne);
            data.Add(trackTwo);

            _trackLeftAirspace.TracksleftAirspace += Raise.EventWith(args);

            _display.Received(1).Write("*TRACKS LEFT AIRSPACE*\n");
            _display.Received(1).Write("Tag: " + trackOne.Tag + ", Time: " + DateTime.Now);
            _display.Received(1).Write("Tag: " + trackTwo.Tag + ", Time: " + DateTime.Now);
        }
    }
}
