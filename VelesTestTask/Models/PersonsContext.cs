using System.Data.Entity;

namespace VelesTestTask.Models
{
    public class PersonsContext : DbContext
    {    
        public PersonsContext() : base("PersonsContext")
        {
        }

        public DbSet<Person> People { get; set; }
    }
}