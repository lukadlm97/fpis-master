using Grpc.Core;
using Grpc.Net.Client;
using PredlaganjeSaradnjeIRCDemo.GRPCService;
using System;
using System.Threading.Tasks;

namespace PredlaganjeSaradnjeIRCDemo.ClientConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string address = "http://localhost:5000";
            GrpcChannel channel = GrpcChannel.ForAddress(address);

            var httpsClient = new Greeter.GreeterClient(channel);
            var httpscli = new Authenticate.AuthenticateClient(channel);

            /* var response = await httpscli.LogInAsync(new UserRequest
             {
                 Username = "pera",
                 Password = "pess"
             });

             var token = response.Token;
             var header = new Metadata();
             header.Add("Authorization", $"Bearer {token}");
             var options = new CallOptions(header);

             var reply = await httpsClient.SayHelloAsync(new HelloRequest
             {
                 Name = "luka"
             },options);

             Console.WriteLine(reply.Message);*/

            var response = await httpsClient.SayHelloAsync(new HelloRequest
            {
                Name = "Luka"
            });

            Console.WriteLine(response.ToString());

            Console.WriteLine(httpsClient.ToString());

            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
