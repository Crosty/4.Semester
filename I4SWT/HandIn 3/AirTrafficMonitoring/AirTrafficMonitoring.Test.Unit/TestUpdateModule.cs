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
    public class TestUpdateModule
    {
        //Demand
        private IFilterModule _filterModule;
        private List<ITrack> _tracks;
        //uut
        private IUpdateModule _uut;
        //Required
        private Point _insideAirspacePointA;
        private Point _insideAirspacePointB;
        private Point _insideAirspacePointC;
        private Point _insideAirspacePointD;

        [SetUp]
        public void Setup()
        {
            //Arrange
            _filterModule = Substitute.For<IFilterModule>();
            _uut = new UpdateModule(_filterModule);

            _uut.TracksUpdated += (sender, args) =>
            {
                _tracks = args.Data;
            };
            

            _insideAirspacePointA = new Point(10100, 10500, 8000);
            _insideAirspacePointB = new Point(70000, 25000, 3900);
            _insideAirspacePointC = new Point(80000, 35000, 3200);
            _insideAirspacePointD = new Point(26000, 26000, 4800);
        }

        [Test]
        public void UpdateModule_UpdateListOfTracks()
        {
            var data = new List<ITrack>();
            var args = new EventTracks(data);

            var pointA = new Track("Tag1", _insideAirspacePointA, DateTime.Now);
            var pointB = new Track("Tag2", _insideAirspacePointB, DateTime.Now);
            var pointC = new Track("Tag3", _insideAirspacePointC, DateTime.Now);
            var pointD = new Track("Tag4", _insideAirspacePointD, DateTime.Now);

            data.Add(pointA);
            data.Add(pointB);
            data.Add(pointC);
            data.Add(pointD);

            _filterModule.TracksFiltered += Raise.EventWith(args);

            Assert.That(_tracks.Count(), Is.EqualTo(4));
            Assert.That(_tracks.Contains(pointA));
            Assert.That(_tracks.Contains(pointB));
            Assert.That(_tracks.Contains(pointC));
            Assert.That(_tracks.Contains(pointD));

        }

    }
}
