using E_commerce.infrastructer.Entities;
using E_commerce.infrastructer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _work;
        public CategoriesController(IUnitOfWork work)
        {
            _work = work;
        }
        [HttpGet("Categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _work.CategoryRepo.GetAllAsync();
                if (categories == null)
                    return BadRequest("No Categories Found");
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            if(!ModelState.IsValid)
                return BadRequest("Invalid Data");
            try
            {
                var category = await _work.CategoryRepo.GetByIdAsync(id);
                if (category == null)
                    return BadRequest("Category Not Found");
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] string categoryName)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if(string.IsNullOrWhiteSpace(categoryName))
                return BadRequest("Category Name Cannot Be Empty");
            try
            {
                var category = new Categories { Name = categoryName };
                _work.CategoryRepo.Add(category);
                await _work.SaveChangesAsync();

                return Ok("Category Created Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
