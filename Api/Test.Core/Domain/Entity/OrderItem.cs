using Microsoft.EntityFrameworkCore;
using Test.Data.Mapping;

namespace Test.Core.Entities
{
    public class OrderItem : BaseEntity
    {
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public decimal Price { get; set; }
        public double Quantity { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }

}
