using System.Text.Json;
using TicketManagementApp.Models;

namespace TicketManagementApp.Services;

public class TicketService : ITicketService
{
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger<TicketService> _logger;
    private List<Ticket> _tickets;

    public TicketService(IWebHostEnvironment environment, ILogger<TicketService> logger)
    {
        _environment = environment;
        _logger = logger;
        _tickets = new List<Ticket>();
        LoadTicketsAsync().Wait();
    }

    private async Task LoadTicketsAsync()
    {
        try
        {
            var jsonPath = Path.Combine(_environment.ContentRootPath, "Data", "tickets.json");

            if (!File.Exists(jsonPath))
            {
                _logger.LogWarning($"Archivo tickets.json no encontrado en: {jsonPath}");
                return;
            }

            var jsonContent = await File.ReadAllTextAsync(jsonPath);
            _tickets = JsonSerializer.Deserialize<List<Ticket>>(jsonContent) ?? new List<Ticket>();
            _logger.LogInformation($"Cargados {_tickets.Count} tickets desde el archivo JSON");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al cargar los tickets");
            _tickets = new List<Ticket>();
        }
    }

    public async Task<List<Ticket>> GetAllTicketsAsync()
    {
        // Simulando latencia de red
        await Task.Delay(300);
        return _tickets.ToList();
    }

    public async Task<Ticket?> GetTicketByIdAsync(int id)
    {
        // Simulando latencia de red
        await Task.Delay(200);
        return _tickets.FirstOrDefault(t => t.Id == id);
    }

    public async Task<bool> UpdateTicketStatusAsync(int id, string newStatus)
    {
        // Simulando latencia de red
        await Task.Delay(400);

        var ticket = _tickets.FirstOrDefault(t => t.Id == id);
        if (ticket == null)
        {
            _logger.LogWarning($"Ticket con ID {id} no encontrado");
            return false;
        }

        var validStatuses = new[] { "Abierto", "En progreso", "Cerrado" };
        if (!validStatuses.Contains(newStatus))
        {
            _logger.LogWarning($"Estado inválido: {newStatus}");
            return false;
        }

        ticket.Status = newStatus;
        _logger.LogInformation($"Ticket {id} actualizado a estado: {newStatus}");
        return true;
    }
}
