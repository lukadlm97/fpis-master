using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PredlaganjeSaradnjeIRCDemo.RESTProxy.ViewModel
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
