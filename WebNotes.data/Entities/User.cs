using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebNotes.data.Abstract.Entities;

namespace WebNotes.data.Entities
{
    public class User:IdentityUser
    {
        public virtual UserProfile UserProfile { get; set; }
    }
}
