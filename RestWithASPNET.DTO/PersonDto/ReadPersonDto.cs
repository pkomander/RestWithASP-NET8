using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestWithASPNET.DTO.PersonDto
{
    public class ReadPersonDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo FirstName é obrigatorio")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "O campo LastName é obrigatorio")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "O campo Address é obrigatorio")]
        public string Address { get; set; }
        [Required(ErrorMessage = "O campo Gender é obrigatorio")]
        public string Gender { get; set; }
    }
}
