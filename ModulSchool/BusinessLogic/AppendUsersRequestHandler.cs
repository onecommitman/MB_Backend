using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModulSchool.Models;
using ModulSchool.Services.Interfaces;

namespace ModulSchool.BusinessLogic
{
    public class AppendUsersRequestHandler
    {
        public AppendUsersRequestHandler(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }
        private IUserInfoService _userInfoService;
        public void AppendUserHandle(User user)
        {
            //Guid guid = Guid.NewGuid();
            //user.Id = guid;
            _userInfoService.AppendUserPost(user);
            //return Task.FromResult<User>(user);
        }
    }
}
