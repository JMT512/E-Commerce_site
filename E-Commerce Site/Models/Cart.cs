namespace AAAAAAAAAAA.Models
{
    public class Cart
    {
		public int CartId { get; set; }
		public int quantity { get; set; }
        public Guid AppUserId { get; set; }
		public AppUser AppUser { get; set; }
		public int ProductId {  get; set; }
        public virtual Product Product { get; set; }
    }
}
