using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoyageAPI.Models.DTOs.Request
{
    public class VoyageCreateRequest
    {
        public string Pays { get; set; }
        public bool Public { get; set; }

    }
}
