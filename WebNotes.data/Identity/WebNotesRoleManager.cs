using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebNotes.data.Entities;

namespace WebNotes.data.Identity
{
    public class WebNotesRoleManager : RoleManager<Role>
    {
        public WebNotesRoleManager(IRoleStore<Role, string> store) : base(store)
        {
        }
    }
}
