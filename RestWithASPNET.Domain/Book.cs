using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestWithASPNET.Domain
{
    public class Book
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Author { get; set; }
        public DateTime LaunchDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public string Title { get; set; }
    }
}
