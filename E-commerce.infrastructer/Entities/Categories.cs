using System;
using System.Collections.Generic;
using System.Text;

namespace E_commerce.infrastructer.Entities
{
    public class Categories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Products> Products { get; set; }
    }
}
