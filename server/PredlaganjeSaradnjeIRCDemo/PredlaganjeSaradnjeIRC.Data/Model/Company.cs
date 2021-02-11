using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PredlaganjeSaradnjeIRC.Data.Model
{
    public class Company
    {
        [Required]
        public int Id { get; set; }
        public IEnumerable<Location> Locations { get; set; }
        public IEnumerable<Contact> Contacts  { get; set; }
        public string Name { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
