using PredlaganjeSaradnjeIRC.Data;
using PredlaganjeSaradnjeIRC.Data.Model;
using PredlaganjeSaradnjeIRC.Data.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PredlaganjeSaradnjeIRC.Services
{
    public class CityService : ICity
    {
        private readonly ApplicationContext _context;
        public CityService(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<City> GetAll()
        {
            return _context.Cities;
        }

        public City GetById(int cityId)
        {
            return GetAll()
                        .FirstOrDefault(city => city.Id == cityId);
        }
    }
}
