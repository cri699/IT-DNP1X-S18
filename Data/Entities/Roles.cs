using System;
using System.Collections.Generic;

namespace CatsSystem.Data.Entities
{
    public partial class Roles
    {
        public Roles()
        {
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public ICollection<Users> Users { get; set; }
    }
}
