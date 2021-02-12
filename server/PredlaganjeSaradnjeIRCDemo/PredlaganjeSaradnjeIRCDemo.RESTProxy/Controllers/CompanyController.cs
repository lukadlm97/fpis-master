using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using PredlaganjeSaradnjeIRCDemo.GRPCService;
using PredlaganjeSaradnjeIRC.Data.Model;
using System.Threading.Tasks;
using System.Linq;
using PredlaganjeSaradnjeIRCDemo.RESTProxy.ViewModel;
using Grpc.Core;

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
        public async Task<ActionResult> GetCompanies([FromHeader] string Authorization)
        {
            var header = new Metadata();
            header.Add("Authorization", $"Bearer {Authorization}");
            var option = new CallOptions(header);

            var companies = await httpsCompanyClient.GetCompaniesAsync(new EmptyRequest(),option);

            if (companies == null)
                return NotFound();

            return Ok(companies.Companies);
        }

        [HttpGet("cities")]
        public async Task<ActionResult> GetAllCities([FromHeader] string Authorization)
        {
            var header = new Metadata();
            header.Add("Authorization", $"Bearer {Authorization}");
            var option = new CallOptions(header);

            var cities = await httpsCompanyClient.GetCitiesAsync(new EmptyRequest(),option);

            if (cities == null)
            {
                return BadRequest("Nije moguce ucitati gradove");
            }

            return Ok(cities.Cities);
        }

        [HttpGet("contacts")]
        public async Task<ActionResult> GetAllContacts([FromHeader] string Authorization)
        {
            var header = new Metadata();
            header.Add("Authorization", $"Bearer {Authorization}");
            var option = new CallOptions(header);

            var contacts = await httpsCompanyClient.GetContactAsync(new EmptyRequest(),option);

            if (contacts == null)
            {
                return NotFound("Nije moguce vratiti kontakte!");
            }

            return Ok(contacts.Contacts);
        }

        [HttpGet("locations")]
        public async Task<ActionResult> GetAllLocations([FromHeader] string Authorization)
        {
            var header = new Metadata();
            header.Add("Authorization", $"Bearer {Authorization}");
            var option = new CallOptions(header);

            var locations = await httpsCompanyClient.GetLocationsAsync(new EmptyRequest(),option);

            if (locations == null)
            {
                return NotFound("Nije moguce vratiti kontakte!");
            }

            return Ok(locations.Locations);
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateCompany([FromBody] PredlaganjeSaradnjeIRC.Data.Model.Company company, 
                                                        [FromHeader] string Authorization)
        {
            var header = new Metadata();
            header.Add("Authorization", $"Bearer {Authorization}");
            var option = new CallOptions(header);

            var enu = company.Contacts.LastOrDefault()?.ContactType??0;
            var createCompany = await httpsCompanyClient.AddCompanyAsync(new CompanyRequest
            {
                CompanyId = 0,
                Location = new LocationRequest
                {
                    Door = company.Locations.LastOrDefault()?.Door ?? 0,
                    Number = company.Locations.LastOrDefault()?.Number ?? 0,
                    Storey = company.Locations.LastOrDefault()?.Storey ?? 0,
                    StreetName = company.Locations.LastOrDefault().StreetName??"",
                    CityId = company.Locations.LastOrDefault()?.City?.Id??0
                },
                Name = company.Name,
                Password = company.Password,
                Username = company.Username,
                Contact = new ContactRequest
                {
                    ContactType = (PredlaganjeSaradnjeIRCDemo.GRPCService.ContactType)enu,
                    Content = company.Contacts.LastOrDefault()?.Content??"empty"
                }
            },option);

            if(createCompany.Status == GRPCService.StatusCode.Error)
            {
                return BadRequest();
            }
            else
            {
                var fullCompany = new CompanyView
                {
                    Id = createCompany.Company.Id,
                    Name = createCompany.Company.Name,
                    Username = createCompany.Company.Username,
                    Password = createCompany.Company.Password,
                    Contact = new PredlaganjeSaradnjeIRC.Data.Model.Contact
                    {
                        Id = createCompany.Contact.Id,
                        Content = createCompany.Contact.Content,
                        ContactType = (PredlaganjeSaradnjeIRC.Data.Model.ContactType)createCompany.Contact.ContactType
                    }

                    ,
                    Location = new PredlaganjeSaradnjeIRC.Data.Model.Location
                    {
                        Id = createCompany.Location.Id,
                        City = new PredlaganjeSaradnjeIRC.Data.Model.City 
                        { 
                            Id = createCompany.Location.City.Id,
                            Name = createCompany.Location.City.Name,
                            PostalCode = createCompany.Location.City.Postalcode,
                        },
                        Door = createCompany.Location.Door,
                        Number = createCompany.Location.Number,
                        Storey = createCompany.Location.Storey,
                        StreetName = createCompany.Location.StreetName,
                    }
                };

                return Ok(fullCompany);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCompany(int id, [FromBody] PredlaganjeSaradnjeIRC.Data.Model.Company updatedCompany,
                                                        [FromHeader] string Authorization)
        {
            var header = new Metadata();
            header.Add("Authorization", $"Bearer {Authorization}");
            var option = new CallOptions(header);

            var updated = await httpsCompanyClient.UpdateCompanyAsync(new CompanyRequest
            {
                CompanyId = id,
                Name = updatedCompany.Name,
                Password = updatedCompany.Password,
                Username = updatedCompany.Username,
                Contact = new ContactRequest
                {
                    ContactType = (PredlaganjeSaradnjeIRCDemo.GRPCService.ContactType)updatedCompany.Contacts.LastOrDefault()?.ContactType,
                    Content = updatedCompany.Contacts.LastOrDefault()?.Content
                },
                Location = new LocationRequest
                {
                    Door = updatedCompany.Locations.LastOrDefault()?.Door ?? 0,
                    Number = updatedCompany.Locations.LastOrDefault()?.Number ?? 0,
                    Storey = updatedCompany.Locations.LastOrDefault()?.Storey ?? 0,
                    StreetName = updatedCompany.Locations.LastOrDefault()?.StreetName,
                    CityId = updatedCompany.Locations.LastOrDefault()?.City?.Id??0
                },
            },option);

            if (updated.Status == GRPCService.StatusCode.Ok)
            {
                var gotValues = new CompanyView
                {
                    Id = updated.Company.Id,
                    Name = updated.Company.Name,
                    Username = updated.Company.Username,
                    Password = updated.Company.Password,
                    Contact = new PredlaganjeSaradnjeIRC.Data.Model.Contact
                    {
                        Id = updated.Contact.Id,
                        Content = updated.Contact.Content,
                        ContactType = (PredlaganjeSaradnjeIRC.Data.Model.ContactType)updated.Contact.ContactType
                    }

                    ,
                    Location = new PredlaganjeSaradnjeIRC.Data.Model.Location
                    {
                        Id = updated.Location.Id,
                        City = new PredlaganjeSaradnjeIRC.Data.Model.City
                        {
                            Id = updated.Location.City.Id,
                            Name = updated.Location.City.Name,
                            PostalCode = updated.Location.City.Postalcode,
                        },
                        Door = updated.Location.Door,
                        Number = updated.Location.Number,
                        Storey = updated.Location.Storey,
                        StreetName = updated.Location.StreetName,
                    }
                };

                if (gotValues == null)
                {
                    return BadRequest();
                }
                return Ok(gotValues);
            }
            return Forbid("Nije moguce izmeniti kompaniju!");
        }

        [HttpPost("{id}/contact")]
        public async Task<ActionResult> AddNewContact(int id, [FromBody] Contact contact, [FromHeader] string Authorization)
        {
            var header = new Metadata();
            header.Add("Authorization", $"Bearer {Authorization}");
            var option = new CallOptions(header);

            var createdContact = await httpsCompanyClient.AddContactAsync(new ContactRequest
            {
                CompanyId = id,
                ContactType = (GRPCService.ContactType)contact.ContactType,
                Content = contact.Content
            },option);

            if (createdContact.Status == GRPCService.StatusCode.Ok)
            {
                var newContact = createdContact.Contact;
                return Created("Kontakt je uspesno dodat!", newContact);
            }
            return Forbid("Nemoguce uneti novi kontakt!");
        }

        [HttpPost("{id}/location")]
        public async Task<ActionResult> AddNewLocation(int id, [FromBody] Location location, [FromHeader] string Authorization)
        {
            var header = new Metadata();
            header.Add("Authorization", $"Bearer {Authorization}");
            var option = new CallOptions(header);

            var createdLocation = await httpsCompanyClient.AddLocationAsync(new LocationRequest
            {
                CompanyId = id,
                CityId = location.City.Id,
                Door = location.Door??0,
                Number = location.Number,
                Storey = location.Storey??0,
                StreetName = location.StreetName
            },option);

            if (createdLocation.Status == GRPCService.StatusCode.Ok)
            {
                var newLocation = createdLocation.Location;
                return Created("lokacija je uspesno dodata!", newLocation);
            }
            return Forbid("Nemoguce uneti novu lokaciju!");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCompany(int id, [FromHeader] string Authorization)
        {
            var header = new Metadata();
            header.Add("Authorization", $"Bearer {Authorization}");
            var option = new CallOptions(header);

            var deleted = await httpsCompanyClient.DeleteCompanyAsync(new SearchCompanyRequest
            {
                CompanyId = id
            },option);

            if (deleted.Status == GRPCService.StatusCode.Ok)
            {
                return Ok("Kompanija je uspesno obrisana!");
            }
            return BadRequest("Kompaniju nije moguce obrisati!");
        }
    }
}
