using System.ComponentModel.DataAnnotations;

namespace CustomerAPI.Dtos
{
    public class CreateCustomerDtos
    {

        [Required(ErrorMessage = "Debes especificar el nombre por favor")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Debes especificar el apellido por favor")]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
