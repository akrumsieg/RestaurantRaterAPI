using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    public class Rating
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey(nameof(Restaurant))] //tie it to another table
        public int RestaurantID { get; set; }
        public virtual Restaurant Restaurant { get; set; } //it returns the correct Restaurant object bc something something magic??

        [Required]
        [Range(0, 10)] //inclusive
        public double FoodScore { get; set; }

        [Required]
        [Range(0, 10)]
        public double EnvironmentScore { get; set; }

        [Required]
        [Range(0, 10)]
        public double CleanlinessScore { get; set; }

        public double AverageRating => (FoodScore + EnvironmentScore + CleanlinessScore) / 3;
    }
}