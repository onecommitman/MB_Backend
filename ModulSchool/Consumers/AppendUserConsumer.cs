using System;
using System.Threading.Tasks;
using ModulSchool.Commands;
using ModulSchool.Services.Interfaces;
using MassTransit;

namespace ModulSchool.Consumers
{
    public class AppendUserConsumer : IConsumer<AppendUserCommand>
    {
        private readonly IUserInfoService _userInfoService;

        public AppendUserConsumer(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        public async Task Consume(ConsumeContext<AppendUserCommand> context)
        {
            await _userInfoService.AppendUserPost(context.Message.User);
        }
    }
}
