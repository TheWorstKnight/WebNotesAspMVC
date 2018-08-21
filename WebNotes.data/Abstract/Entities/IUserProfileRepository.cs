using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebNotes.data.Entities;

namespace WebNotes.data.Abstract.Entities
{
    public interface IUserProfileRepository:IDisposable
    {
        void Add(UserProfile profile);
        void Remove(string profileId);
        void Edit(UserProfile profile);
        IEnumerable<UserProfile> SeeAll();
        UserProfile SeeOneById(string profileId);

    }
}
