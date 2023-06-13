using System;
using System.Collections.Generic;

#nullable disable

namespace Repository.Models
{
    public partial class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public DateTime? Yob { get; set; }
    }
}
