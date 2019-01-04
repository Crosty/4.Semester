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
    public class TestFilterModule
    {
        //Demand
        private IObjectifyingModule _objectifyingModule;
        private IAirspace _airspace;
        private List<ITrack> _tracks;
        //uut
        private IFilterModule _uut;
        //Required
        private Point _insideAirspace;
        private Point _outsideAirspace;

        [SetUp]
        public void Setup()
        {
            //Arrange
            _objectifyingModule = Substitute.For<IObjectifyingModule>();
            _airspace = Substitute.For<IAirspace>();

            _uut = new FilterModule(_objectifyingModule, _airspace);
            _uut.TracksFiltered += (sender, args) =>
            {
                _tracks = args.Data;
            };

            //Border is (10000, 10000, 500) to (90000, 90000, 20000).
            _insideAirspace = new Point(50000, 50000, 10000);
            _outsideAirspace = new Point(500, 500, 5);

            //Assumptions
            _airspace.CheckIfWithinAirspace(_insideAirspace).Returns(true);
            _airspace.CheckIfWithinAirspace(_outsideAirspace).Returns(false);
        }

        [Test]
        public void FilterTracks_ChecksIfTracksWithinAirspaceOrNot_ContainsListOfTracksWithinAirspace()
        {
            var data = new List<ITrack>();
            var args = new EventTracks(data);

            var insideAirspace = new Track("TagOne", _insideAirspace, DateTime.Now);
            var outsideAirspace = new Track("TagTwo", _outsideAirspace, DateTime.Now);

            data.Add(insideAirspace);
            data.Add(outsideAirspace);

            _objectifyingModule.TracksObjectified += Raise.EventWith(args);

            //CheckWithInAirspace if True, it is correct, If false it is incorrect.
            Assert.That(_tracks.Contains(insideAirspace), Is.True);
            Assert.That(_tracks.Contains(outsideAirspace), Is.False);
        }
    }
}
