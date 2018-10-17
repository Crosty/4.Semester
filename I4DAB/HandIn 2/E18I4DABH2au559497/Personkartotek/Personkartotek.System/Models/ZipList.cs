using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Personkartotek.System.Models
{
    public class ZipList
    {
        #region Properties
        public virtual int ZipListId { get; set; }
        public virtual string City { get; set; }
        public virtual string Country { get; set; }
        public virtual string ZipCode { get; set; }

        #endregion
    }
}