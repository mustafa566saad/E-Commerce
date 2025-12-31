using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;


namespace E_commerce.Core.DTOs
{
    public record ProductDTO
    {
        public string Name { get; init; }
        public string Description { get; init; }
        [Column(TypeName = "decimal(18,2)")]

        public decimal Price { get; init; }
        public int CategoryID { get; init; }
        public IFormFileCollection Photos { get; init; }

    }

    public record UpdateProductDTO
    {
        public int ProductID { get; init; }
        public string? Name { get; init; }
        public string? Description { get; init; }
        [Column(TypeName = "decimal(18,2)")]

        public decimal Price { get; init; }
        public int CategoryID { get; init; }
        public IFormFileCollection? Photos { get; init; }
        public List<int>? DeletedPhotos { get; init; }
        public bool DeletAll { get; init; } = false;

    }
}
