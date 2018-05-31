using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CatsSystem.Data.Entities
{
    public partial class Users
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
       
        [ForeignKey("RoleId")]
        public Roles Role { get; set; }
        public ICollection<Carts> Carts { get; set; }
    }
}
