using AutoMapper;
using Google.Protobuf.Collections;
using Grpc.Core;
using PredlaganjeSaradnjeIRC.Data.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PredlaganjeSaradnjeIRCDemo.GRPCService.Services
{
    public class CompanyService : Company.CompanyBase
    {
        private readonly ICompany _companyService;
        private readonly IContact _contactService;
        private readonly ILocation _locationService;
        private readonly ICity _cityService;
        private readonly IMapper _mapper;
        private readonly IEmployee _employeeService;

        public CompanyService(ICompany companyService, IContact contactService, ILocation locationService, 
                                ICity cityService, IMapper mapper,IEmployee employeeService )
        {
            _companyService = companyService;
            _contactService = contactService;
            _locationService = locationService;
            _cityService = cityService;
            _mapper = mapper;
            _employeeService = employeeService;
        }

        public override async Task<CompaniesResponse> GetCompanies(EmptyRequest request, ServerCallContext context)
        {
            var companies = _companyService.GetAll();

            if (companies == null)
                return null;

            var convertedCompanies = companies.Select(x => _mapper.Map<CompanyResponse>(x));

            return new CompaniesResponse
            {
                Companies = { convertedCompanies }
            };
        }

        public override async Task<EmployeesResponse> GetEmployees(EmptyRequest request, ServerCallContext context)
        {
            var employees = _employeeService.GetAll();
            if (employees == null)
            {
                return null;
            }

            var convertedEmplyees = employees.Select(x => _mapper.Map<EmployeeResponse>(x));

            return new EmployeesResponse { 
                Employees = { convertedEmplyees }
            };
        }

        public override async Task<CitiesResponse> GetCities(EmptyRequest request, ServerCallContext context)
        {
            var cities = _cityService.GetAll();

            if (cities == null)
            {
                return null;
            }

            var convertedCities = cities.Select(x => _mapper.Map<CityResponse>(x));

            return new CitiesResponse
            {
                Cities = { convertedCities }
            };
        }

        public override async Task<ContactsResponse> GetContact(EmptyRequest request, ServerCallContext context)
        {
            var companies = _companyService.GetAll();

            if (companies == null)
                return null;


            List<ContactResponse> contacts = new List<ContactResponse>();
            foreach(var comp in companies)
            {
                foreach(var cont in comp.Contacts)
                {
                    ContactResponse response = _mapper.Map<ContactResponse>(cont);
                    response.CompanyId = comp.Id;
                    contacts.Add(response);
                }
            }


            return new ContactsResponse
            {
                Contacts = { contacts }
            };

        }

        public override async Task<LocationsResponse> GetLocations(EmptyRequest request, ServerCallContext context)
        {
            var companies = _companyService.GetAll();

            if (companies == null)
                return null;


            List<LocationResponse> locations = new List<LocationResponse>();
            foreach (var comp in companies)
            {
                foreach (var cont in comp.Locations)
                {
                    LocationResponse response = _mapper.Map<LocationResponse>(cont);
                    response.CompanyId = comp.Id;
                    locations.Add(response);
                }
            }


            return new LocationsResponse
            {
                Locations = { locations }
            };
        }

        public override async Task<UpsertCompanyResponse> AddCompany(CompanyRequest request, ServerCallContext context)
        {
            PredlaganjeSaradnjeIRC.Data.Model.Company company = 
                _mapper.Map<PredlaganjeSaradnjeIRC.Data.Model.Company>(request);

            PredlaganjeSaradnjeIRC.Data.Model.Location location = 
                _mapper.Map<PredlaganjeSaradnjeIRC.Data.Model.Location>(request.Location);

            company.Locations = new List<PredlaganjeSaradnjeIRC.Data.Model.Location> { location };
            company.Contacts = new List<PredlaganjeSaradnjeIRC.Data.Model.Contact>();

            if (_companyService.Add(company))
            {
                var createdCompany =  _companyService.GetInserted();

                return new UpsertCompanyResponse
                {
                    Status = StatusCode.Ok,
                    Message = "Kompanija je kreirana",
                    Company = _mapper.Map<CompanyResponse>(createdCompany)
                };
            }
            return new UpsertCompanyResponse
            {
                Status = StatusCode.Error,
                Message = "Nije moguce kreirati kompaniju"
            };
        }

        public override async Task<UpsertContactResponse> AddContact(ContactRequest request, ServerCallContext context)
        {
            var contact = _mapper.Map<PredlaganjeSaradnjeIRC.Data.Model.Contact>(request);

            if (_companyService.AddNewContact(request.CompanyId, contact))
            {
                var company = _companyService.GetById(request.CompanyId);
                return new UpsertContactResponse
                {
                    Status = StatusCode.Ok,
                    Message = "Kontakt je uspesno dodat",
                    Contact = _mapper.Map<ContactResponse>(company.Contacts.LastOrDefault())
                };
            }

            return new UpsertContactResponse
            {
                Message = "Nije moguce dodati kontakt",
                Status = StatusCode.Error
            };
        }

        public override async Task<UpsertLocationResponse> AddLocation(LocationRequest request, ServerCallContext context)
        {
            var location = _mapper.Map<PredlaganjeSaradnjeIRC.Data.Model.Location>(request);

            if (_locationService.Add(request.CompanyId, location))
            {
                var company = _companyService.GetById(request.CompanyId);
                return new UpsertLocationResponse
                {
                    Location = _mapper.Map<LocationResponse>(company.Locations.LastOrDefault()),
                    Message = "Lokacije je uspesno promenjena",
                    Status = StatusCode.Ok
                };
            }

            return new UpsertLocationResponse
            {
                Message = "Lokacije nije uspesno promenjena",
                Status = StatusCode.Error
            };
        }

        public override async Task<UpsertCompanyResponse> DeleteCompany(SearchCompanyRequest request, ServerCallContext context)
        {
            if (_companyService.Delete(request.CompanyId))
            {
                return new UpsertCompanyResponse
                {
                    Status = StatusCode.Ok,
                    Message = "kompanija je uspesno obrisana"
                };
            }
            return new UpsertCompanyResponse
            {
                Status = StatusCode.Error,
                Message="kompaniju nije moguce obrisati"
            };
        }

        public override async Task<UpsertCompanyResponse> UpdateCompany(CompanyRequest request, ServerCallContext context)
        {
            var forUpdate = _mapper.Map<PredlaganjeSaradnjeIRC.Data.Model.Company>(request);

            if (_companyService.Update(request.CompanyId, forUpdate))
            {
                var updated = _companyService.GetById(request.CompanyId);
                if (updated == null)
                {
                    return new UpsertCompanyResponse
                    {
                        Message = "Nije moguce izmeniti kompaniju",
                        Status = StatusCode.Error
                    };
                }
                return new UpsertCompanyResponse
                {
                    Status = StatusCode.Ok,
                    Message = "kompanija je uspesno izmenjena",
                    Company = _mapper.Map<CompanyResponse>(updated)
                };
            }
            return new UpsertCompanyResponse
            {
                Message = "Nije moguce izmeniti kompaniju",
                Status = StatusCode.Error
            };
        }

    }
}