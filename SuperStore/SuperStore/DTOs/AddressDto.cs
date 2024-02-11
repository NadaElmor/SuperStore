using System.ComponentModel.DataAnnotations;

namespace SuperStore.DTOs
{
    public class AddressDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Country { get; set; }
    }
}