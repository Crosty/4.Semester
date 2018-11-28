using System;
using System.Collections.Generic;

namespace Personkartotek.Models
{
    public partial class Person
    {
        public Person()
        {
            Address = new HashSet<Address>();
            Email = new HashSet<Email>();
        }

        public int PersonId { get; set; }
        public string PersonType { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public ICollection<Address> Address { get; set; }
        public ICollection<Email> Email { get; set; }
    }
}
