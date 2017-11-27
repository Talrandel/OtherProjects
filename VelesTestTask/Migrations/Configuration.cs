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
              new Person { FirstName = "����", LastName = "������", PatronicName = "��������", Contacts = { "1", "2", "3" } },
              new Person { FirstName = "����", LastName = "������", PatronicName = "��������", Contacts = { "2", "3", "4" } },
              new Person { FirstName = "�����������", LastName = "���������", PatronicName = "�������������", Contacts = { "3", "4", "5" } },
              new Person { FirstName = "����2", LastName = "������2", PatronicName = "��������2", Contacts = { "4", "5", "6" } },
              new Person { FirstName = "����2", LastName = "������2", PatronicName = "��������2", Contacts = { "5", "6", "1" } },
              new Person { FirstName = "�����������2", LastName = "���������2", PatronicName = "�������������2", Contacts = { "6", "1", "2" } }
            );
        }
    }
}