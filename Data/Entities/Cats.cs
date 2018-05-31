using System;
using System.Collections.Generic;

namespace CatsSystem.Data.Entities
{
    public partial class Cats
    {
        public Cats()
        {
            Carts = new HashSet<Carts>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Photo { get; set; }
        public string Status { get; set; }

        public ICollection<Carts> Carts { get; set; }
    }
}
