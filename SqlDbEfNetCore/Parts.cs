using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Loader;

namespace SqlDbEfNetCore
{
    public class Parts
    {
        public int Id { get; set; }
        public string PartNumber { get; set; }
        public string Description { get; set; }
        public string Vendor { get; set; }
        public string Type { get; set; }
    }
}
