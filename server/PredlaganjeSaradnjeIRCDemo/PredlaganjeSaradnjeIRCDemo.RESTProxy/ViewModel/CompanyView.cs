using PredlaganjeSaradnjeIRC.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PredlaganjeSaradnjeIRCDemo.RESTProxy.ViewModel
{
    public class CompanyView
    {
        public int Id { get; set; }
        public Location Location { get; set; }
        public Contact Contact { get; set; }
        public string Name { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
