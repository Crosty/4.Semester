using System;
using System.Collections.Generic;

namespace Personkartotek.Models
{
    public partial class Address
    {
        public int AddressId { get; set; }
        public int PersonId { get; set; }
        public int ZipId { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }

        public Person Person { get; set; }
        public Zip Zip { get; set; }
    }
}
