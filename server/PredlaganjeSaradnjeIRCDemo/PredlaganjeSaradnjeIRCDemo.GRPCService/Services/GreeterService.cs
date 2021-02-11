using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PredlaganjeSaradnjeIRCDemo.GRPCService.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;

        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

      //  [Authorize(Roles ="Admin")]
        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            _logger.LogInformation($"Sending hello to {request.Name}");
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name,
            });
        }

        public override async Task<HelloReplies> SayHelloMoreTime(HelloRequest request, ServerCallContext context)
        {
            var replies = GetReplies();

            return new HelloReplies
            {
                Replies = { replies }
            };
        }

        private static List<HelloReply> GetReplies()
        {
            List<HelloReply> replies = new List<HelloReply>();

            replies.Add(new HelloReply { Message = "Hi first time!" });
            replies.Add(new HelloReply { Message = "Hi secound time!" });
            replies.Add(new HelloReply { Message = "Hi third time!" });

            return replies;
        }
    }
}