using Microsoft.EntityFrameworkCore;
using PredlaganjeSaradnjeIRC.Data;
using PredlaganjeSaradnjeIRC.Data.Model;
using PredlaganjeSaradnjeIRC.Data.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PredlaganjeSaradnjeIRC.Services
{
    public class LocationService : ILocation
    {
        private readonly ApplicationContext _context;
        private readonly ICity cityService;
        public LocationService(ApplicationContext context)
        {
            _context = context;
            cityService = new CityService(context);
        }

        public bool Add(int companyId, Location newLocation)
        {
            var company = _context.Companies.Include(company => company.Locations)
                                                .FirstOrDefault(company => company.Id == companyId);

            if (company == null)
                return false;

            company.Locations = AddNewLocation(company.Locations, newLocation);

            try
            {
                _context.Update(company);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        private IEnumerable<Location> AddNewLocation(IEnumerable<Location> locations, Location newLocation)
        {
            List<Location> locationsForReturn;
            newLocation.City = cityService.GetById(newLocation.City.Id);

            if (locations.Count() == 0)
            {
                locationsForReturn = new List<Location>();
                locationsForReturn.Add(newLocation);

                return (IEnumerable<Location>)locationsForReturn;
            }

            locationsForReturn = locations.ToList();
            locationsForReturn.Add(newLocation);

            return (IEnumerable<Location>)locationsForReturn;
        }

        public Location GetByCompanyId(int companyId)
        {
            var company = _context.Companies
                                    .Include(company => company.Locations)
                                        .ThenInclude(location => location.City)
                                    .FirstOrDefault(company => company.Id == companyId);

            if (company.Locations == null)
            {
                return null;
            }

            return company.Locations
                .LastOrDefault();
        }

        public Location GetInserted()
        {
            return _context.Locations
                            .LastOrDefault();
        }
    }
}
