using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Core
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public DateTime LastTime { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedById { get; set; }
        public long LastUserId { get; set; }
        public DateTime LastModified { get; set; }

    }
}
