using Core.Models;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public interface ITicketsScreenUseCases
    {
        Task<int> AddTicket(Ticket ticket);
    }
}