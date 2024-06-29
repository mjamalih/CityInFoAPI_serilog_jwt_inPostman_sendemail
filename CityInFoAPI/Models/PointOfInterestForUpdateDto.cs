using System.ComponentModel.DataAnnotations;

namespace CityInFoAPI.Models
{
    public class PointOfInterestForUpdateDto
    {
        [Required(ErrorMessage ="نام وارد نشده")]
        public string Name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? Description { get; set; }
    }
}
