using System.ComponentModel.DataAnnotations;

namespace CityInFoAPI.Models
{
    public class PointOfInterestForCreationDto
    {
        [Required(ErrorMessage ="نام وارد نشده")]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? Description { get; set; }
    }
}
