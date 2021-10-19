using Microsoft.EntityFrameworkCore;
using MyContacts.Models;
using System;

namespace MyContacts.DatabaseLayer
{
    public class Context : DbContext
    {
        public Context(DbContextOptions opts) : base(opts) { }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Call> Calls { get; set; }

        public DbSet<Conferention> Conferentions { get; set; }

        public DbSet<Phone> PhoneNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Creating phone numbers
            modelBuilder.Entity<Phone>().HasData(new Phone
            {
                Id = new Guid("e7aaa2b2-207d-4626-a74d-a8af8d41b239"),
                PhoneNumber = "88005553535"
            },
            new Phone
            {
                Id = new Guid("6d8dae9f-de21-4dda-a952-dcc0103351d1"),
                PhoneNumber = "89054122233"
            });

            modelBuilder.Entity<Contact>().HasData(new Contact
            {
                Id = new Guid("53503136-b159-4538-95b4-68cecace6828"),
                Name = "Ivan",
                MiddleName = "Ivanovich",
                LastName = "Ivanov",
                PhoneNumberId = new Guid("e7aaa2b2-207d-4626-a74d-a8af8d41b239")

            });

        }
    }
}
