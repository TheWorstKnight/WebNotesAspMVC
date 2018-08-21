using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebNotes.bl.DTO;
using WebNotes.bl.Infrustructure;

namespace WebNotes.bl.Interfaces
{
    public interface INoteService:IDisposable
    {
        Task<OperationDetails> Create(NoteDTO noteDto);
        Task<OperationDetails> Edit(NoteDTO noteDto);
        Task<OperationDetails> Remove(int id);
        Task<IEnumerable<NoteDTO>> SeeAll(string userProfileId);
        Task<IEnumerable<NoteDTO>> SeeAll(string userProfileId, DateTime date);
        Task<IEnumerable<NoteDTO>> SeeAll(string userProfileId, DateTime startDate, DateTime endDate);
        Task<NoteDTO> SeeOneById(string userProfileId, int noteId);
    }
}
