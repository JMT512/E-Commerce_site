namespace AAAAAAAAAAA.Models
{
	public class UserReviewRatings
	{
		public int UserReviewRatingsId { get; set; }

		public string UserReviewText { get; set; }

		public string UserReviewTitle { get; set; }

		public int UserProductRating { get; set; }

		public Guid AppUserId { get; set; }
		public AppUser AppUser { get; set; }
		public int ProductId { get; set; }

		public Product product { get; set; }
	}
}
