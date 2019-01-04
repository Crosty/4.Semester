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

    public class TestTrackRender
    {
        //Demand
        private IDisplay _display;
        private IUpdateModule _updateModule;
        //uut
        private TrackRender _uut;
        //Required
        private Point _trackOne;
        private Point _trackTwo;

        [SetUp]
        public void Setup()
        {
            //Arrange
            _updateModule = Substitute.For<IUpdateModule>();
            _display = Substitute.For<IDisplay>();

            _uut = new TrackRender(_updateModule, _display);

            _trackOne = new Point(25000, 25000, 2500);
            _trackTwo = new Point(35000, 35000, 3500);
        }

        [Test]
        public void TrackRender_RenderTheTracksOnDisplay()
        {
            var data = new List<ITrack>();
            var args = new EventTracks(data);

            var trackOne = new Track("TagOne", _trackOne, DateTime.Now);
            var trackTwo = new Track("TagTwo", _trackTwo, DateTime.Now);

            data.Add(trackOne);
            data.Add(trackTwo);

            _updateModule.TracksUpdated += Raise.EventWith(args);

            _display.Received(1).Clear();
            _display.Received(1).Write("*TRACKS*\n");
            _display.Received(1).Write("Tag: " + trackOne.Tag + " Current position: " + trackOne.Position.X +
                                       " east, " + trackOne.Position.Y + " north, altitude: " + trackOne.Position.Altitude +
                                       "m, Velocity: " + trackOne.Velocity + "m/s, Course: " + trackOne.Course + " degree");

            _display.Received(1).Write("Tag: " + trackTwo.Tag + " Current position: " + trackTwo.Position.X +
                                       " east, " + trackTwo.Position.Y + " north, altitude: " + trackTwo.Position.Altitude +
                                       "m, Velocity: " + trackTwo.Velocity + "m/s, Course: " + trackTwo.Course + " degree");
        }
    }
}