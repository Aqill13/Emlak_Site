using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data
{
    public class Context : IdentityDbContext<UserAdmin>
    {
        public Context()
        {
        }

        public Context(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Advert> Adverts { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<GeneralSettings> GeneralSettings { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Situation> Situations { get; set; }
        public DbSet<EntityLayer.Entities.Type> Types { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Advert>()
                .HasOne(a => a.District)
                .WithMany(d => d.Adverts)
                .HasForeignKey(a => a.DistrictId)
                .OnDelete(DeleteBehavior.Restrict); // Cascade delete passiv

            modelBuilder.Entity<Advert>()
                .HasOne(a => a.City)
                .WithMany(c => c.Adverts)
                .HasForeignKey(a => a.CityId)
                .OnDelete(DeleteBehavior.Restrict); // Cascade delete'i devre dışı bırakma

            modelBuilder.Entity<Advert>()
                .HasOne(a => a.Situation)
                .WithMany(s => s.Adverts)
                .HasForeignKey(a => a.SituationId)
                .OnDelete(DeleteBehavior.Restrict); // Cascade delete'i devre dışı bırakma

            modelBuilder.Entity<Advert>()
                .HasOne(a => a.Type)
                .WithMany(t => t.Adverts)
                .HasForeignKey(a => a.TypeId)
                .OnDelete(DeleteBehavior.Restrict); // Cascade delete'i devre dışı bırakma
        }

    }
}
