using Core.Models;
using DataStore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Filters.v2;

namespace PlatformDemo.Controller
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/tickets")]
    public class TicketV2Controller : ControllerBase
    {
        private readonly BugsContext context;

        public TicketV2Controller(BugsContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
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
        [Ticker_EnsureDescriptionPresent]
        public async Task<IActionResult> Post([FromBody] Ticket ticket) 
        {
            context.Tickets.Add(ticket);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = ticket.TicketId });
        }

        [HttpPut("{id}")]
        [Ticker_EnsureDescriptionPresent]
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

