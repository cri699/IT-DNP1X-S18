using System;
using System.Collections.Generic;

namespace CatsSystem.Data.Entities
{
    public partial class Carts
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CatId { get; set; }

        public Cats Cat { get; set; }
        public Users User { get; set; }
    }
}
