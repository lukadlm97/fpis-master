using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using PredlaganjeSaradnjeIRCDemo.RESTProxy.ViewModel;
using System.Threading.Tasks;

namespace PredlaganjeSaradnjeIRCDemo.RESTProxy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        GRPCService.Authenticate.AuthenticateClient httpsAuthClient;

        public AuthController()
        {
            GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:5000");
            httpsAuthClient = new GRPCService.Authenticate.AuthenticateClient(channel);
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterRequest request)
        {
            var response = await httpsAuthClient.RegisterAsync(new GRPCService.UserCreateRequest { Username = request.Username,FirstName = request.FirstName,Password=request.Password,LastName=request.LastName });

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUser([FromBody] LogInRequest request)
        {
            var response = await httpsAuthClient.LogInAsync(new GRPCService.UserRequest { Username = request.Username,Password = request.Password });

            if (response == null)
                return NotFound();

            return Ok(response);
        }
    }
}
