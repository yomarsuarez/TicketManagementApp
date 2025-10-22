# Arquitectura Técnica - Sistema de Gestión de Tickets

## 📋 Índice de la Explicación Técnica

Esta guía te llevará paso a paso a través de toda la arquitectura, desde el punto de entrada hasta los componentes más específicos.

---

## 🎯 Orden de Presentación Recomendado

```
1. Program.cs - Punto de entrada y configuración
2. Estructura de carpetas - Organización del proyecto
3. Models - Capa de datos
4. Services - Lógica de negocio
5. Components/Layout - Estructura visual base
6. Components/Pages - Páginas principales
7. Components/Tickets - Componentes específicos
8. Flujo de datos - Cómo interactúan las capas
9. Características técnicas clave
```

---

# PARTE 1: PUNTO DE ENTRADA

## 📄 1. Program.cs - El Corazón de la Aplicación

**Ubicación**: `TicketManagementApp/Program.cs`

### ¿Qué es?
Es el punto de entrada de la aplicación. Configura todos los servicios, middleware y la aplicación Blazor.

### Código Explicado Línea por Línea:

```csharp
using TicketManagementApp.Components;
using TicketManagementApp.Services;
```
**Líneas 1-2**: Importamos los namespaces necesarios:
- `Components`: Donde están todos nuestros componentes Razor
- `Services`: Donde está la lógica de negocio

```csharp
var builder = WebApplication.CreateBuilder(args);
```
**Línea 4**: Crea el constructor de la aplicación web. Este objeto `builder` se usa para configurar todo antes de ejecutar la app.

```csharp
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
```
**Líneas 6-7**:
- `AddRazorComponents()`: Habilita los componentes Razor (archivos .razor)
- `AddInteractiveServerComponents()`: Habilita el modo interactivo de Blazor Server
  - Esto permite que la UI responda en tiempo real sin recargar la página
  - Usa SignalR por debajo para comunicación en tiempo real

```csharp
builder.Services.AddSingleton<ITicketService, TicketService>();
```
**Línea 11**: **MUY IMPORTANTE** - Inyección de Dependencias (DI)
- Registra el servicio `TicketService` como un Singleton
- `Singleton`: Una sola instancia para toda la aplicación
- `ITicketService`: Interfaz (contrato)
- `TicketService`: Implementación concreta
- Esto permite usar `@inject ITicketService` en cualquier componente

```csharp
var app = builder.Build();
```
**Línea 13**: Construye la aplicación con toda la configuración anterior.

```csharp
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
```
**Líneas 15-19**: Configuración de seguridad para PRODUCCIÓN:
- Si NO estamos en desarrollo, usa manejo de errores global
- HSTS: Fuerza HTTPS en producción

```csharp
app.UseHttpsRedirection();
app.UseAntiforgery();
```
**Líneas 21-22**:
- Redirige HTTP a HTTPS automáticamente
- Antiforgery: Protección contra ataques CSRF

```csharp
app.MapStaticAssets();
```
**Línea 24**: Mapea archivos estáticos (CSS, JS, imágenes) para que se sirvan correctamente.

```csharp
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
```
**Líneas 25-26**: Configura el enrutamiento de componentes Razor:
- `<App>`: Componente raíz de la aplicación
- `AddInteractiveServerRenderMode()`: Habilita interactividad del lado del servidor

```csharp
app.Run();
```
**Línea 28**: Inicia la aplicación y comienza a escuchar peticiones.

### Conceptos Clave:
- ✅ **Inyección de Dependencias**: Pattern para desacoplar código
- ✅ **Blazor Server**: Renderizado del lado del servidor con interactividad
- ✅ **SignalR**: Comunicación en tiempo real (usado por Blazor Server)
- ✅ **Singleton**: Una sola instancia del servicio para toda la app

---

# PARTE 2: ESTRUCTURA DEL PROYECTO

## 📁 2. Estructura de Carpetas - Organización

```
TicketManagementApp/
│
├── Program.cs                    ⭐ PUNTO DE ENTRADA
│
├── Models/                       📊 CAPA DE DATOS
│   ├── Ticket.cs                 → Modelo principal
│   └── TicketStatus.cs           → Enum de estados
│
├── Services/                     🔧 LÓGICA DE NEGOCIO
│   ├── ITicketService.cs         → Contrato (interfaz)
│   └── TicketService.cs          → Implementación
│
├── Data/                         💾 DATOS MOCK
│   └── tickets.json              → 10 tickets de ejemplo
│
├── Components/                   🎨 COMPONENTES VISUALES
│   ├── App.razor                 → Componente raíz
│   ├── Routes.razor              → Configuración de rutas
│   ├── _Imports.razor            → Imports globales
│   │
│   ├── Layout/                   📐 ESTRUCTURA BASE
│   │   ├── MainLayout.razor      → Layout principal
│   │   ├── MainLayout.razor.css  → Estilos del layout
│   │   ├── NavMenu.razor         → Menú de navegación
│   │   └── NavMenu.razor.css     → Estilos del menú
│   │
│   ├── Pages/                    📄 PÁGINAS PRINCIPALES
│   │   ├── Home.razor            → Página de inicio (/)
│   │   ├── Home.razor.css        → Estilos de Home
│   │   ├── Tickets.razor         → Dashboard de tickets (/tickets)
│   │   ├── Tickets.razor.css     → Estilos del dashboard
│   │   └── Error.razor           → Página de errores
│   │
│   └── Tickets/                  🎫 COMPONENTES ESPECÍFICOS
│       ├── TicketFilters.razor        → Búsqueda y filtros
│       ├── TicketFilters.razor.css    → Estilos de filtros
│       ├── TicketList.razor           → Lista de tickets
│       ├── TicketList.razor.css       → Estilos de lista
│       ├── TicketDetail.razor         → Modal de detalle
│       └── TicketDetail.razor.css     → Estilos del modal
│
└── wwwroot/                      🌐 ARCHIVOS ESTÁTICOS
    ├── app.css                   → Estilos globales
    └── lib/                      → Librerías (Bootstrap)
```

