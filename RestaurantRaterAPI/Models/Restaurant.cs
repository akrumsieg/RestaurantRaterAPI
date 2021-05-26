using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    public class Restaurant
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();

        public double Rating
        {
            get
            {
                double totalAverageRating = 0;
                foreach (Rating rating in Ratings)
                {
                    totalAverageRating += rating.AverageRating;
                }
                return totalAverageRating / Ratings.Count();
            }
        }

        //AverageFoodScore
        public double AverageFoodScore
        {
            get
            {
                double sum = 0;
                foreach (Rating rating in Ratings)
                {
                    sum += rating.FoodScore;
                }
                return sum / Ratings.Count();
            }
        }

        //AverageEnvironmentScore
        public double AverageEnvironmentScore
        {
            get
            {
                double sum = 0;
                foreach (Rating rating in Ratings)
                {
                    sum += rating.EnvironmentScore;
                }
                return sum / Ratings.Count();
            }
        }

        //AverageCleanlinessScore
        public double AverageCleanlinessScore
        {
            get
            {
                double sum = 0;
                foreach (Rating rating in Ratings)
                {
                    sum += rating.CleanlinessScore;
                }
                return sum / Ratings.Count();
            }
        }

        public bool IsRecommended => Rating > 8.5;
    }
}