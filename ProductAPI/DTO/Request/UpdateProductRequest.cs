namespace ProductAPI.DTO.Request
{
    public class UpdateProductRequest
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}