### Principios de Organización:

1. **Separación de Responsabilidades**: Cada carpeta tiene un propósito claro
2. **Models**: Solo definen la estructura de datos (POCOs)
3. **Services**: Toda la lógica de negocio y acceso a datos
4. **Components**: Solo lógica de presentación e interactividad
5. **CSS Isolated**: Cada componente tiene su propio archivo CSS

---

# PARTE 3: MODELOS DE DATOS

## 📊 3. Models/TicketStatus.cs - Enum de Estados

**Ubicación**: `Models/TicketStatus.cs`

```csharp
namespace TicketManagementApp.Models;

public enum TicketStatus
{
    Abierto,
    EnProgreso,
    Cerrado
}
```

### ¿Por qué un Enum?
- ✅ **Type Safety**: No se pueden usar valores inválidos
- ✅ **IntelliSense**: Autocompletado en el IDE
- ✅ **Refactoring**: Fácil de renombrar

### Uso:
```csharp
var status = TicketStatus.Abierto;
// No puedes hacer: var status = "Abiert0" (typo)
```

---

## 📊 4. Models/Ticket.cs - Modelo Principal

**Ubicación**: `Models/Ticket.cs`

```csharp
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
```

### Explicación Detallada:

**Atributo `[JsonPropertyName]`**:
- Mapea las propiedades de C# a las propiedades del JSON
- Ejemplo: `Id` en C# ↔ `"id"` en JSON
- Permite usar convenciones de C# (PascalCase) y JSON (camelCase)

**Propiedades con Valores por Defecto**:
```csharp
public string Title { get; set; } = string.Empty;
```
- Inicializa con cadena vacía en lugar de `null`
- Evita `NullReferenceException`
- Cumple con `Nullable Reference Types` de C# 9.0+

**Métodos Helper**:
```csharp
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
```
- **Pattern Matching**: Switch moderno de C#
- Convierte `string` a `enum`
- `_`: Caso por defecto (default)

```csharp
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
```
- Convierte `enum` a `string`
- Útil para serializar a JSON

### Concepto Clave: POCO (Plain Old CLR Object)
- Clase simple con solo propiedades
- Sin lógica de negocio compleja
- Fácil de serializar/deserializar

---

# PARTE 4: CAPA DE SERVICIOS

## 🔧 5. Services/ITicketService.cs - Contrato del Servicio

**Ubicación**: `Services/ITicketService.cs`

```csharp
using TicketManagementApp.Models;

namespace TicketManagementApp.Services;

public interface ITicketService
{
    Task<List<Ticket>> GetAllTicketsAsync();
    Task<Ticket?> GetTicketByIdAsync(int id);
    Task<bool> UpdateTicketStatusAsync(int id, string newStatus);
}
```

### ¿Por qué una Interfaz?

1. **Abstracción**: Define QUÉ hace el servicio, no CÓMO
2. **Testabilidad**: Fácil de crear mocks para pruebas
3. **Inyección de Dependencias**: Se inyecta `ITicketService`, no `TicketService`
4. **Flexibilidad**: Podemos cambiar la implementación sin tocar los componentes

### Diseño de la API:

**GET /tickets** → `GetAllTicketsAsync()`
- Retorna lista completa de tickets
- `Task<List<Ticket>>`: Operación asíncrona

**GET /tickets/{id}** → `GetTicketByIdAsync(int id)`
- Retorna un ticket específico
- `Ticket?`: Puede retornar `null` si no existe

**PATCH /tickets/{id}** → `UpdateTicketStatusAsync(int id, string newStatus)`
- Actualiza solo el estado
- `Task<bool>`: Retorna éxito/fracaso

---

## 🔧 6. Services/TicketService.cs - Implementación

**Ubicación**: `Services/TicketService.cs`

### Constructor e Inyección de Dependencias:

```csharp
private readonly IWebHostEnvironment _environment;
private readonly ILogger<TicketService> _logger;
private List<Ticket> _tickets;

public TicketService(IWebHostEnvironment environment, ILogger<TicketService> logger)
{
    _environment = environment;
    _logger = logger;
    _tickets = new List<Ticket>();
    LoadTickets();
}
```

**Inyección de Dependencias**:
- `IWebHostEnvironment`: Info del entorno (Development, Production, rutas)
- `ILogger<TicketService>`: Sistema de logging integrado de .NET

**Patrón Constructor Injection**:
- ASP.NET Core inyecta automáticamente estas dependencias
- No necesitamos crear instancias manualmente

### Carga de Datos:

```csharp
private void LoadTickets()
{
    try
    {
        var jsonPath = Path.Combine(_environment.ContentRootPath, "Data", "tickets.json");
        _logger.LogInformation($"Intentando cargar tickets desde: {jsonPath}");

        if (!File.Exists(jsonPath))
        {
            _logger.LogWarning($"Archivo tickets.json no encontrado. Usando datos en memoria.");
            LoadInMemoryData();
            return;
        }

        var jsonContent = File.ReadAllText(jsonPath);
        _tickets = JsonSerializer.Deserialize<List<Ticket>>(jsonContent) ?? new List<Ticket>();
        _logger.LogInformation($"Cargados {_tickets.Count} tickets desde el archivo JSON");
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error al cargar los tickets. Usando datos en memoria.");
        LoadInMemoryData();
    }
}
```

