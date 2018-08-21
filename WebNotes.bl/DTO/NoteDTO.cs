using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebNotes.bl.Infrustructure;

namespace WebNotes.bl.DTO
{
    public class NoteDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Text { get; set; }

        [Required]
        [DateRestrict]
        public DateTime Date { get; set; }

        [ForeignKey("UserDTO")]
        public string UserDTOId { get; set; }

        public UserDTO UserDTO { get; set; }
    }
}
