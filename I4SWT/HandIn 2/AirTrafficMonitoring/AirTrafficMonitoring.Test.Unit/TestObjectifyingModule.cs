using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.System.Interfaces;
using AirTrafficMonitoring.System.Domain;
using TransponderReceiver;
using NUnit.Framework;
using NSubstitute;

namespace AirTrafficMonitoring.Test.Unit
{
    [TestFixture]
    [Author("SWTGruppe12")]
    public class TestObjectifyingModule
    {
        //Demand
        private ITransponderReceiver _tr;
        //uut
        private IObjectifyingModule _uut;
        //Required
        private List<ITrack> _trackList;

        [SetUp]
        public void Setup()
        {
            //Arrange
            _tr = Substitute.For<ITransponderReceiver>();

            _uut = new ObjectifyingModule(_tr);

            _uut.TracksObjectified += (sender, args) =>
            {
                _trackList = args.Data;
            };
        }

        [TestCase("Tag23;10345;43562;4350;20160410235943156")]
        [TestCase("Tag24;24503;70594;2103;20180410135959931")]
        [TestCase("Tag25;24343;70454;2203;20130410135959931")]
        public void CreateTracks_CreateTracksAddToList_ContainsList(string trackList)
        {
            var data = new List<string> { trackList };
            var args = new RawTransponderDataEventArgs(data);

            _tr.TransponderDataReady += Raise.EventWith(args);

            Assert.That(_trackList[0].Tag, Is.EqualTo(trackList.Split(';')[0]));
            Assert.That(_trackList[0].Position.X, Is.EqualTo(Int32.Parse(trackList.Split(';')[1])));
            Assert.That(_trackList[0].Position.Y, Is.EqualTo(Int32.Parse(trackList.Split(';')[2])));
            Assert.That(_trackList[0].Position.Altitude, Is.EqualTo(Int32.Parse(trackList.Split(';')[3])));
            Assert.That(_trackList[0].Timestamp, Is.EqualTo(DateTime.ParseExact(trackList.Split(';')[4], "yyyyMMddHHmmssfff", null)));
        }
    }
}
