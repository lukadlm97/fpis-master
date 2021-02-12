using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PredlaganjeSaradnjeIRC.Data.Model
{
    public class City
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }
    }
}
