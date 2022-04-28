
    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VoyageAPI.Models;

namespace VoyageAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Voyage> Voyages { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var hasher = new PasswordHasher<User>();
            var user1Id = Guid.NewGuid().ToString();
            var user2Id = Guid.NewGuid().ToString();

           User u1 = new User
            {
                Id = user1Id,
                UserName = "jim@test.com",
                Email = "jim@test.com",
                // La comparaison d'identity se fait avec les versions normalisés
                NormalizedEmail = "JIM@TEST.COM",
                NormalizedUserName = "JIM@TEST.COM",
            
            };
            u1.PasswordHash = hasher.HashPassword(u1, "Passw0rd!");
            builder.Entity<User>().HasData(u1);


            User u2 = new User
            {
                Id = user2Id,
                UserName = "tom@test.com",
                Email = "tom@test.com",
                // La comparaison d'identity se fait avec les versions normalisés
                NormalizedEmail = "TOM@TEST.COM",
                NormalizedUserName = "TOM@TEST.COM",

            };
            u2.PasswordHash = hasher.HashPassword(u2, "Passw0rd!");
            builder.Entity<User>().HasData(u2);


            builder.Entity<Voyage>().HasData(new Voyage
            {
                Id = 1,
                Pays = "Amerique",
                Photo = "https://media1.ledevoir.com/images_galerie/nwd_994251_803964/image.jpg",
                Public = true
            });

            builder.Entity<Voyage>().HasData(new Voyage
            {
                Id = 2,
                Pays = "Brazil",
                Photo = "https://media1.ledevoir.com/images_galerie/nwd_994251_803964/image.jpg",
                Public = false
            });

            builder.Entity<Voyage>().HasData(new Voyage
            {
                Id = 3,
                Pays = "Chine",
                Photo = "https://media1.ledevoir.com/images_galerie/nwd_994251_803964/image.jpg",
                Public = false
            });

            builder.Entity<Voyage>().HasData(new Voyage
            {
                Id = 4,
                Pays = "Algerie",
                Photo = "https://media1.ledevoir.com/images_galerie/nwd_994251_803964/image.jpg",
                Public = true
            });


            builder.Entity<Voyage>()
    .HasMany(u => u.Users)
    .WithMany(v => v.Voyages)
    .UsingEntity(r => {
        r.HasData(new { UsersId = user1Id, VoyagesId = 1 });
        r.HasData(new { UsersId = user1Id, VoyagesId = 2 });
        r.HasData(new { UsersId = user1Id, VoyagesId = 3 });
        r.HasData(new { UsersId = user1Id, VoyagesId = 4 });
        r.HasData(new { UsersId = user2Id, VoyagesId = 3 });
        r.HasData(new { UsersId = user2Id, VoyagesId = 4 });



    });


        }
    }
}
