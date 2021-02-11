using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using PredlaganjeSaradnjeIRCDemo.GRPCService;
using PredlaganjeSaradnjeIRC.Data.Model;

using System.Threading.Tasks;

namespace PredlaganjeSaradnjeIRCDemo.RESTProxy.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController: ControllerBase
    {
        GRPCService.Company.CompanyClient httpsCompanyClient;

        public CompanyController()
        {
            GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:5000");
            httpsCompanyClient = new GRPCService.Company.CompanyClient(channel);
        }

        [HttpGet]
        public async Task<ActionResult> GetCompanies()
        {
            var companies = await httpsCompanyClient.GetCompaniesAsync(new EmptyRequest());

            if (companies == null)
                return NotFound();

            return Ok(companies.Companies);
        }

        [HttpGet("cities")]
        public async Task<ActionResult> GetAllCities()
        {
            var cities = await httpsCompanyClient.GetCitiesAsync(new EmptyRequest());

            if (cities == null)
            {
                return BadRequest("Nije moguce ucitati gradove");
            }

            return Ok(cities.Cities);
        }

        [HttpGet("contacts")]
        public async Task<ActionResult> GetAllContacts()
        {
            var contacts = await httpsCompanyClient.GetContactAsync(new EmptyRequest());

            if (contacts == null)
            {
                return NotFound("Nije moguce vratiti kontakte!");
            }

            return Ok(contacts.Contacts);
        }

        [HttpGet("locations")]
        public async Task<ActionResult> GetAllLocations()
        {
            var locations = await httpsCompanyClient.GetLocationsAsync(new EmptyRequest());

            if (locations == null)
            {
                return NotFound("Nije moguce vratiti kontakte!");
            }

            return Ok(locations.Locations);
        }


    }
}
