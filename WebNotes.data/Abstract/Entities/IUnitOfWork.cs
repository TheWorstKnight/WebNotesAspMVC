using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebNotes.data.Identity;

namespace WebNotes.data.Abstract.Entities
{
    public interface IUnitOfWork:IDisposable
    {
        WebNotesUserManager UserManager { get; }
        WebNotesRoleManager RoleManager { get; }
        IUserProfileRepository UsersProfileManager { get; }
        INoteRepository NotesManager { get; }
        Task SaveAsync();
    }
}
