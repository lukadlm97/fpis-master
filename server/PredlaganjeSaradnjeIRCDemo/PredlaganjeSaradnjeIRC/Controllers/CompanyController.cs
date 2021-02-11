using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PredlaganjeSaradnjeIRC.Data.Model;
using PredlaganjeSaradnjeIRC.Data.Service;

namespace PredlaganjeSaradnjeIRC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompany _companyService;
        private readonly IContact _contactService;
        private readonly ILocation _locationService;
        private readonly ICity _cityService;

        public CompanyController(ICompany companyService,IContact contactService,ILocation locationService,ICity cityService)
        {
            this._companyService = companyService;
            _contactService = contactService;
            _locationService = locationService;
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<ActionResult<Company>> GetCompanies()
        {
            var companies = _companyService.GetAll();

            if (companies == null)
                return NotFound();

            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var company = _companyService.GetById(id);

            if (company == null)
                return NotFound();

            return Ok(company);
        }
        
        [HttpPost]
        public async Task<ActionResult<Company>> AddNewCompany([FromBody] Company newCompany)
        {
            if (_companyService.Add(newCompany))
            {
                var company = _companyService.GetInserted();
                // TODO: da se vraca kompanija koja je dodat kao objekat
                return Created("",company);
            }
            return Forbid("Nemoguce uneti novu kompaniju!");
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCompany(int id,[FromBody] Company updatedCompany)
        {
            if (_companyService.Update(id,updatedCompany))
            {
                var updated = _companyService.GetById(id);
                if(updated == null)
                {
                    return BadRequest();
                }
                return Ok(updated);
            }
            return Forbid("Nije moguce izmeniti kompaniju!");
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCompany(int id)
        {
            if (_companyService.Delete(id))
            {
                return Ok("Kompanija je uspesno obrisana!");
            }
            return BadRequest("Kompaniju nije moguce obrisati!");
        }
        
        [HttpGet("contacts")]
        public async Task<ActionResult<Contact>> GetAllContacts()
        {
            var contacts = _contactService.GetAll();

            if(contacts == null)
            {
                return NotFound("Nije moguce vratiti kontakte!");
            }

            return Ok(contacts);
        }

        [HttpGet("{id}/contact")]
        public async Task<ActionResult<Contact>> GetContactsForCompany(int id)
        {
            var contacts = _contactService.GetAll(id);

            if (contacts == null)
            {
                return BadRequest("Nije moguce pronaci kontakte za zadatu kompaniju!");
            }
            return Ok(contacts);
        }
        
        [HttpPost("{id}/contact")]
        public async Task<ActionResult<Contact>> AddNewContact(int id,[FromBody] Contact contact)
        {
            if (_companyService.AddNewContact(id,contact))
            {
                var company = _companyService.GetById(id);
                return Created("Kontakt je uspesno dodat!", company);
            }
            return Forbid("Nemoguce uneti novi kontakt!");
        }
       
        [HttpDelete("{idCompany}/contact/{idContact}")]
        public async Task<ActionResult<Contact>> RemoveContact(int idCompany,int idContact)
        {
            if (_contactService.Delete(idCompany, idContact))
            {
                return Ok("Kontakt je uspesno obrisana!");
            }
            return BadRequest("Kontakt je uspesno obrisana!");
        }
    
        [HttpPut("{idCompany}/contact/{idContact}")]
        public async Task<ActionResult<Contact>> UpdateContact(int idCompany,int idContact,[FromBody] Contact updatedContact)
        {
            if (_contactService.Update(idCompany, idContact, updatedContact))
            {
                return Ok("Kontakt je uspesno izmenjen!");
            }
            return BadRequest("Nije moguce izmeniti kontakt!");
        }

        [HttpGet("{id}/location")]
        public async Task<ActionResult<Location>> GetLocationForCompany(int id)
        {
            var location = _locationService.GetByCompanyId(id);

            if (location == null)
            {
                return NotFound("Nije nadjena lokacija za kompaniju");
            }

            return Ok(location);
        }

        [HttpPost("{id}/location")]
        public async Task<ActionResult<Location>> AddNewLocation(int id,[FromBody] Location newLocation)
        {
            if (_locationService.Add(id, newLocation))
            {
                var company = _companyService.GetById(id);
                return Created("Uspesno ste dodali lokaciju za kompaniju",company);
            }
            return BadRequest("Nije moguce uneti lokaciju za kompaniju");
        }

        [HttpGet("cities")]
        public async Task<ActionResult<City>> GetAllCities()
        {
            var cities = _cityService.GetAll();

            if(cities == null)
            {
                return BadRequest("Nije moguce ucitati gradove");
            }

            return Ok(cities);
        }

    }
}