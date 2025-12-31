using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace E_commerce.infrastructer.Entities
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Categories Category { get; set; }

        public List<Photos> Photos { get; set; } = new();
    }
}
