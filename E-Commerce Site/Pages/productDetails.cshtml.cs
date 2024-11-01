using AAAAAAAAAAA.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using System.Diagnostics;

namespace AAAAAAAAAAA.Pages
{
    public class productDetailsModel : PageModel
    {
		public readonly AppDataContext _db;

		private readonly UserManager<AppUser> _userManager;
		public Product product { get; set; }

		public UserReviewRatings UserReviewRatings { get; set; }

		public List<UserReviewRatings> fetchReviews { get; set; }

		public int totalRatings { get; set; }

		public int raitingId { get; set; }
		public int currentRating {  get; set; }

		public int userHasRated { get; set; }

        public int productId { get; set; }

		public string userRatingTitle { get; set; }

		public string userRatingDescription { get; set; }

		public int userRatingStatus { get; set; }

		public productDetailsModel(AppDataContext db, UserManager<AppUser> userManager)
		{
			_db = db;
			_userManager = userManager;
		}
		public void OnGet(int id)
        {
			var user = _userManager.GetUserId(User);
			Guid userId = Guid.Parse(user);
			productId = id;

			if (id == null)
			{
				NotFound();
			}

			product = _db.Products.Find(productId);

			fetchReviews = _db.ProductUserReview.Where(p => p.ProductId == productId).ToList();

			var userHasRated = _db.ProductUserReview.Where(p => p.ProductId == productId && p.AppUserId == userId).FirstOrDefault();

            Debug.WriteLine("I AM WRITING THIS OUT" + userHasRated);

            if (userHasRated==null)
			{
				userRatingStatus = 0;
			}
			else
            {
				userRatingStatus = 1;
			}

			var hasRating = _db.ProductUserReview.Where(p => p.ProductId == productId).Count();

			if (hasRating == 0) {
			       currentRating = 0;
			}
			else
			{
				int overall = 0;
				var rating = _db.ProductUserReview
					        .Where(p => p.ProductId == productId)
							.Select(p => p.UserProductRating);

				foreach ( var item in rating)
				{
					overall =+ item;
				}

				currentRating = overall / hasRating;
			}
		}

		public async Task<IActionResult> OnPostUpdate(int id, int userHasRated, string userRatingTitle, string userRatingDescription ) {

			var user = _userManager.GetUserId(User);
			Guid userId = Guid.Parse(user);

			var createReview = new UserReviewRatings
			{
				UserReviewText = userRatingDescription,
				UserReviewTitle = userRatingTitle,
				UserProductRating = userHasRated,
				AppUserId = userId,
				ProductId = id,
			};

			_db.ProductUserReview.Add(createReview);
			_db.SaveChanges();
			return RedirectToPage("Products");

		}
	}
}


