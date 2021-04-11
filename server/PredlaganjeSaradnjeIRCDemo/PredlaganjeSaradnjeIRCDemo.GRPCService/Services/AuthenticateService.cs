using Grpc.Core;
using Microsoft.Extensions.Logging;
using PredlaganjeSaradnjeIRC.Data.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PredlaganjeSaradnjeIRCDemo.GRPCService.Services
{
    public class AuthenticateService: Authenticate.AuthenticateBase
    {
        private readonly IUser _userService;
        private readonly ILogger<AuthenticateService> _logger;

        public AuthenticateService(IUser userService,ILogger<AuthenticateService>  logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async override Task<UserResponse> LogIn(UserRequest request, ServerCallContext context)
        {
            var token = await  _userService.LogIn(request.Username, request.Password);
            var status = token == null?StatusCode.Error: StatusCode.Ok;

            if (status == StatusCode.Ok)
                _logger.LogInformation("Successfully logedin");
            else
                _logger.LogError("Problem with login");

            return new UserResponse
            {
                Status = status,
                Token = token??""
            };
        }

        public async override Task<UserResponse> Register(UserCreateRequest request, ServerCallContext context)
        {
            var isRegistred = await _userService.Register(request.Username, request.Password, 
                                                            request.FirstName, request.LastName);

            return new UserResponse
            {
                Status = isRegistred ? StatusCode.Ok : StatusCode.Error
            };
        }
    }
}
