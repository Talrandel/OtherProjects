namespace VelesTestTask.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<PersonsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PersonsContext context)
        {
            context
                .People
                .AddOrUpdate(
              p => p.Id,
              new Person { FirstName = "Петр", LastName = "Петров", PatronicName = "Петрович", Contacts = { "1", "2", "3" } },
              new Person { FirstName = "Иван", LastName = "Иванов", PatronicName = "Иванович", Contacts = { "2", "3", "4" } },
              new Person { FirstName = "Александров", LastName = "Александр", PatronicName = "Александрович", Contacts = { "3", "4", "5" } },
              new Person { FirstName = "Петр2", LastName = "Петров2", PatronicName = "Петрович2", Contacts = { "4", "5", "6" } },
              new Person { FirstName = "Иван2", LastName = "Иванов2", PatronicName = "Иванович2", Contacts = { "5", "6", "1" } },
              new Person { FirstName = "Александров2", LastName = "Александр2", PatronicName = "Александрович2", Contacts = { "6", "1", "2" } }
            );
        }
    }
}