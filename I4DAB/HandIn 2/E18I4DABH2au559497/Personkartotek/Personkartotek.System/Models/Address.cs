using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Personkartotek.System.Models
{
    public class Address
    {
        #region Properties
        public virtual int AddressId { get; set; }
        public virtual string StreetName { get; set; }
        public virtual string HouseNumber { get; set; }
        public virtual string City { get; set; }
        public virtual long Person { get; set; }
        public virtual long Zip { get; set; }
        #endregion

        #region Collections
        public virtual ICollection<Zip> Zips { get; set; }
        #endregion
    }
}