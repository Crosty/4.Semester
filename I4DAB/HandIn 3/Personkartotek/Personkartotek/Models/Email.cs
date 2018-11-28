using System;
using System.Collections.Generic;

namespace Personkartotek.Models
{
    public partial class Email
    {
        public int EmailId { get; set; }
        public int PersonId { get; set; }
        public string EmailAddress { get; set; }

        public Person Person { get; set; }
    }
}
