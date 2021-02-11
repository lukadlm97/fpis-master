using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PredlaganjeSaradnjeIRC.Data.Model
{
    public class Contact
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public ContactType ContactType { get; set; }
    }
}