**Características Técnicas**:

1. **Fallback Pattern**: Si falla cargar JSON, usa datos en memoria
2. **Logging Estructurado**: Usa ILogger para diagnóstico
3. **Deserialización JSON**: Usa `System.Text.Json`
4. **Null Coalescing**: `?? new List<Ticket>()` - Si deserializa null, crea lista vacía

### Simulación de API con Latencia:

```csharp
public async Task<List<Ticket>> GetAllTicketsAsync()
{
    // Simulando latencia de red
    await Task.Delay(300);
    return _tickets.ToList();
}
```

**¿Por qué `Task.Delay(300)`?**
- Simula una llamada HTTP real (~300ms de latencia)
- Permite ver el estado de "Cargando..." en la UI
- Más realista para demostración

**¿Por qué `.ToList()`?**
- Crea una copia de la lista
- Evita que componentes modifiquen directamente `_tickets`
- **Defensive Programming**

### Actualización de Estado (PATCH):

```csharp
public async Task<bool> UpdateTicketStatusAsync(int id, string newStatus)
{
    await Task.Delay(400); // Latencia simulada

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
```

**Validaciones**:
1. ✅ Verifica que el ticket existe
2. ✅ Valida que el nuevo estado es correcto
3. ✅ Registra el cambio en logs
4. ✅ Retorna éxito/fracaso

**Patrón Repository**:
- El servicio abstrae el acceso a datos
- Los componentes no saben si los datos vienen de JSON, DB, o API
- Fácil de cambiar la fuente de datos en el futuro

---

# PARTE 5: COMPONENTES DE LAYOUT

## 📐 7. Components/Layout/MainLayout.razor - Estructura Base

**Ubicación**: `Components/Layout/MainLayout.razor`

Este componente define la estructura HTML base de TODAS las páginas.

```razor
@inherits LayoutComponentBase

<div class="page">
    <div class="sidebar">
        <NavMenu />
    </div>

    <main>
        <div class="top-row px-4">
            <!-- Header aquí -->
        </div>

        <article class="content px-4">
            @Body
        </article>
    </main>
</div>
```

**`@inherits LayoutComponentBase`**:
- Hereda de la clase base de layouts en Blazor
- Proporciona la propiedad `@Body`

**`@Body`**:
- Placeholder donde se renderizan las páginas
- Cuando navegas a `/tickets`, aquí se inserta `Tickets.razor`

**Estructura**:
- `sidebar`: Menú de navegación (fijo a la izquierda)
- `main`: Contenido principal de la página

---

## 📐 8. Components/Layout/NavMenu.razor - Menú de Navegación

**Ubicación**: `Components/Layout/NavMenu.razor`

```razor
<div class="nav-scrollable">
    <nav class="nav flex-column">
        <div class="nav-item px-3">
            <NavLink class="nav-link" href="" Match="NavLinkMatch.All">
                <span class="bi bi-house-door-fill-nav-menu"></span> Home
            </NavLink>
        </div>

        <div class="nav-item px-3">
            <NavLink class="nav-link" href="tickets">
                <span class="bi bi-envelope-fill-nav-menu"></span> Tickets
            </NavLink>
        </div>
    </nav>
</div>
```

**Componente `<NavLink>`**:
- Componente especial de Blazor para navegación
- Añade automáticamente clase `active` cuando la ruta coincide
- `Match="NavLinkMatch.All"`: Coincide solo cuando la URL es EXACTAMENTE "/"

**CSS Isolated**:
- `NavMenu.razor.css` solo afecta a este componente
- Los estilos NO se filtran a otros componentes
- Blazor genera clases únicas automáticamente

---

# PARTE 6: PÁGINAS PRINCIPALES

## 📄 9. Components/Pages/Home.razor - Página de Inicio

**Ubicación**: `Components/Pages/Home.razor`

```razor
@page "/"
@inject NavigationManager Navigation
@rendermode InteractiveServer
```

**Directivas de Página**:

**`@page "/"`**:
- Define la ruta de esta página
- Esta página se muestra cuando navegas a la raíz del sitio

**`@inject NavigationManager Navigation`**:
- Inyecta el servicio de navegación
- Permite navegar programáticamente: `Navigation.NavigateTo("/tickets")`

**`@rendermode InteractiveServer`**: ⭐ CRÍTICO
- Habilita la interactividad en esta página
- Sin esto, los eventos `@onclick` NO funcionan
- Blazor Server necesita esto para establecer la conexión SignalR

### Navegación Programática:

```razor
<button class="btn-primary-home" @onclick="GoToTickets">
    Ir al Dashboard de Tickets
</button>

@code {
    private void GoToTickets()
    {
        Navigation.NavigateTo("/tickets");
    }
}
```

**Flujo de Evento**:
1. Usuario hace clic en el botón
2. Blazor Server captura el evento vía SignalR
3. Ejecuta `GoToTickets()` en el servidor
4. `NavigationManager` cambia la ruta a `/tickets`
5. Blazor renderiza `Tickets.razor` en el cliente

---

## 📄 10. Components/Pages/Tickets.razor - Dashboard Principal

**Ubicación**: `Components/Pages/Tickets.razor`

### Estructura y Directivas:

```razor
@page "/tickets"
@using TicketManagementApp.Models
@using TicketManagementApp.Services
@using TicketManagementApp.Components.Tickets
@inject ITicketService TicketService
@rendermode InteractiveServer
```

**`@using`**:
- Importa namespaces para usar dentro del componente
- Evita escribir `TicketManagementApp.Models.Ticket` cada vez

