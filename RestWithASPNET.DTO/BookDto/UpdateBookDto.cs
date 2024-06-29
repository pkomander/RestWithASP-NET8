using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestWithASPNET.DTO.BookDto
{
    public class UpdateBookDto
    {
        [Required(ErrorMessage = "O campo Author é obrigatorio")]
        public string Author { get; set; }
        [Required(ErrorMessage = "O campo LaunchDate é obrigatorio")]
        public DateTime LaunchDate { get; set; }
        [Required(ErrorMessage = "O campo Price é obrigatorio")]
        [Range(0, 500, ErrorMessage = "O Price deve estar entre 0 e 500.")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "O campo Title é obrigatorio")]
        public string Title { get; set; }
    }
}
