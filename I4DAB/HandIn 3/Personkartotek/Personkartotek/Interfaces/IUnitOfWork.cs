using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personkartotek.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPersonRepo People { get; }
        int Complete(); 
    }
}
