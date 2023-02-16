using System.Collections.Generic;
using TISLR2.Models;

namespace Users.Models
{
    public interface IRepository
    {
        IEnumerable<User> GetAll();
        User Get(int id);
        void Create(User user);
    }
}