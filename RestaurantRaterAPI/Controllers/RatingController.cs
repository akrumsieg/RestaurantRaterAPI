using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRaterAPI.Controllers
{
    public class RatingController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();

        [HttpPost]
        public async Task<IHttpActionResult> PostRating(Rating model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Restaurant restaurant = await _context.Restaurants.FindAsync(model.RestaurantID);
            if (restaurant == null)
            {
                return BadRequest($"The target restaurant with the ID of {model.RestaurantID} does not exist.");
            }

            _context.Ratings.Add(model);
            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok($"You rated {restaurant.Name} successfully.");
            }

            return InternalServerError();
        }

        //get all ratings??
        //get rating by restaurant id??


        //update rating
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRating(int id, Rating updatedRating)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Rating rating = await _context.Ratings.FindAsync(id);
            if (rating == null) return BadRequest($"The target rating with the ID of {rating.ID} does not exist.");

            rating.FoodScore = updatedRating.FoodScore;
            rating.EnvironmentScore = updatedRating.EnvironmentScore;
            rating.CleanlinessScore = updatedRating.CleanlinessScore;
            if (await _context.SaveChangesAsync() == 1) return Ok("You updated the rating successfully.");

            return InternalServerError();
        }


        //delete rating


    }
}
