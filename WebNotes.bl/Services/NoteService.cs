using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebNotes.bl.DTO;
using WebNotes.bl.Infrustructure;
using WebNotes.bl.Interfaces;
using WebNotes.data.Abstract.Entities;
using WebNotes.data.Entities;
using Microsoft.AspNet.Identity;

namespace WebNotes.bl.Services
{
    public class NoteService : INoteService
    {
        private readonly IUnitOfWork db;
        public NoteService(IUnitOfWork db)
        {
            this.db = db;
        }
        public async Task<OperationDetails> Create(NoteDTO noteDto)
        {
            User user = await db.UserManager.FindByIdAsync(noteDto.UserDTOId);
            if (user != null)
            {
                Note note = new Note
                {
                    Name = noteDto.Name,
                    Date = noteDto.Date,
                    Text = noteDto.Text,
                    UserProfileId = noteDto.UserDTOId
                };

                try
                {
                    db.NotesManager.Add(note);
                    await db.SaveAsync();
                    return new OperationDetails(true, "Запись была успешно создана", "");
                }
                catch (Exception ex)
                {
                    return new OperationDetails(false, ex.Message, "");
                }    
            }
            else return new OperationDetails(false, "Такого пользователя не существует", "Id");
        }

        public async Task<OperationDetails> Edit(NoteDTO noteDto)
        {
            var exists = await db.UserManager.FindByIdAsync(noteDto.UserDTOId);
            if (exists != null)
            {
                Note note = new Note
                {
                    Name = noteDto.Name,
                    Date = noteDto.Date,
                    Text = noteDto.Text,
                    UserProfileId = noteDto.UserDTOId
                };

                try
                {
                    db.NotesManager.Edit(note);
                    await db.SaveAsync();
                    return new OperationDetails(true, "Запись была успешно отредактирована", "");
                }
                catch (Exception ex)
                {
                    return new OperationDetails(false, ex.Message, "");
                }
            }
            else return new OperationDetails(false, "Такого пользователя не существует", "Id");
        }

        public async Task<OperationDetails> Remove(int id)
        {
                try
                {
                    db.NotesManager.Remove(id);
                    await db.SaveAsync();
                    return new OperationDetails(true, "Запись была успешно удалена", "");
                }
                catch (Exception ex)
                {
                    return new OperationDetails(false, ex.Message, "");
                }

        }

        public async Task<IEnumerable<NoteDTO>> SeeAll(string UserDTOId)
        {
            var exists = await db.UserManager.FindByIdAsync(UserDTOId);
            if (exists != null)
            {
                List<NoteDTO> resultList = new List<NoteDTO>();
                List<Note> notes = db.NotesManager.SeeAll(UserDTOId).ToList();
                if (notes != null && notes.Count > 0)
                {
                    foreach (var note in notes)
                    {
                        resultList.Add(new NoteDTO
                        {
                            Id = note.NoteId,
                            UserDTOId = note.UserProfileId,
                            Name = note.Name,
                            Date = note.Date,
                            Text = note.Text
                        });
                    }
                    return resultList;
                }
                else return null;
            }
            else return null;
        }

        public async Task<IEnumerable<NoteDTO>> SeeAll(string UserDTOId, DateTime date)
        {
            var exists = await db.UserManager.FindByIdAsync(UserDTOId);
            if (exists != null)
            {
                List<NoteDTO> resultList = new List<NoteDTO>();
                List<Note> notes = db.NotesManager.SeeAll(UserDTOId,date).ToList();
                if (notes != null && notes.Count > 0)
                {
                    foreach (var note in notes)
                    {
                        resultList.Add(new NoteDTO
                        {
                            Id = note.NoteId,
                            UserDTOId = note.UserProfileId,
                            Name = note.Name,
                            Date = note.Date,
                            Text = note.Text
                        });
                    }
                    return resultList;
                }
                else return null;
            }
            else return null;
        }

        public async Task<IEnumerable<NoteDTO>> SeeAll(string UserDTOId, DateTime startDate, DateTime endDate)
        {
            var exists = await db.UserManager.FindByIdAsync(UserDTOId);
            if (exists != null)
            {
                List<NoteDTO> resultList = new List<NoteDTO>();
                List<Note> notes = db.NotesManager.SeeAll(UserDTOId, startDate, endDate).ToList();
                if (notes != null && notes.Count > 0)
                {
                    foreach (var note in notes)
                    {
                        resultList.Add(new NoteDTO
                        {
                            Id = note.NoteId,
                            UserDTOId = note.UserProfileId,
                            Name = note.Name,
                            Date = note.Date,
                            Text = note.Text
                        });
                    }
                    return resultList;
                }
                else return null;
            }
            else return null;
        }

        public async Task<NoteDTO> SeeOneById(string UserDTOId, int noteId)
        {
            var exists = await db.UserManager.FindByIdAsync(UserDTOId);
            if (exists != null)
            {
                Note note = db.NotesManager.SeeOneById(UserDTOId, noteId);
                if (note != null)
                {
                    return new NoteDTO
                    {
                        Id = note.NoteId,
                        UserDTOId = note.UserProfileId,
                        Name = note.Name,
                        Date = note.Date,
                        Text = note.Text
                    };
                }
                else return null;
            }
            else return null;

        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
