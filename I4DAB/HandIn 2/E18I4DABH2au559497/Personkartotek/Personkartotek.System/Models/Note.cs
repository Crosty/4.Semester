using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Personkartotek.System.Models
{
    public class Note
    {
        #region Properties
        public virtual int NoteId { get; set; }
        public virtual string Description { get; set; }

        public virtual long Person { get; set; }
        #endregion
    }
}