using GoogleClosure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleClosureProcessor.Models
{
    public class GoogleClosureResponse
    {
        public string CompiledCode { get; set; }
        public List<Warning> Warnings { get; set; }
        public List<ErrorModel> Errors { get; set; }
        public Statistics Statistics { get; set; }
        public string OutputFilePath { get; set; }
    }
}
