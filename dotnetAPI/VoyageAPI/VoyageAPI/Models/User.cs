using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoyageAPI.Models
{
    public class User : IdentityUser
    {

        public List<Voyage> Voyages { get; set; }


    }
}
