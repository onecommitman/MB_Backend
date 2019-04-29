using System;
//using System.Collections.Generic;
//using System.Linq;
using System.Threading.Tasks;
using ModulSchool.Models;

namespace ModulSchool.Services.Interfaces
{
    public interface IUserInfoService
    {
        Task<User> GetById(Guid id);

        void AppendUserPost(User user);
    }
    
}
