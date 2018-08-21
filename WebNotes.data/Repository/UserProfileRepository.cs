using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebNotes.data.Abstract.Entities;
using WebNotes.data.Entities;
using System.Data.Entity;

namespace WebNotes.data.Repository
{
    public class UserProfileRepository : IUserProfileRepository
    {
        public NotesContext db;
        public UserProfileRepository(NotesContext db)
        {
            this.db = db;
        }
        public void Add(UserProfile user)
        {
            try
            {
                db.UserProfiles.Add(user);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public void Edit(UserProfile user)
        {
            try
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Remove(string id)
        {
            UserProfile u = db.UserProfiles.Find(id);

                try
                {
                    db.UserProfiles.Remove(u);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }         
        }

        public IEnumerable<UserProfile> SeeAll()
        {
            var users = db.UserProfiles.Include(u => u.Notes);
            return users;
        }

        public UserProfile SeeOneById(string id)
        {
            var user = db.UserProfiles.Include(u => u.Notes).FirstOrDefault(u => u.Id == id);
            return user;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
