using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebNotes.data.Abstract.Entities;
using WebNotes.data.Entities;
using WebNotes.data.Identity;

namespace WebNotes.data.Repository
{
    public class WebNotesUnitOfWork:IUnitOfWork
    {
        private readonly NotesContext db;

        private WebNotesUserManager userManager;
        private WebNotesRoleManager roleManager;
        private IUserProfileRepository usersProfileManager;
        private INoteRepository notesManager;

        public WebNotesUnitOfWork()
        {
            db = new NotesContext();
            userManager = new WebNotesUserManager(new UserStore<User>(db));
            roleManager = new WebNotesRoleManager(new RoleStore<Role>(db));
            usersProfileManager = new UserProfileRepository(db);
            notesManager = new NoteRepository(db);
        }
        public WebNotesUserManager UserManager
        {
            get
            {
                return userManager;
            }
        }

        public WebNotesRoleManager RoleManager
        {
            get
            {
                return roleManager;
            }
        }

        public IUserProfileRepository UsersProfileManager
        {
            get
            {
                return usersProfileManager;
            }
        }

        public INoteRepository NotesManager
        {
            get
            {
               return notesManager;
            }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                    usersProfileManager.Dispose();
                    notesManager.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
