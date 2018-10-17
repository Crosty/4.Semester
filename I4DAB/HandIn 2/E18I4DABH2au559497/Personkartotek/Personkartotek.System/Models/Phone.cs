using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Personkartotek.System.Models
{
    public class Phone
    {
        #region Properties
        public virtual int PhoneId { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string PhoneProvider { get; set; }
        public virtual string PhoneType { get; set; }

        public virtual long Person { get; set; }
        #endregion
    }
}