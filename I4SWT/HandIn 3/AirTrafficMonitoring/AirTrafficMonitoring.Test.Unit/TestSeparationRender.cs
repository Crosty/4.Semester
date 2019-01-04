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
    public class TestSeparationRender
    {
        //Demand
        private ISeparationModule _separationModule;
        private IDisplay _display;
        //uut
        private ISeparationRender _uut;
        //Required
        private string _trackOne;
        private string _trackTwo;

        [SetUp]
        public void Setup()
        {
            //Arrange
            _separationModule = Substitute.For<ISeparationModule>();
            _display = Substitute.For<IDisplay>();

            _uut = new SeparationRender(_separationModule, _display);

            _trackOne = "TagOne";
            _trackTwo = "TagTwo";
        }

        [Test]
        public void SeparationRender_RenderTheSeparationOnDisplay()
        {
            var data = new List<ICollision>();
            var args = new EventSeparations(data);

            var separation = new Collision(_trackOne, _trackTwo, DateTime.Now);

            data.Add(separation);

            _separationModule.TracksSeparated += Raise.EventWith(args);

            _display.Received(1).Write("*SEPARATIONS*\n");
            _display.Received(1).Write("TagOne: " + separation.TagOne + " TagTwo: " + separation.TagTwo + " Time: " +
                                       DateTime.Now);
        }
    }
}
