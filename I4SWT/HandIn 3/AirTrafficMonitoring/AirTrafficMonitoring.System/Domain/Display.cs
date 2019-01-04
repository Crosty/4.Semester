using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.System.Interfaces;

namespace AirTrafficMonitoring.System.Domain
{
    public class Display : IDisplay
    {
        public void Write(string str)
        {
            Console.WriteLine(str);
        }

        //Keeps the console clean - There's no history of events
        public void Clear()
        {
            Console.Clear();
        }
    }
}
