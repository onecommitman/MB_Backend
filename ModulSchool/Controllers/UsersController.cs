using System;
using System.Collections.Generic;
//using System.Linq;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModulSchool.BusinessLogic;
using ModulSchool.Models;

namespace ModulSchool.Controllers
{
    [Route("api/users")]
    //[ApiController]
    public class UsersController : ControllerBase
    {
        private readonly GetUsersInfoRequestHandler _getUsersInfoRequestHandler;
        private readonly AppendUsersRequestHandler _appendUsersRequestHandler;
        public UsersController(AppendUsersRequestHandler appendUserRequestHandler, GetUsersInfoRequestHandler getUsersInfoRequestHandler)
        {
            _appendUsersRequestHandler = appendUserRequestHandler;
            _getUsersInfoRequestHandler = getUsersInfoRequestHandler;
        }
        //public UsersController(GetUsersInfoRequestHandler getUsersInfoRequestHandler)
        //{
        //    _getUsersInfoRequestHandler = getUsersInfoRequestHandler;
        //}
        [HttpGet("{id}")]
        public Task<User>GetUserInfo(Guid id)
        {
            return _getUsersInfoRequestHandler.Handle(id);
        }

        
        // POST: api/Users
        [HttpPost("append")]
        public Task<User> AppendUser([FromBody] User user)
        {
            Guid guid = Guid.NewGuid();
            user.Id = guid;
            _appendUsersRequestHandler.AppendUserHandle(user);
            return Task.FromResult<User>(user);
        }

        //// PUT: api/Users/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
