using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;
using SuperHeroAPI.Entities;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSuperHeroes()
        {
            var result = await _context.SuperHeroes.ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSuperHeroesById(int id)
        {
            var result = await _context.SuperHeroes.FirstOrDefaultAsync(s=>s.Id==id);

            if (result == null)
                return NotFound("Hero Not Found");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHero([FromBody]SuperHero id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _context.SuperHeroes.Add(id);
            await _context.SaveChangesAsync();

            return Ok("Hero added");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateHero([FromBody] SuperHero id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result=await _context.SuperHeroes.FindAsync(id.Id);

            if(result == null)
                return NotFound("Hero not found");

            result.Name = id.Name;
            result.FirstName = id.FirstName;
            result.LastName = id.LastName;
            result.Place = id.Place;

            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHero(int id)
        {
            var result = await _context.SuperHeroes.FirstOrDefaultAsync(s => s.Id == id);

            if (result == null)
                return NotFound("Hero Not Found");

            _context.SuperHeroes.Remove(result);
            await _context.SaveChangesAsync();

            return Ok("Hero "+result.Name+" Deleted");
        }
    }
}
