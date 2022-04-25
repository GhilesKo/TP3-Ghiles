using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoyageAPI.Models
{
    public class Voyage
    {

        public int Id { get; set; }
        public string Pays { get; set; }

        public List<User> Users { get; set; } = new List<User>();

        public bool Public { get; set; }

        public string Photo { get; set; } = "https://media1.ledevoir.com/images_galerie/nwd_994251_803964/image.jpg";

        public int UsersCount { get; set; }

    }
}
