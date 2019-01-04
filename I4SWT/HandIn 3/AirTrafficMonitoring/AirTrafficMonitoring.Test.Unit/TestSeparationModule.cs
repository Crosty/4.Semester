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
    public class TestSeparationModule
    {
        //Demand
        private IUpdateModule _updateModule;
        private List<ICollision> _collisions;
        private ILog _log;

        //uut
        private ISeparationModule _uut;

        //Required
        private Point _trackOne;
        private Point _trackTwo;

        [SetUp]
        public void Setup()
        {
            //Arrange
            _updateModule = Substitute.For<IUpdateModule>();
            _log = Substitute.For<ILog>();

            _uut = new SeparationModule(_updateModule, _log);

            _uut.TracksSeparated += (sender, args) =>
            {
                _collisions = args.CollisionsData;
            };

            //If they are within 3000 coords and 300 altitude, the separation will occur.
            _trackOne = new Point(20000, 20000, 1000);
            _trackTwo = new Point(20500, 20500, 1050);
        }

        [Test]
        public void SeparateTracks_ChecksIfTracksAreTooCloseToEachOther_ContainsListOfSeparatingTracks()
        {
            var data = new List<ITrack>();
            var args = new EventTracks(data);

            var trackOne = new Track("TagOne", _trackOne, DateTime.Now);
            var trackTwo = new Track("TagTwo", _trackTwo, DateTime.Now);

            data.Add(trackOne);
            data.Add(trackTwo);

            _updateModule.TracksUpdated += Raise.EventWith(args);

            Assert.That(_collisions.Count(), Is.EqualTo(1));
            Assert.That(_collisions.Any(), Is.True);
        }

        [Test]
        public void SeparateTracks_LoggingTheSeparatedTracks()
        {
            var data = new List<ITrack>();
            var args = new EventTracks(data);

            var trackOneLog = new Track("TagOne", _trackOne, DateTime.Now);
            var trackTwoLog = new Track("TagTwo", _trackTwo, DateTime.Now);

            data.Add(trackOneLog);
            data.Add(trackTwoLog);

            _updateModule.TracksUpdated += Raise.EventWith(args);

            _log.Received().Logging("TagOne: TagOne; TagTwo: TagTwo; " + "Time: " + DateTime.Now);
        }
    }
}