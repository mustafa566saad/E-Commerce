using E_commerce.Core.DTOs;
using E_commerce.infrastructer.Entities;

namespace E_commerce.infrastructer.Interfaces
{
    public interface IProductRepo: IBaseRepo<Products>
    {
        public Task<Products> AddProductAsync(ProductDTO productDTO);
        public Task<Products> UpdateProductAsync(Products OldProduct, UpdateProductDTO productDTO);

    }
}
