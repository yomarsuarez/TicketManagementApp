using TicketManagementApp.Models;

namespace TicketManagementApp.Services;

public interface ITicketService
{
    Task<List<Ticket>> GetAllTicketsAsync();
    Task<Ticket?> GetTicketByIdAsync(int id);
    Task<bool> UpdateTicketStatusAsync(int id, string newStatus);
}
