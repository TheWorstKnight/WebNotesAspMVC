using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebNotes.data.Entities
{
    public class UserProfile
    {
        [Key]
        [ForeignKey("User")]
        public string Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Surname { get; set; }

        [MaxLength(30)]
        public string City { get; set; }
        public virtual IEnumerable<Note> Notes { get; set; }

        public virtual User User { get; set; }

        public UserProfile()
        {
            Notes = new List<Note>();
        }
    }
}
