using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostcodeParser.Test
{
    public class PostcodeTestDto
    {
        public string Postcode { get; set; }
        public string OutwardCode { get; set; }
        public string Area { get; set; }
        public string District { get; set; }
        public string InwardCode { get; set; }
        public string Sector { get; set; }
        public string Unit { get; set; }
        public bool IsValid { get; set; }
        public string Normalized { get; set; }
    }
}
