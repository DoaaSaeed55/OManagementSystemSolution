using OMS.Core.Entities;

namespace OManagementSystemSolution.APIs.DTOs
{
    public class ProductDTO
    {


        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        //public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
