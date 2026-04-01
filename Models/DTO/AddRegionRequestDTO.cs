using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddRegionRequestDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be of Length 3")]
        [MaxLength(3, ErrorMessage = "Code has to be of Length 3")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot have more than 100 characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
