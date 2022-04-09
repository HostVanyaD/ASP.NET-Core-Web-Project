namespace HighPaw.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SizeCategory
    {
        public int Id { get; init; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public IEnumerable<Pet> Pets { get; init; } = new List<Pet>();
    }
}