**`@inject ITicketService TicketService`**:
- Inyecta el servicio que registramos en `Program.cs`
- Blazor resuelve automáticamente la dependencia

### Estado del Componente:

```csharp
@code {
    private List<Ticket> allTickets = new();
    private List<Ticket> filteredTickets = new();
    private Ticket? selectedTicket;
    private bool isLoading = true;
    private string? errorMessage;
    private string currentFilter = "Todos";
    private string currentSearch = "";
```

**Variables de Estado**:
- `allTickets`: Lista original sin filtrar
- `filteredTickets`: Lista después de aplicar filtros/búsqueda
- `selectedTicket`: Ticket abierto en el modal (null si está cerrado)
- `isLoading`: Control del spinner de carga
- `currentFilter`: Estado actual del filtro ("Todos", "Abierto", etc.)
- `currentSearch`: Texto de búsqueda actual

### Ciclo de Vida del Componente:

```csharp
protected override async Task OnInitializedAsync()
{
    try
    {
        await LoadTickets();
    }
    catch (Exception ex)
    {
        errorMessage = $"Error al cargar los tickets: {ex.Message}";
    }
    finally
    {
        isLoading = false;
    }
}
```

**`OnInitializedAsync()`**:
- Se ejecuta UNA VEZ cuando el componente se carga
- Perfecto para cargar datos iniciales
- `async`: Permite usar `await` para operaciones asíncronas

**Patrón try-catch-finally**:
- `try`: Intenta cargar los tickets
- `catch`: Si falla, guarda el mensaje de error
- `finally`: Siempre oculta el spinner (pase lo que pase)

### Lógica de Filtrado:

```csharp
private void ApplyFilters()
{
    filteredTickets = allTickets;

    if (currentFilter != "Todos")
    {
        filteredTickets = filteredTickets.Where(t => t.Status == currentFilter).ToList();
    }

    if (!string.IsNullOrWhiteSpace(currentSearch))
    {
        filteredTickets = filteredTickets.Where(t =>
            t.Title.Contains(currentSearch, StringComparison.OrdinalIgnoreCase) ||
            t.Description.Contains(currentSearch, StringComparison.OrdinalIgnoreCase)
        ).ToList();
    }
}
```

**LINQ (Language Integrated Query)**:
- `.Where()`: Filtra elementos que cumplan la condición
- `.Contains()`: Busca substring
- `StringComparison.OrdinalIgnoreCase`: Ignora mayúsculas/minúsculas

**Filtrado Combinado**:
1. Primero aplica filtro de estado
2. Luego aplica búsqueda de texto
3. El resultado es la intersección de ambos filtros

### Comunicación con Componentes Hijos:

```razor
<TicketFilters
    OnFilterChanged="HandleFilterChange"
    OnSearchChanged="HandleSearchChange" />
```

**EventCallback**:
- El componente hijo (`TicketFilters`) notifica al padre
- Cuando el usuario cambia el filtro, llama a `HandleFilterChange`
- **Flujo unidireccional de datos** (como React)

```csharp
private void HandleFilterChange(string filter)
{
    currentFilter = filter;
    ApplyFilters();
}
```

### Renderizado Condicional:

```razor
@if (isLoading)
{
    <div class="loading-container">
        <div class="spinner"></div>
        <p>Cargando tickets...</p>
    </div>
}
else if (errorMessage != null)
{
    <div class="error-message">
        <span class="error-icon">⚠</span>
        <p>@errorMessage</p>
    </div>
}
else
{
    <!-- Muestra los tickets -->
}
```

**Estados de la UI**:
1. **Loading**: Muestra spinner mientras carga
2. **Error**: Muestra mensaje si algo falla
3. **Success**: Muestra los tickets

---

# PARTE 7: COMPONENTES ESPECÍFICOS

## 🎫 11. Components/Tickets/TicketFilters.razor - Búsqueda y Filtros

**Ubicación**: `Components/Tickets/TicketFilters.razor`

### Parámetros del Componente:

```csharp
[Parameter]
public EventCallback<string> OnFilterChanged { get; set; }

[Parameter]
public EventCallback<string> OnSearchChanged { get; set; }
```

**`[Parameter]`**:
- Atributo que marca propiedades que se pasan desde el padre
- Similar a `props` en React o `@Input()` en Angular

**`EventCallback<T>`**:
- Tipo especial de Blazor para callbacks
- Asíncrono por defecto
- `T`: Tipo del parámetro que se pasa al callback

### Input de Búsqueda en Tiempo Real:

```razor
<input
    type="text"
    placeholder="Buscar tickets..."
    value="@searchText"
    @oninput="OnSearchInput"
    class="search-input" />
```

**`value="@searchText"`**:
- Enlace ONE-WAY (solo lectura)
- La UI muestra el valor de `searchText`

**`@oninput="OnSearchInput"`**:
- Se ejecuta en CADA tecla presionada
- Búsqueda en tiempo real (no necesita Enter)

**¿Por qué no `@bind`?**:
- `@bind` tiene un delay (solo actualiza al perder foco)
- `@oninput` es inmediato (cada keystroke)

```csharp
private async Task OnSearchInput(ChangeEventArgs e)
{
    searchText = e.Value?.ToString() ?? "";
    await OnSearchChanged.InvokeAsync(searchText);
}
```

**`ChangeEventArgs`**:
- Contiene información del evento
- `e.Value`: Nuevo valor del input

**`InvokeAsync()`**:
- Invoca el callback del padre
- El padre (`Tickets.razor`) recibe el nuevo texto de búsqueda

### Botones de Filtro:

```razor
<button
    class="filter-btn @(selectedFilter == "Abierto" ? "active" : "")"
    @onclick="@(() => SelectFilter("Abierto"))">
    Abiertos
</button>
```

