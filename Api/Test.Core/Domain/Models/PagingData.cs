using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Core.Domain.Models
{
    public class PagingData<T> 
    {
        public ICollection<T> Data { get; set; }

        public int Total { get; set; }
    }
}
