using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Personkartotek.System.Models
{
    public class Email
    {
        #region Properties
        public virtual int EmailId { get; set; }
        public virtual string EmailAddress { get; set; }

        public virtual long Person { get; set; }
        #endregion
    }
}