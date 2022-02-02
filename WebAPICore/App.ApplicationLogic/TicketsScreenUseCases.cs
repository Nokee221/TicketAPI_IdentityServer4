using Core.Models;
using MyApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public class TicketsScreenUseCases : ITicketsScreenUseCases
    {
        private readonly ITicketRepository ticketRepository;

        public TicketsScreenUseCases(ITicketRepository ticketRepository)
        {
            this.ticketRepository = ticketRepository;
        }

        public async Task<int> AddTicket(Ticket ticket)
        {
            return await this.ticketRepository.CreateAsync(ticket);
        }
    }
}
