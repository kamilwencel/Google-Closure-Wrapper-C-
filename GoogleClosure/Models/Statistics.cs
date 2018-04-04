using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleClosureProcessor.Models
{
    public class Statistics
    {
        public int OriginalSize { get; set; }
        public int OriginalGzipSize { get; set; }
        public int CompressedSize { get; set; }
        public int CompressedGzipSize { get; set; }
        public int CompileTime { get; set; }
    }
}