**Clases Dinámicas**:
```razor
class="filter-btn @(selectedFilter == "Abierto" ? "active" : "")"
```
- Si `selectedFilter == "Abierto"`, añade clase `active`
- Permite resaltar el botón activo con CSS

**Lambda en `@onclick`**:
```razor
@onclick="@(() => SelectFilter("Abierto"))"
```
- Lambda que llama al método con parámetro
- Sin lambda, no podríamos pasar el parámetro

---

## 🎫 12. Components/Tickets/TicketList.razor - Lista de Tickets

**Ubicación**: `Components/Tickets/TicketList.razor`

### Parámetro de Entrada:

```csharp
[Parameter]
public List<Ticket> Tickets { get; set; } = new();

[Parameter]
public EventCallback<int> OnTicketSelected { get; set; }
```

**Flujo de Datos**:
- Padre → Hijo: `Tickets` (lista de tickets a mostrar)
- Hijo → Padre: `OnTicketSelected` (ID del ticket clickeado)

### Iteración con `@foreach`:

```razor
@foreach (var ticket in Tickets)
{
    <div class="ticket-card" @onclick="() => SelectTicket(ticket.Id)">
        <h3>@ticket.Title</h3>
        <p>@TruncateDescription(ticket.Description)</p>
        <span class="ticket-status status-@GetStatusClass(ticket.Status)">
            @ticket.Status
        </span>
    </div>
}
```

**`@foreach`**:
- Itera sobre la lista de tickets
- Genera HTML dinámicamente para cada ticket

**Métodos Helper**:

```csharp
private string TruncateDescription(string description)
{
    if (description.Length <= 100)
        return description;
    return description.Substring(0, 100) + "...";
}
```
- Limita la descripción a 100 caracteres
- Añade "..." si es más larga
- Mejor UX: no muestra texto infinito

```csharp
private string GetStatusClass(string status)
{
    return status switch
    {
        "Abierto" => "open",
        "En progreso" => "progress",
        "Cerrado" => "closed",
        _ => "open"
    };
}
```
- Mapea estado a clase CSS
- Permite colorear badges según el estado
- `status-open`, `status-progress`, `status-closed`

```csharp
private string FormatDate(DateTime date)
{
    var timeSpan = DateTime.Now - date;
    if (timeSpan.TotalDays < 1)
        return $"Hace {(int)timeSpan.TotalHours}h";
    if (timeSpan.TotalDays < 7)
        return $"Hace {(int)timeSpan.TotalDays}d";
    return date.ToString("dd/MM/yyyy");
}
```
- Formato de fecha "inteligente"
- Reciente: "Hace 2h", "Hace 3d"
- Antiguo: "15/10/2025"
- Mejor UX: información relativa

---

## 🎫 13. Components/Tickets/TicketDetail.razor - Modal de Detalle

**Ubicación**: `Components/Tickets/TicketDetail.razor`

### Estructura del Modal:

```razor
<div class="modal-overlay" @onclick="Close">
    <div class="ticket-detail-panel" @onclick:stopPropagation="true">
        <!-- Contenido del modal -->
    </div>
</div>
```

**`@onclick:stopPropagation="true"`**: ⭐ IMPORTANTE
- Evita que el clic en el panel cierre el modal
- Solo hacer clic FUERA del panel (overlay) cierra el modal
- Sin esto, cualquier clic cerraría el modal

### Parámetros:

```csharp
[Parameter]
public Ticket Ticket { get; set; } = null!;

[Parameter]
public EventCallback OnClose { get; set; }

[Parameter]
public EventCallback<(int id, string status)> OnStatusChanged { get; set; }
```

**`null!`**:
- Null-forgiving operator
- Le dice al compilador: "Confía en mí, no será null"
- El padre SIEMPRE pasa un Ticket válido

**Tuple en EventCallback**:
```csharp
EventCallback<(int id, string status)>
```
- Pasa múltiples valores al padre
- Sintaxis moderna de C# (tuples)

### Botones de Cambio de Estado:

```razor
<button
    class="status-btn status-btn-open @(Ticket.Status == "Abierto" ? "active" : "")"
    @onclick="@(() => ChangeStatus("Abierto"))"
    disabled="@(Ticket.Status == "Abierto" || isUpdating)">
    Abierto
</button>
```

**Disabled Dinámico**:
```razor
disabled="@(Ticket.Status == "Abierto" || isUpdating)"
```
- Deshabilita si ya está en ese estado
- Deshabilita durante la actualización (evita doble clic)

### Lógica de Cambio de Estado:

```csharp
private async Task ChangeStatus(string newStatus)
{
    if (isUpdating || Ticket.Status == newStatus)
        return;

    isUpdating = true;
    await OnStatusChanged.InvokeAsync((Ticket.Id, newStatus));
    isUpdating = false;
}
```

**Guard Clauses**:
- Primera línea verifica precondiciones
- Si ya está actualizando o el estado es el mismo, sale
- **Defensive Programming**

**Estado `isUpdating`**:
- Muestra mensaje "Actualizando estado..."
- Deshabilita todos los botones
- Evita múltiples clics simultáneos

---

# PARTE 8: FLUJO DE DATOS COMPLETO

## 🔄 14. Flujo de Ejecución: Cambio de Estado

Vamos a seguir el flujo completo cuando el usuario cambia el estado de un ticket:

### Paso 1: Usuario Hace Clic en Botón (TicketDetail.razor)

```razor
<button @onclick="@(() => ChangeStatus("En progreso"))">
    En Progreso
</button>
```

