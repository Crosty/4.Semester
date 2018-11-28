using System;
using System.Collections.Generic;

namespace Personkartotek.Models
{
    public partial class Zip
    {
        public Zip()
        {
            Address = new HashSet<Address>();
        }

        public int ZipId { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        public ICollection<Address> Address { get; set; }
    }
}
