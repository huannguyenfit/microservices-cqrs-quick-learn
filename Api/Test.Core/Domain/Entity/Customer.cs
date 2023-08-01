using Microsoft.EntityFrameworkCore;

namespace Test.Core.Entities
{
    public class Customer : BaseEntity
    {
        public string FullName { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }

        private ICollection<Order> _orders;
        public virtual ICollection<Order> Orders
        {
            get { return _orders ?? (_orders = new List<Order>()); }
            protected set { _orders = value; }
        }
    }

}
