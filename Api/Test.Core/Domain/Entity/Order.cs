using Microsoft.EntityFrameworkCore;
using Test.Data.Mapping;

namespace Test.Core.Entities
{
    public class Order : BaseEntity
    {
        public long StoreId { get; set; }
        public long CustomerId { get; set; }
        public decimal OrderTotal { get; set; }
        public virtual Store Store { get; set; }
        public virtual Customer Customer { get; set; }

        private ICollection<OrderItem> _orderItems;
        public virtual ICollection<OrderItem> OrderItems
        {
            get { return _orderItems ?? (_orderItems = new List<OrderItem>()); }
            protected set { _orderItems = value; }
        }


    }
}
