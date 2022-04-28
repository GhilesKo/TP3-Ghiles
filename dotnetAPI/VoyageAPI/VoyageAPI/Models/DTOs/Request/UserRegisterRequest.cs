using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VoyageAPI.Models.DTOs.Request
{
    public class UserRegisterRequest
    {
      
        [EmailAddress]
        public String Email { get; set; }

   
        public String Password { get; set; }


    }
}
