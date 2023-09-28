using MinistryTask.Domain.Enums;

namespace MinistryTask.Domain.Models
{
    public class ProductStatus
    {
        public int ProductStatusId { get; set; }
        public StatusOfProduct Status { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
