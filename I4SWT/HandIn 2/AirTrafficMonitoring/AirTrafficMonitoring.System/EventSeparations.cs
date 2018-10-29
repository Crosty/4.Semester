using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.System.Interfaces;

namespace AirTrafficMonitoring.System
{
    public class EventSeparations : EventArgs
    {
        public List<ICollision> CollisionsData;

        public EventSeparations(List<ICollision> collisionsData)
        {
            CollisionsData = collisionsData;
        }
    }
}