1. Usuario hace clic
2. Blazor Server captura el evento vía SignalR
3. Ejecuta `ChangeStatus("En progreso")` en el servidor

### Paso 2: Método `ChangeStatus` (TicketDetail.razor)

```csharp
private async Task ChangeStatus(string newStatus)
{
    isUpdating = true; // ✓ Muestra "Actualizando..."
    await OnStatusChanged.InvokeAsync((Ticket.Id, newStatus));
    isUpdating = false; // ✓ Oculta "Actualizando..."
}
```

3. Invoca el callback del padre (Tickets.razor)
4. Pasa el ID del ticket y el nuevo estado

### Paso 3: Callback en Padre (Tickets.razor)

```csharp
private async Task HandleStatusChanged((int id, string status) data)
{
    var success = await TicketService.UpdateTicketStatusAsync(data.id, data.status);
    if (success)
    {
        await LoadTickets(); // Recarga todos los tickets
        selectedTicket = await TicketService.GetTicketByIdAsync(data.id); // Actualiza el modal
    }
}
```

5. Llama al servicio para actualizar en la fuente de datos
6. Si tiene éxito, recarga la lista completa
7. Actualiza el ticket seleccionado (para refrescar el modal)

### Paso 4: Servicio Actualiza Datos (TicketService.cs)

```csharp
public async Task<bool> UpdateTicketStatusAsync(int id, string newStatus)
{
    await Task.Delay(400); // Simula latencia de red

    var ticket = _tickets.FirstOrDefault(t => t.Id == id);
    if (ticket == null) return false;

    ticket.Status = newStatus;
    _logger.LogInformation($"Ticket {id} actualizado a estado: {newStatus}");
    return true;
}
```

8. Simula 400ms de latencia (realista)
9. Encuentra el ticket en memoria
10. Actualiza su estado
11. Registra el cambio en logs

### Paso 5: Blazor Re-renderiza la UI

```csharp
await LoadTickets();
```

12. Recarga `allTickets` desde el servicio
13. `ApplyFilters()` recalcula `filteredTickets`
14. Blazor detecta cambios de estado
15. Envía solo los CAMBIOS al cliente via SignalR (diff)
16. El navegador actualiza el DOM

**Resultado Visual**:
- ✅ Badge en el modal cambia de color
- ✅ Badge en la lista cambia de color
- ✅ Estadísticas se actualizan
- ✅ Filtros siguen funcionando
- ✅ Todo sin recargar la página

### Diagrama del Flujo:

```
Usuario Clic
    ↓
TicketDetail.razor (@onclick)
    ↓
ChangeStatus()
    ↓
OnStatusChanged.InvokeAsync()
    ↓
Tickets.razor (HandleStatusChanged)
    ↓
TicketService.UpdateTicketStatusAsync()
    ↓
Actualiza datos en memoria
    ↓
LoadTickets() recarga lista
    ↓
StateHasChanged() (automático)
    ↓
Blazor calcula diff
    ↓
SignalR envía cambios al cliente
    ↓
DOM actualizado
```

---

# PARTE 9: CARACTERÍSTICAS TÉCNICAS CLAVE

## ⚡ 15. Blazor Server - Interactividad en Tiempo Real

### ¿Cómo Funciona?

1. **Primera Carga**:
   - El servidor renderiza HTML inicial
   - Se envía al navegador

2. **Conexión SignalR**:
   - JavaScript en el cliente establece conexión WebSocket
   - Mantiene conexión persistente con el servidor

3. **Evento del Usuario**:
   - Usuario hace clic en botón
   - JavaScript captura el evento
   - Envía mensaje al servidor vía SignalR

4. **Procesamiento en Servidor**:
   - Servidor ejecuta el método C#
   - Actualiza el estado del componente

5. **UI Diff**:
   - Blazor calcula diferencias (solo lo que cambió)
   - Envía solo los cambios al cliente

6. **Actualización del DOM**:
   - Cliente aplica los cambios al DOM
   - Todo sin recargar la página

### Ventajas de Blazor Server:

✅ **Código C# en el servidor**: Seguro, no expone lógica al cliente
✅ **Sin JavaScript**: Escribes C#, no JS
✅ **Bajo peso**: Solo se envían cambios, no todo el HTML
✅ **Debugging**: Puedes usar breakpoints en Visual Studio

### Desventajas:

❌ **Requiere conexión constante**: Si se pierde, la app no funciona
❌ **Latencia**: Cada interacción va al servidor (mitigado con SignalR)
❌ **Escalabilidad**: Cada usuario = una conexión activa

---

## 🎨 16. CSS Isolated - Estilos con Scope

### ¿Qué es?

Cada componente puede tener su propio archivo CSS que NO afecta a otros componentes.

**Ejemplo**:
```
TicketList.razor
TicketList.razor.css  ← Estilos solo para TicketList
```

### ¿Cómo Funciona?

1. Blazor genera un identificador único para el componente
2. Añade este ID como atributo a todos los elementos del componente
3. Transforma los selectores CSS para incluir este ID

**Tu CSS**:
```css
.ticket-card {
    background: white;
}
```

**CSS Generado por Blazor**:
```css
.ticket-card[b-abc123] {
    background: white;
}
```

**HTML Generado**:
```html
<div class="ticket-card" b-abc123>
```

### Ventajas:

✅ **No hay conflictos**: Puedes usar clase `.ticket-card` en múltiples componentes
✅ **Mantenibilidad**: Los estilos están junto al componente
✅ **Refactoring**: Puedes mover/eliminar componente con sus estilos

---

## 🔌 17. Inyección de Dependencias (DI)

### Patrón de Diseño

