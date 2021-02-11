using PredlaganjeSaradnjeIRC.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PredlaganjeSaradnjeIRC.Data.Service
{
    public interface ICity
    {
        IEnumerable<City> GetAll();
        City GetById(int cityId); 
    }
}
