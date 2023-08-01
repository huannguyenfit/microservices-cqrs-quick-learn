using Microsoft.EntityFrameworkCore;
using Test.Data.Mapping;

namespace Test.Core.Entities
{

    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public decimal Price { get; set; }
        public double Quantity { get; set; }
        public long StoreId { get; set; }
        public virtual Store Store { get; set; }

        
    }

}
