using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PredlaganjeSaradnjeIRC.Data.Model
{
    public class RequestForCooperation
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string DescriptionOfProposal { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public Company Company { get; set; }
        public Employee Employee { get; set; }
    }
}
