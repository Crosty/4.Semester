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
            //Enter the functions here:
            IDisplay display = new Display();
            IAirspace airspace = new Airspace();
            ILog log = new Log();

            ITransponderReceiver transponderReceiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
            IObjectifyingModule objectifyingModule = new ObjectifyingModule(transponderReceiver);
            IFilterModule filterModule = new FilterModule(objectifyingModule, airspace);
            IUpdateModule updateModule = new UpdateModule(filterModule);
            ISeparationModule separationModule = new SeparationModule(updateModule, log);
            ITracksEnterAirspace tracksEnterAirspace = new TracksEnterAirspace(airspace, updateModule);

            ISeparationRender separationRender = new SeparationRender(separationModule, display);
            ITrackRender trackRender = new TrackRender(updateModule, display);
            ITracksEnterAirspaceRender tracksEnterAirspaceRender = new TracksEnterAirspaceRender(tracksEnterAirspace, display);

            Console.ReadKey();
        }
    }
}
