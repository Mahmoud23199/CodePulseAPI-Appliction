using CodePulseBL.DTOs;
using CodePulseBL.Repository;
using CodePulseBL.UnitOfWork;
using CodePulseDB.Models.Domain;
using CodePulseDB.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unit;

        public CategoriesController(IUnitOfWork unit)
        {
            this._unit = unit;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _unit.Categorys.GetAllAsync(i => true);

            if (items == null)
            {
                return NotFound();
            }
            return Ok(items);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id != Guid.Empty)
            {
                var item = await _unit.Categorys.GetByIdAsync(i => i.Id == id, new string[] { });

                if (item != null)
                {
                    return Ok(item);
                }
                else
                {
                    return NotFound($"Item with id {id} not found");
                }
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateCategories(CreateCategoriesDto categoriesDto)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = categoriesDto.Name,
                    UrlHandle = categoriesDto.UrlHandle,
                };

                // Save in DB
                await _unit.Categorys.AddedAsync(category);

                // Prepare the response DTO
                var response = new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    UrlHandle = category.UrlHandle
                };

                return  CreatedAtAction(nameof(GetById), new {id = response.Id},response);
               // return Ok(response);
            }
            else
            {
                ModelState.AddModelError("", "Invalid input data");
                return BadRequest(ModelState);
            }
        }


        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(Guid id,CreateCategoriesDto categoryDto) 
        {

            if (ModelState.IsValid)
            {
                var category = await _unit.Categorys.GetByIdAsync(i=>i.Id== id);

                if (category != null)
                {
                    category.UrlHandle = categoryDto.UrlHandle;
                    category.Name = categoryDto.Name;

                  var result= await _unit.Categorys.UpdateAsync(category);

                    return Ok(result);
                }
                else
                {
                    return NotFound("not found");
                }
            }
            else
            {
                ModelState.Values.SelectMany(i=>i.Errors).Select(i=>i.ErrorMessage).ToList();

                return BadRequest(ModelState);
            }
        
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

                var category = await _unit.Categorys.GetByIdAsync(i => i.Id == id);

            if (category != null)
            {

                var result = await _unit.Categorys.DeleteAsync(category);
                if (result)
                {
                    return Ok("Categories Deleted succesfully");
                }
                return NoContent();
            }
            else
            {
                return NotFound("not found");
            }
          

        }

    }
}
