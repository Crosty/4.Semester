using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Personkartotek.App;

namespace Personkartotek
{
    public class Run
    {
        private static void Main(string[] args)
        {
            App.App runApp = new App.App();
            runApp.TheApp();
        }
    }
}
