using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace E_commerce.infrastructer.Entities
{
    public class Photos
    {
        public int Id { get; set; }
        public string Url { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Products Product { get; set; }
    }
}
