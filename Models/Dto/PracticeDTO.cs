using System.ComponentModel.DataAnnotations;

namespace PracticeForRestApi.Models.Dto
{
    public class PracticeDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public int Occupancy { get; set; }
        public int Sqft { get; set; }
    }
}