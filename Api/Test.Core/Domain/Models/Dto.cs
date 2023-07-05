namespace Test.Core.Models
{
    public record CustomerDto(long Id, string FullName, DateTime DOB, string Email);
    public record StoreDto(long Id, string Name, string Location, IEnumerable<ProductDto> Products);

    public class ProductDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double Quantity { get; set; }
        public string StoreName { get; set; }

    }

    public class OrderDto
    {
        public long Id { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string StoreName { get; set; }
        public string StoreLocation { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }


    public class ProductViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public long StoreId { get; set; }
        public double Quantity { get; set; }
    }
    public class StoreViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }
    public class CustomerViewModel
    {
        public string FullName { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
    }

    public class OrderViewModel
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public string? StoreId { get; set; }
        public long ProductId { get; set; }
        public decimal OrderPaid { get; set; }
    }
}
