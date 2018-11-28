using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Personkartotek.Interfaces;
using Personkartotek.Models;
using Personkartotek.Repositories;

namespace Personkartotek.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PersonkartotekDBHandIn32Context _context;

        public UnitOfWork(PersonkartotekDBHandIn32Context context)
        {
            _context = context;
            People = new PersonRepo(_context);
        }

        public IPersonRepo People { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}