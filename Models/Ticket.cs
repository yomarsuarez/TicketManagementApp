using System.Text.Json.Serialization;

namespace TicketManagementApp.Models;

public class Ticket
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public string Status { get; set; } = "Abierto";

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    public TicketStatus GetTicketStatus()
    {
        return Status switch
        {
            "Abierto" => TicketStatus.Abierto,
            "En progreso" => TicketStatus.EnProgreso,
            "Cerrado" => TicketStatus.Cerrado,
            _ => TicketStatus.Abierto
        };
    }

    public void SetTicketStatus(TicketStatus status)
    {
        Status = status switch
        {
            TicketStatus.Abierto => "Abierto",
            TicketStatus.EnProgreso => "En progreso",
            TicketStatus.Cerrado => "Cerrado",
            _ => "Abierto"
        };
    }
}
