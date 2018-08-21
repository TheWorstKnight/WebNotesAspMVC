using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebNotes.bl.DTO
{
    public class UserDTO
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [MaxLength(15)]
        public string Login { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(30)]
        public string Surname { get; set; }

        [MaxLength(30)]
        public string City { get; set; }

        [Required]
        [MaxLength(13)]
        public string Phone { get; set; }

        public string Role { get; set; }
        public List<NoteDTO> Notes { get; set; }
    }
}
