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
    public interface IUserService:IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
        Task<OperationDetails> ChangePassword(UserDTO userDto);
        Task<OperationDetails> Edit(UserDTO userDto);
        Task<OperationDetails> Remove(string id);
        Task<IEnumerable<UserDTO>> GetAll();
        Task<UserDTO> GetOne(string id);
    }
}
