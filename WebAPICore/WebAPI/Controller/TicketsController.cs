using Core.Models;
using DataStore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.QueryFilters;

namespace PlatformDemo.Controller
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly BugsContext context;

        public TicketsController(BugsContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] TicketQueryFilter ticketQueryFilter)
        {
            IQueryable<Ticket> tickets = context.Tickets;

            if(ticketQueryFilter != null)
            {
                if(ticketQueryFilter.Id.HasValue)
                {
                    tickets.Where(x => x.TicketId == ticketQueryFilter.Id);

                }

                if(!string.IsNullOrWhiteSpace(ticketQueryFilter.Title))
                {
                    tickets = tickets.Where(x => x.Title.Contains(ticketQueryFilter.Title, StringComparison.OrdinalIgnoreCase));
                }

                if (!string.IsNullOrWhiteSpace(ticketQueryFilter.Description))
                {
                    tickets = tickets.Where(x => x.Description.Contains(ticketQueryFilter.Description, StringComparison.OrdinalIgnoreCase));
                }

            }
            
            return Ok(await context.Tickets.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) 
        {
            var ticket = await context.Tickets.FindAsync(id);
            if (ticket == null) return BadRequest();

            return Ok(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Ticket ticket) 
        {
            context.Tickets.Add(ticket);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = ticket.TicketId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id , [FromBody] Ticket ticket)
        {
            if (id != ticket.TicketId) return BadRequest();

            context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch
            {
                if (context.Tickets.Find(id) == null)
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await context.Tickets.FindAsync(id);
            if (ticket == null) return NotFound();

            context.Tickets.Remove(ticket);
            await context.SaveChangesAsync();

            return Ok(ticket);
        }
    }

} 

