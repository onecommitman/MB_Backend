using System;
using System.Threading.Tasks;
using ModulSchool.Models;
using ModulSchool.Services.Interfaces;
using MassTransit;
using ModulSchool.Commands;

namespace ModulSchool.BusinessLogic
{
    public class AppendUsersRequestHandler
    {
        private readonly IBus _bus;

        public AppendUsersRequestHandler(IBus bus)
        {
            _bus = bus;
        }

        public Task<User> Handle(User user)
        {
            Guid guid = Guid.NewGuid();
            user.Id = guid;

            // было так: _userInfoService.AppendUser(user);
            _bus.Send(new AppendUserCommand()
            {
                User = user
            });

            return Task.FromResult<User>(user);
        }
    }
}
