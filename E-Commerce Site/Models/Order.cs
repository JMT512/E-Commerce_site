namespace AAAAAAAAAAA.Models
{
	public class Order
	{
		public int OrderId { get; set; }


		public Guid AppUserId { get; set; }
		public AppUser AppUser { get; set; }
		public int ProductId { get; set; }

		public Product product { get; set; }
	}
}
