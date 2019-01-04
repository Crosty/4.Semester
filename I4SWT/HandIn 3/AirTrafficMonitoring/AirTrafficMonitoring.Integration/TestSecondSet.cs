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
    //SecondSet is the second step of our system
    //This includes a step from ObjectifyingModule -> FilterModule -> UpdateModule.
    public class TestSecondSet
    {
        //Demand
        //Faking TransponderReceiver
        private ITransponderReceiver _fakeTR;
        //iut
        private IUpdateModule _iut;
        //Required
        private IObjectifyingModule _objectifyingModule;
        private IFilterModule _filterModule;
        private IAirspace _airspace;
        private List<ITrack> _updatedTrackList;

        [SetUp]
        public void Setup()
        {
            //Arrange
            _fakeTR = Substitute.For<ITransponderReceiver>();

            _airspace = new Airspace();
            _objectifyingModule = new ObjectifyingModule(_fakeTR);
            _filterModule = new FilterModule(_objectifyingModule, _airspace);
            

            _iut = new UpdateModule(_filterModule);

            _iut.TracksUpdated += (sender, args) =>
            {
                _updatedTrackList = args.Data;
            };
        }

        [TestCase("Tag23;20345;10000;1000;20180410135958999", "Tag23;10346;43562;4350;2018041015959999", double.NaN, -2147483648.0)]
        public void SecondSet_CreateTracks_FilterTracks_UpdateTracks(string trackOneFirst, string trackOneSecond, double trackOneCourse, double trackOneVelocity)
        {
            //First position
            var data = new List<string>();
            var args = new RawTransponderDataEventArgs(data);

            var track1 = trackOneFirst;

            data.Add(track1);
            
            _fakeTR.TransponderDataReady += Raise.EventWith(args);

            //Second position
            data = new List<string>();

            track1 = trackOneSecond;

            data.Add(track1);

            _fakeTR.TransponderDataReady += Raise.EventWith(args);

            Assert.That(_updatedTrackList.Count == 1);
            Assert.That(_updatedTrackList[0].Tag, Is.EqualTo(trackOneFirst.Split(';')[0]));
            Assert.That(_updatedTrackList[0].Course, Is.EqualTo(trackOneCourse));
            Assert.That(_updatedTrackList[0].Velocity, Is.EqualTo(trackOneVelocity));
        }
    }
}
