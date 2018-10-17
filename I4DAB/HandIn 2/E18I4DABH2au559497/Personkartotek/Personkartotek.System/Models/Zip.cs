using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Personkartotek.System.Models
{
    public class Zip
    {
        #region Properties
        public virtual int ZipId { get; set; }
        public virtual string City { get; set; }
        public virtual string Country { get; set; }
        public virtual string ZipCode { get; set; }
        #endregion

        #region Collections
        public virtual ICollection<ZipList> ZipLists { get; set; }
        #endregion
    }
}