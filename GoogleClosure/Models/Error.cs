using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleClosure.Models
{
    public class ErrorModel
    {
        public string Type { get; set; }
        public string File { get; set; }
        public int Lineno { get; set; }
        public int Charno { get; set; }
        public string Error { get; set; }
        public string Line { get; set; }
    }
}
