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
    public class RestaurantController : ApiController
    {
        private RestaurantDbContext _context = new RestaurantDbContext();
        public async Task<IHttpActionResult> PostRestaurant(Restaurant model)
        {
            if (model == null)
            {
                return BadRequest("Your request body cannot be empty");
            }
            if (ModelState.IsValid) //checks if all required properties are included
            {
                _context.Restaurants.Add(model); //stages the add
                await _context.SaveChangesAsync(); //saves changes

                return Ok(); //http 200 message
            }
            return BadRequest(ModelState); //generates message saying ModelState is invalid
        }
    }
}
