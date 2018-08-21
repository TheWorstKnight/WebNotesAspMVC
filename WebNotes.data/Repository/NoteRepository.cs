using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebNotes.data.Abstract.Entities;
using WebNotes.data.Entities;

namespace WebNotes.data.Repository
{
    public class NoteRepository : INoteRepository
    {
        public NotesContext db;
        public NoteRepository(NotesContext db)
        {
            this.db = db;
        } 
        public void Add(Note note)
        {
            try
            {
                db.Notes.Add(note);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Edit(Note note)
        {
            try
            {
                db.Entry(note).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Remove(int id)
        {
            Note n = db.Notes.Find(id);

                try
                {
                    db.Notes.Remove(n);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

        }

        public IEnumerable<Note> SeeAll(string userProfileId)
        {
            var result = from n in db.Notes
                         where n.UserProfileId == userProfileId
                         select n;
            return result.ToList().Count > 0 ? result.ToList() : null;
        }

        public IEnumerable<Note> SeeAll(string userProfileId,DateTime date)
        {
            var result = from n in db.Notes
                         where n.UserProfileId == userProfileId && n.Date==date
                         select n;
            return result.ToList().Count > 0 ? result.ToList() : null;
        }

        public IEnumerable<Note> SeeAll(string userProfileId,DateTime startDate, DateTime endDate)
        {
            var result = from n in db.Notes
                         where n.UserProfileId == userProfileId && n.Date >= startDate && n.Date <= endDate
                         select n;
            return result.ToList().Count > 0 ? result.ToList() : null;
        }

        public Note SeeOneById(string userProfileId,int noteId)
        {
            foreach (var n in db.Notes)
            {
                if (n.UserProfileId == userProfileId && n.NoteId==noteId)
                    return n;
            }

            return null;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
