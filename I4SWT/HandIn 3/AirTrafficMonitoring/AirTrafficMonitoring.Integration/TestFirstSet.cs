using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.System.Domain;
using AirTrafficMonitoring.System.Interfaces;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace AirTrafficMonitoring.Integration
{
    [TestFixture]
    [Author("SWTGruppe12")]
    //FirstSet is the first step of our system.
    //This includes a step from ObjectifyingModule -> FilterModule.
    //(Also TransponderReceiver, but not necessarily mentioned).
    public class TestFirstSet
    {
        //Demand
        //Faking TransponderReceiver
        private ITransponderReceiver _fakeTR;
        //iut
        private IFilterModule _iut;
        //Required
        private IObjectifyingModule _objectifyingModule;
        private IAirspace _airspace;
        private List<ITrack> _filteredTrackList;

        [SetUp]
        public void SetUp()
        {
            //Arrange
            _fakeTR = Substitute.For<ITransponderReceiver>();

            _objectifyingModule = new ObjectifyingModule(_fakeTR);
            _airspace = new Airspace(); 

            _iut = new FilterModule(_objectifyingModule, _airspace);

            _iut.TracksFiltered += (sender, args) =>
            {
                _filteredTrackList = args.Data;
            };
        }

        [TestCase("Tag23;10345;43562;4350;20160410235943156", "Tag24;24503;70594;2103;20180410135959931")]
        public void FirstSet_CreateTracksWithObjectifyingModule_FilterTracks(string trackOne, string trackTwo)
        {
            var data = new List<string>();
            var args = new RawTransponderDataEventArgs(data);

            var track1 = trackOne;
            var track2 = trackTwo;

            data.Add(track1);
            data.Add(track2);

            _fakeTR.TransponderDataReady += Raise.EventWith(args);

            Assert.That(_filteredTrackList.Count == 2);
            Assert.That(_filteredTrackList[0].Tag, Is.EqualTo(trackOne.Split(';')[0]));
            Assert.That(_filteredTrackList[1].Tag, Is.EqualTo(trackTwo.Split(';')[0]));
        }
    }
}
