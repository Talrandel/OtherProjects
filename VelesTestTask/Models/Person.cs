using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VelesTestTask.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PatronicName { get; set; }
        public virtual List<string> Contacts { get; set; }
    }
}