**Problema que Resuelve**:
- Los componentes necesitan servicios (ej: TicketService)
- No queremos crear instancias manualmente (`new TicketService()`)
- Queremos desacoplar componentes de implementaciones concretas

**Solución**:
1. Registrar servicios en `Program.cs`
2. Inyectarlos automáticamente donde se necesiten

### Ciclos de Vida:

**Singleton** (una instancia para toda la app):
```csharp
builder.Services.AddSingleton<ITicketService, TicketService>();
```
- Se crea una vez al iniciar la app
- Todas las peticiones usan la misma instancia
- Perfecto para servicios con estado compartido

**Scoped** (una instancia por petición HTTP):
```csharp
builder.Services.AddScoped<ITicketService, TicketService>();
```
- Se crea una nueva instancia por cada circuito de Blazor
- En Blazor Server, = por cada conexión de usuario

**Transient** (nueva instancia cada vez):
```csharp
builder.Services.AddTransient<ITicketService, TicketService>();
```
- Nueva instancia cada vez que se solicita
- Usa más memoria, pero garantiza aislamiento

### Uso en Componentes:

```razor
@inject ITicketService TicketService

@code {
    // Blazor inyecta automáticamente la instancia
    // No necesitas hacer: var service = new TicketService();
}
```

---

## 📱 18. Diseño Responsivo

### Media Queries en CSS:

```css
/* Desktop (por defecto) */
.stats {
    grid-template-columns: repeat(4, 1fr); /* 4 columnas */
}

/* Tablet */
@media (max-width: 768px) {
    .stats {
        grid-template-columns: repeat(2, 1fr); /* 2 columnas */
    }
}

/* Móvil */
@media (max-width: 480px) {
    .stats {
        grid-template-columns: 1fr; /* 1 columna */
    }
}
```

**Mobile-First vs Desktop-First**:
- Desktop-First: Estilos base para desktop, override para móvil
- Mobile-First: Estilos base para móvil, override para desktop

**Breakpoints Comunes**:
- Móvil: < 480px
- Tablet: 768px - 1024px
- Desktop: > 1024px

---

# PARTE 10: PUNTOS CLAVE PARA LA PRESENTACIÓN

## 🎯 19. Aspectos Técnicos a Destacar

### 1. Arquitectura en Capas

```
┌─────────────────────────────────────┐
│   PRESENTATION (Components)         │
│   - Blazor Razor Components         │
│   - Event Handling                  │
│   - UI State Management             │
├─────────────────────────────────────┤
│   BUSINESS LOGIC (Services)         │
│   - TicketService                   │
│   - Validation Logic                │
│   - Data Transformation             │
├─────────────────────────────────────┤
│   DATA (Models + Data Source)       │
│   - Ticket, TicketStatus            │
│   - tickets.json (Mock API)         │
└─────────────────────────────────────┘
```

**Separación de Responsabilidades**:
- Cada capa tiene un propósito único
- Fácil de mantener y testear
- Cambios en una capa no afectan otras

### 2. Patrones de Diseño Implementados

✅ **Repository Pattern** (TicketService)
- Abstrae el acceso a datos
- Fácil cambiar de JSON a DB

✅ **Dependency Injection**
- Desacoplamiento
- Testabilidad

✅ **Component Pattern** (Blazor)
- Componentes reutilizables
- Composición sobre herencia

✅ **Observer Pattern** (EventCallback)
- Comunicación hijo → padre
- Desacoplamiento de componentes

### 3. Buenas Prácticas de C#

✅ **Async/Await**:
```csharp
public async Task<List<Ticket>> GetAllTicketsAsync()
```
- Operaciones no bloqueantes
- Mejor rendimiento

✅ **Pattern Matching**:
```csharp
return status switch
{
    "Abierto" => TicketStatus.Abierto,
    _ => TicketStatus.Abierto
};
```
- Código más limpio
- Compilador verifica casos

✅ **Nullable Reference Types**:
```csharp
private string? errorMessage;
```
- Evita NullReferenceException
- Compilador ayuda a prevenir bugs

✅ **LINQ**:
```csharp
filteredTickets.Where(t => t.Status == currentFilter)
```
- Consultas declarativas
- Código más legible

### 4. Características de Blazor

✅ **Component Lifecycle**:
- `OnInitializedAsync()`: Carga inicial
- `OnParametersSet()`: Cuando cambian parámetros
- `StateHasChanged()`: Fuerza re-render

✅ **Event Handling**:
- `@onclick`: Click events
- `@oninput`: Input en tiempo real
- `@bind`: Two-way binding

✅ **Conditional Rendering**:
```razor
@if (isLoading) { ... }
else if (errorMessage != null) { ... }
else { ... }
```

✅ **Iteración**:
```razor
@foreach (var ticket in Tickets) { ... }
```

### 5. Simulación de API REST

Endpoints simulados:
- `GET /tickets` → `GetAllTicketsAsync()`
- `GET /tickets/{id}` → `GetTicketByIdAsync(id)`
- `PATCH /tickets/{id}` → `UpdateTicketStatusAsync(id, status)`

Latencia simulada:
- 300ms para GET
- 400ms para PATCH
- Realismo en la demo

---

## 📝 20. Script de Presentación (5 minutos)

### Minuto 1: Introducción y Arquitectura

"He desarrollado un sistema de gestión de tickets en Blazor Server siguiendo principios SOLID y arquitectura en capas.

El punto de entrada es **Program.cs** donde:
1. Configuramos Blazor Server con renderizado interactivo
2. Registramos el servicio de tickets usando Inyección de Dependencias
3. Configuramos el enrutamiento y middleware

