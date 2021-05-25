using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        [HttpPost]
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

        //GetAll
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
            return Ok(restaurants);
        }

        //GetById
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant != null)
            {
                return Ok(restaurant);
            }
            return NotFound();
        }

        //Update(PUT)
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRestaurant(int id, Restaurant updatedRestaurant)
        {
            if (ModelState.IsValid)
            {
                Restaurant restaurant = await _context.Restaurants.FindAsync(id);
                if(restaurant != null)
                {
                    restaurant.Name = updatedRestaurant.Name;
                    restaurant.Address = updatedRestaurant.Address;
                    restaurant.Rating = updatedRestaurant.Rating;

                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        //Delete
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRestaurant (int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null) //different check logic
            {
                return NotFound();
            }

            _context.Restaurants.Remove(restaurant);

            if (await _context.SaveChangesAsync() == 1) //if one db entry was changed
            {
                return Ok("The restaurant was successfully deleted.");
            }

            return InternalServerError();
        }
    }
}
