using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Personkartotek.System.Models
{
    public class AA
    {
        #region Properties
        public virtual int AlternativeId { get; set; }
        public virtual string StreetName { get; set; }
        public virtual string HouseNumber { get; set; }
        public virtual string City { get; set; }
        public virtual string AddressType { get; set; }

        public virtual long Person { get; set; }
        public virtual long Address { get; set; }
        #endregion
    }
}