La estructura está dividida en:
- **Models**: Definición de datos (Ticket, TicketStatus)
- **Services**: Lógica de negocio (TicketService)
- **Components**: UI interactiva (Pages, Layout, Tickets)

Esto permite separación de responsabilidades y facilita el testing."

### Minuto 2: Modelos y Servicios

"En la capa de **Models**, tenemos:
- **Ticket**: POCO con propiedades y métodos helper
- **TicketStatus**: Enum para type safety

En **Services**:
- **ITicketService**: Interfaz que define el contrato (abstracción)
- **TicketService**: Implementación que simula una API REST
  - Carga datos desde JSON con fallback a datos en memoria
  - Simula latencia de red (300-400ms) para realismo
  - Logging estructurado con ILogger

El servicio está registrado como Singleton, compartido por toda la app."

### Minuto 3: Componentes y Flujo de Datos

"Los componentes siguen un patrón de composición:

- **Tickets.razor**: Componente padre, orquesta todo
  - Mantiene el estado (allTickets, filteredTickets)
  - Recibe eventos de componentes hijos vía EventCallback

- **TicketFilters**: Componente hijo para búsqueda y filtros
  - Input en tiempo real con `@oninput`
  - Notifica al padre cuando cambia algo

- **TicketList**: Muestra tickets con formato
  - Métodos helper para truncar texto, formatear fechas

- **TicketDetail**: Modal lateral
  - Permite cambiar estado
  - Usa `stopPropagation` para evitar cerrar al hacer clic dentro

El flujo es unidireccional: Padre → Hijo (datos), Hijo → Padre (eventos)."

### Minuto 4: Características Técnicas

"Características clave implementadas:

1. **Blazor Server Interactivo**:
   - Renderizado del lado del servidor
   - SignalR para comunicación en tiempo real
   - `@rendermode InteractiveServer` en componentes

2. **CSS Isolated**:
   - Cada componente tiene su CSS
   - Sin conflictos de estilos
   - Blazor genera scopes automáticamente

3. **Responsive Design**:
   - Media queries para móvil, tablet, desktop
   - Grid layout adaptativo
   - Modal full-screen en móvil

4. **UX Improvements**:
   - Estados de carga (spinner)
   - Manejo de errores
   - Formato de fechas relativas
   - Latencia simulada para feedback visual"

### Minuto 5: Demo en Vivo

"Permítanme mostrar la funcionalidad:

1. **Filtros**: [Click "Abiertos"] → 5 tickets sin recargar página
2. **Búsqueda**: [Escribir "sesión"] → Filtrado en tiempo real
3. **Combinación**: [Filtros + búsqueda funcionan juntos]
4. **Detalle**: [Click en ticket] → Modal con animación
5. **PATCH**: [Cambiar estado] → Ver latencia simulada
6. **Persistencia**: [Cerrar y reabrir] → Cambio persiste
7. **Responsive**: [F12, modo móvil] → Layout se adapta

Todo funciona sin recargar la página gracias a Blazor Server."

---

## 🏆 21. Preguntas Frecuentes Anticipadas

### "¿Por qué Blazor Server y no Blazor WebAssembly?"

"Blazor Server permite:
- Código C# seguro en el servidor
- Menos payload inicial (mejor first load)
- Debugging más fácil
- Ideal para intranets con conexión estable

WebAssembly sería mejor para:
- Apps que funcionan offline
- Reducir carga del servidor
- Clientes con conexión intermitente"

### "¿Cómo escalaría esto en producción?"

"Para producción real:
1. **Backend**: API REST real con ASP.NET Core Web API
2. **Base de Datos**: SQL Server o PostgreSQL con Entity Framework Core
3. **Caching**: Redis para mejorar performance
4. **SignalR Backplane**: Azure SignalR Service para múltiples servidores
5. **Autenticación**: Identity con JWT
6. **Testing**: Unit tests con xUnit, Mocks con Moq"

### "¿Cómo manejarías estado más complejo?"

"Opciones:
1. **Fluxor**: State management pattern como Redux
2. **Blazor State**: Librería de manejo de estado
3. **Cascading Parameters**: Para estado compartido entre componentes
4. **Browser Storage**: LocalStorage para persistencia cliente"

### "¿Qué mejoras añadirías?"

"Siguientes pasos:
1. **SignalR Hub**: Actualizaciones en tiempo real entre usuarios
2. **Paginación**: Para grandes volúmenes de tickets
3. **Ordenamiento**: Por fecha, prioridad, estado
4. **Comentarios**: Sistema de conversación en tickets
5. **Adjuntos**: Subida de archivos
6. **Notificaciones**: Push notifications cuando cambia estado
7. **Dashboard**: Gráficos y métricas con Chart.js
8. **Export**: Exportar a PDF/Excel"

---

## 📚 22. Recursos y Referencias

### Tecnologías Utilizadas:

- **.NET 9.0**: Framework principal
- **Blazor Server**: UI framework
- **C# 12**: Lenguaje de programación
- **System.Text.Json**: Serialización
- **SignalR**: Comunicación tiempo real (implícito en Blazor Server)
- **CSS3**: Estilos con Grid y Flexbox

### Patrones y Principios:

- **SOLID Principles**
- **Repository Pattern**
- **Dependency Injection**
- **Component Pattern**
- **Observer Pattern** (EventCallback)

### Buenas Prácticas:

- ✅ Separación de responsabilidades
- ✅ CSS Isolated
- ✅ Async/Await
- ✅ Logging estructurado
- ✅ Manejo de errores
- ✅ Nullable reference types
- ✅ Pattern matching
- ✅ LINQ

---

Espero que esta guía te ayude a presentar tu proyecto de manera profesional. ¡Éxito en tu prueba técnica! 🚀
