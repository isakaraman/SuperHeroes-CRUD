using System.ComponentModel.DataAnnotations;

namespace SuperHeroAPI.Entities
{
    public class SuperHero
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        [Required]
        public string FirstName { get; set; }=string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Place { get; set; } = string.Empty;
    }
}
