using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.System.Interfaces;

namespace AirTrafficMonitoring.System.Domain
{
    public class SeparationRender : ISeparationRender
    {
        private readonly IDisplay _display;

        public SeparationRender(ISeparationModule separationModule, IDisplay display)
        {
            _display = display;
            separationModule.TracksSeparated += RenderSeparations;
        }

        private void RenderSeparations(object sender, EventSeparations e)
        {
            //_display.Clear();
            _display.Write("*SEPARATIONS*\n");
            foreach (var collision in e.CollisionsData)
            {
                var str = "TagOne: " + collision.TagOne + " TagTwo: " + collision.TagTwo + " Time: " +
                          collision.Timestamp;
                _display.Write(str);
            }
        }
    }
}
