using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebNotes.data.Abstract.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebNotes.data.Infrustructure;

namespace WebNotes.data.Entities
{
    public class Note
    {
        [Key]
       public int NoteId { get; set; }

        [Required,MaxLength(30)]
       public string Name { get; set; }

        [MaxLength(255)]
       public string Text { get; set; }

        [Required]
        [DateRestrict]
       public DateTime Date { get; set; }

        [ForeignKey("UserProfile")]
       public string UserProfileId { get; set; }

       public virtual UserProfile UserProfile { get; set; }


    }
}
