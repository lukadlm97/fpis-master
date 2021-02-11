using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PredlaganjeSaradnjeIRC.Data.Model
{
    public class Location
    {
        [Required]
        public int Id { get; set; }
        public virtual City City { get; set; }
        [Required]
        public string StreetName { get; set; }
        [Required]
        public int Number { get; set; }
        public int? Storey { get; set; }
        public int? Door { get; set; }
    }
}
