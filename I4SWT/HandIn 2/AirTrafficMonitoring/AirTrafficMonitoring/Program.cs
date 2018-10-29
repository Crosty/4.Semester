using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirTrafficMonitoring.System.Domain;
using AirTrafficMonitoring.System.Interfaces;
using TransponderReceiver;

namespace AirTrafficMonitoring
{
    class Program
    {
        static void Main(string[] args)
        {
            IDisplay display = new Display();
            IAirspace airspace = new Airspace();
            ILog log = new Log();

            ITransponderReceiver transponderReceiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            IObjectifyingModule objectifyingModule = new ObjectifyingModule(transponderReceiver);
            IFilterModule filterModule = new FilterModule(objectifyingModule, airspace);
            IUpdateModule updateModule = new UpdateModule(filterModule);
            ISeparationModule separationModule = new SeparationModule(updateModule, log);

            ISeparationRender separationRender = new SeparationRender(separationModule, display);
            ITrackRender trackRender = new TrackRender(updateModule, display);

            Console.ReadKey();
        }
    }
}
