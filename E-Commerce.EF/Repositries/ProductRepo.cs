using AutoMapper;
using E_commerce.Core.DTOs;
using E_commerce.Core.Services;
using E_commerce.infrastructer.Entities;
using E_commerce.infrastructer.Interfaces;
using E_Commerce.EF.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.EF.Repositries
{
    public class ProductRepo:BaseRepo<Products>, IProductRepo
    {
        private readonly IImageServices _image;
        private readonly IMapper mapper;
        private readonly AppDbContext _context;
        public ProductRepo(AppDbContext context,IMapper mapper,IImageServices image) : base(context)
        {
            _context = context;
            this._image = image;
            this.mapper = mapper;
        }

        public async Task<Products> AddProductAsync(ProductDTO productDTO)
        {
            var product = mapper.Map<Products>(productDTO);
            var ImagePaths = await _image.UploadImageAsync(productDTO.Photos, productDTO.Name);
            var photos = ImagePaths.Select(path => new Photos { Url = path, Product = product }).ToList();
            _context.Photos.AddRange(photos);
            product.Photos = photos;
            Add(product);
            return product;
        }

        public async Task<Products> UpdateProductAsync(Products OldProduct, UpdateProductDTO productDTO)
        {
            if (productDTO.DeletAll)
            {
                var OldPhotos = await _context.Photos.Where(e => e.ProductId == productDTO.ProductID).ToListAsync();
                foreach (var photo in OldPhotos)
                {
                    _image.DeleteImage(photo.Url);
                }
                _context.Photos.RemoveRange(OldPhotos);
            }

            if (productDTO.DeletedPhotos != null)
            {
                var DeletedPhotos= await _context.Photos.Where(e=>productDTO.DeletedPhotos.Contains(e.Id)).ToListAsync();
                foreach (var photo in DeletedPhotos)
                {
                    _image.DeleteImage(photo.Url);
                }
                _context.Photos.RemoveRange(DeletedPhotos);
            }

            if (productDTO.Name != null)
                OldProduct.Name = productDTO.Name;            

            if(productDTO.Description != null)
                OldProduct.Description = productDTO.Description;

            if(productDTO.Price != 0)
                OldProduct.Price = productDTO.Price;

            if(productDTO.CategoryID != 0)
                OldProduct.CategoryId = productDTO.CategoryID;

            if (productDTO.Photos != null)
            {
                var ImagePaths = await _image.UploadImageAsync(productDTO.Photos, OldProduct.Name);
                var photos = ImagePaths.Select(path => new Photos { Url = path, Product = OldProduct }).ToList();
                await _context.Photos.AddRangeAsync(photos);
                OldProduct.Photos.AddRange(photos);
            }
            Update(OldProduct);

            return OldProduct;
        }
    }
}
