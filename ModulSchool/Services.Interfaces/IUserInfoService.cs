using System;
//using System.Collections.Generic;
//using System.Linq;
using System.Threading.Tasks;
using ModulSchool.Models;
using Microsoft.AspNetCore.Mvc;

namespace ModulSchool.Services.Interfaces
{
    public interface IUserInfoService
    {
        Task<User> GetById(Guid id);

        Task<IActionResult> AppendUserPost(User user);
    }
    
}
