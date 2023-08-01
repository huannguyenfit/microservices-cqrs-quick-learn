using Microsoft.EntityFrameworkCore;
using Test.Data.Mapping;

namespace Test.Core.Entities
{

    public class Store : BaseEntity
    {
        public string Name { get; set; }
        public string Location { get; set; }

        private ICollection<Order> _orders;
        public virtual ICollection<Order> Orders
        {
            get { return _orders ?? (_orders = new List<Order>()); }
            protected set { _orders = value; }
        }

        private ICollection<Product> _products;
        public virtual ICollection<Product> Products
        {
            get { return _products ?? (_products = new List<Product>()); }
            protected set { _products = value; }
        }
    }

}
