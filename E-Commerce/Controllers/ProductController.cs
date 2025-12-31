using AutoMapper;
using E_commerce.Core.DTOs;
using E_commerce.infrastructer.Entities;
using E_commerce.infrastructer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _work;
        public ProductController(IUnitOfWork work)
        {
            _work = work;
        }

        [HttpGet("Products")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _work.ProductRepo.GetAllAsync();
                if (products == null)
                    return BadRequest("No Products Found");
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _work.ProductRepo.GetByIdAsync(id);
                if (product == null)
                    return BadRequest("Product Not Found");
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(ProductDTO productDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = await _work.CategoryRepo.GetByExpressionAsync(e=>e.Id == productDto.CategoryID);
            if(category == null)
                return BadRequest("Category Not Found");
            if(productDto.Photos == null || productDto.Photos.Count == 0)
                return BadRequest("At least one photo is required");
            if (productDto.Price == 0)
                return BadRequest("Price must be more than 0");
            try
            {
                var check = await _work.ProductRepo.AddProductAsync(productDto);
                if (check == null)
                    return BadRequest("Failed to Create Product");

                await _work.SaveChangesAsync();
                return Ok("Product Created Successfully");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDTO productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var existingProduct = await _work.ProductRepo.GetByIdAsync(productDto.ProductID);
            if (existingProduct == null)
                return BadRequest("Product Not Found");

            if (productDto.CategoryID != 0)
            {
                var category = await _work.CategoryRepo.GetByExpressionAsync(e => e.Id == productDto.CategoryID);
                if (category == null)
                    return BadRequest("Category Not Found");
            }
            try
            {
                var check = await _work.ProductRepo.UpdateProductAsync(existingProduct,productDto);
                if (check == null)
                    return BadRequest("Failed to Update Product");
                await _work.SaveChangesAsync();
                return Ok("Product Updated Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
