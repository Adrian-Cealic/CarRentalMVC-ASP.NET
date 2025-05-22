using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Make { get; set; }

        [Required]
        [StringLength(100)]
        public string Model { get; set; }

        [Required]
        [Range(1900, 2100)] // Assuming a reasonable year range
        public int Year { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0.01, 10000.00)] // Example price range
        public decimal PricePerDay { get; set; }

        [StringLength(500)]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; } // Optional, can be a URL to the car image

        public bool IsAvailable { get; set; } = true; // Default to available
        
        [Required]
        [Display(Name = "Transmission")]
        public string Transmission { get; set; } = "Automatic"; // Default to Automatic
    }
} 