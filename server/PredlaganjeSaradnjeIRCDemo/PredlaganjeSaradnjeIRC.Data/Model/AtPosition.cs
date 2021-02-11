using System;
using System.Collections.Generic;
using System.Text;

namespace PredlaganjeSaradnjeIRC.Data.Model
{
    public class AtPosition
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public Position Position { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
