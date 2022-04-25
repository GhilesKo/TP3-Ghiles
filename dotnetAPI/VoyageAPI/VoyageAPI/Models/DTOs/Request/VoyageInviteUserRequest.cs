using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoyageAPI.Models.DTOs.Request
{
    public class VoyageInviteUserRequest
    {

        public int VoyageId { get; set; }

        public string UserEmail{ get; set; }
    }
}
