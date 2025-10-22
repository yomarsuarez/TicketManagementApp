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
            _logger.LogInformation($"Intentando cargar tickets desde: {jsonPath}");

            if (!File.Exists(jsonPath))
            {
                _logger.LogWarning($"Archivo tickets.json no encontrado en: {jsonPath}. Usando datos en memoria.");
                LoadInMemoryData();
                return;
            }

            var jsonContent = await File.ReadAllTextAsync(jsonPath);
            _tickets = JsonSerializer.Deserialize<List<Ticket>>(jsonContent) ?? new List<Ticket>();
            _logger.LogInformation($"Cargados {_tickets.Count} tickets desde el archivo JSON");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al cargar los tickets. Usando datos en memoria.");
            LoadInMemoryData();
        }
    }

    private void LoadInMemoryData()
    {
        _tickets = new List<Ticket>
        {
            new Ticket { Id = 1, Title = "No puedo iniciar sesión", Description = "Intento entrar con mi cuenta y no me deja. He probado resetear la contraseña pero sigo sin poder acceder.", Status = "Abierto", CreatedAt = DateTime.Parse("2025-10-16T10:30:00Z") },
            new Ticket { Id = 2, Title = "Error al cargar la página de perfil", Description = "Cuando intento acceder a mi perfil, la página se queda en blanco y no carga ningún dato.", Status = "En progreso", CreatedAt = DateTime.Parse("2025-10-17T09:15:00Z") },
            new Ticket { Id = 3, Title = "No recibo correos de notificación", Description = "Tengo activadas las notificaciones por correo pero no llegan a mi bandeja de entrada.", Status = "Abierto", CreatedAt = DateTime.Parse("2025-10-18T14:20:00Z") },
            new Ticket { Id = 4, Title = "Problema con el pago", Description = "Mi tarjeta fue rechazada pero se realizó el cobro. Necesito ayuda urgente.", Status = "En progreso", CreatedAt = DateTime.Parse("2025-10-19T11:45:00Z") },
            new Ticket { Id = 5, Title = "La aplicación móvil se cierra sola", Description = "Cada vez que abro la app en mi iPhone, se cierra después de unos segundos.", Status = "Cerrado", CreatedAt = DateTime.Parse("2025-10-15T08:00:00Z") },
            new Ticket { Id = 6, Title = "No puedo subir archivos", Description = "Al intentar adjuntar documentos PDF, aparece un error y no se completa la carga.", Status = "Abierto", CreatedAt = DateTime.Parse("2025-10-20T16:30:00Z") },
            new Ticket { Id = 7, Title = "Búsqueda no funciona correctamente", Description = "Los resultados de búsqueda no coinciden con los términos que ingreso.", Status = "Cerrado", CreatedAt = DateTime.Parse("2025-10-14T13:10:00Z") },
            new Ticket { Id = 8, Title = "Solicitud de eliminación de cuenta", Description = "Quiero eliminar permanentemente mi cuenta y todos mis datos asociados.", Status = "En progreso", CreatedAt = DateTime.Parse("2025-10-21T10:00:00Z") },
            new Ticket { Id = 9, Title = "Dashboard no muestra estadísticas", Description = "Los gráficos y métricas en el dashboard aparecen vacíos aunque tengo datos registrados.", Status = "Abierto", CreatedAt = DateTime.Parse("2025-10-21T15:45:00Z") },
            new Ticket { Id = 10, Title = "Error 500 al actualizar información", Description = "Cada vez que intento actualizar mi información de contacto obtengo un error 500.", Status = "Abierto", CreatedAt = DateTime.Parse("2025-10-22T09:20:00Z") }
        };
        _logger.LogInformation($"Cargados {_tickets.Count} tickets desde memoria");
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
