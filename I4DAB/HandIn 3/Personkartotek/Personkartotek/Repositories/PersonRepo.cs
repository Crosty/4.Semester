using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Personkartotek.Interfaces;
using Personkartotek.Models;

namespace Personkartotek.Repositories
{
    public class PersonRepo : Repository<Person>, IPersonRepo
    {
        private readonly PersonkartotekDBHandIn32Context _context;
        public PersonRepo(PersonkartotekDBHandIn32Context context) : base(context)
        {
            _context = context;
        }
    }
}
