using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Personkartotek.System.Models
{
    public class Person
    {
        #region Properties
        public virtual int PersonId { get; set; }
        public virtual string PersonType { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual string LastName { get; set; }
        #endregion

        #region Collections
        public virtual ICollection<Address> Addresses { get; set; }
        //ACollection is Alternative collection
        public virtual ICollection<AA> ACollection { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }
        public virtual ICollection<Email> Emails { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        #endregion
    }
}