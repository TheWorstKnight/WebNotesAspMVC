using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebNotes.data.Entities;

namespace WebNotes.data.Abstract.Entities
{
    public interface INoteRepository:IDisposable
    {
        void Add(Note note);
        void Remove(int id);
        void Edit(Note note);
        IEnumerable<Note> SeeAll(string userProfileId);
        IEnumerable<Note> SeeAll(string userProfileId, DateTime date);
        IEnumerable<Note> SeeAll(string userProfileId, DateTime startDate, DateTime endDate);
        Note SeeOneById(string userProfileId, int noteId);

    }
}
