using Core.Models;
using DataStore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformDemo.Controller
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/projects")]
    public class ProjectsV2Controller : ControllerBase
    {
        private readonly BugsContext _context;

        public ProjectsV2Controller(BugsContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_context.Projects.ToList());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await  _context.Projects.FindAsync(id);
            if(entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }

        [HttpGet]
        [Route("/api/projects/{pid}/tickets")]
        public async Task<IActionResult> GetProjectTickets(int pId)
        {
            var tickets = await _context.Tickets.Where(t => t.ProjectId == pId).ToListAsync();

            if (tickets == null || tickets.Count <= 0)
                return NotFound();

            return Ok(tickets);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Project project)
        {

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById),
                    new {id = project.ProjectId},
                    project
                );
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id , Project project)
        {
            if (id != project.ProjectId) return BadRequest();

            _context.Entry(project).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();

            }
            catch
            {
                if (_context.Projects.Find(id) == null)
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _context.Projects.FindAsync(id);

            if (entity == null) return NotFound();

            _context.Projects.Remove(entity);
            await _context.SaveChangesAsync();

            return Ok(entity);
        }

    }  
}
 