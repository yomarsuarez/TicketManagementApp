# Sistema de Gestión de Tickets - Blazor

Aplicación web desarrollada en Blazor Server para la gestión de tickets de soporte técnico. Permite visualizar, filtrar y actualizar el estado de tickets de manera eficiente a través de una interfaz moderna y responsiva.

## Características Principales

- **Dashboard Interactivo**: Vista principal con estadísticas en tiempo real de los tickets
- **Filtrado por Estado**: Filtra tickets por Abierto, En Progreso o Cerrado
- **Búsqueda en Tiempo Real**: Busca tickets por título o descripción
- **Detalle de Ticket**: Panel lateral modal con información completa del ticket
- **Cambio de Estado**: Actualiza el estado del ticket mediante botones interactivos
- **Diseño Responsivo**: Interfaz adaptable a diferentes tamaños de pantalla
- **Simulación de API**: Servicio mock que simula endpoints REST con latencia

## Tecnologías Utilizadas

### Framework y Lenguaje
- **.NET 9.0**: Framework principal
- **Blazor Server**: Renderizado del lado del servidor con interactividad en tiempo real
- **C#**: Lenguaje de programación

### Librerías y Herramientas
- **CSS Puro**: Estilos personalizados sin dependencias externas
- **CSS Isolated**: Estilos con alcance limitado por componente
- **System.Text.Json**: Serialización/deserialización de JSON
- **ILogger**: Sistema de logging integrado

### Características de .NET Utilizadas
- **Dependency Injection**: Inyección de dependencias para servicios
- **Async/Await**: Programación asíncrona
- **LINQ**: Consultas y filtrado de datos
- **Pattern Matching**: Switch expressions
- **EventCallback**: Comunicación entre componentes

## Estructura del Proyecto

```
TicketManagementApp/
├── Components/
│   ├── Layout/
│   │   ├── MainLayout.razor          # Layout principal
│   │   └── NavMenu.razor             # Menú de navegación
│   ├── Pages/
│   │   ├── Tickets.razor             # Página principal del dashboard
│   │   └── Tickets.razor.css         # Estilos de la página
│   └── Tickets/
│       ├── TicketFilters.razor       # Componente de filtros
│       ├── TicketFilters.razor.css   # Estilos de filtros
│       ├── TicketList.razor          # Componente de lista
│       ├── TicketList.razor.css      # Estilos de lista
│       ├── TicketDetail.razor        # Componente de detalle modal
│       └── TicketDetail.razor.css    # Estilos del modal
├── Models/
│   ├── Ticket.cs                     # Modelo de ticket
│   └── TicketStatus.cs               # Enum de estados
├── Services/
│   ├── ITicketService.cs             # Interfaz del servicio
│   └── TicketService.cs              # Implementación del servicio
├── Data/
│   └── tickets.json                  # Datos mock de tickets
├── Program.cs                        # Configuración de la app
└── README.md                         # Este archivo
```

## Arquitectura y Buenas Prácticas

### Separación de Responsabilidades
- **Models**: Definen la estructura de datos
- **Services**: Lógica de negocio y acceso a datos
- **Components**: Lógica de presentación e interactividad
- **CSS Isolated**: Estilos con alcance limitado por componente

### Patrones Implementados
- **Dependency Injection**: Los servicios se registran e inyectan
- **Repository Pattern**: El servicio abstrae el acceso a datos
- **Component Pattern**: Componentes reutilizables y desacoplados
- **Event-Driven**: Comunicación entre componentes mediante callbacks

### Principios SOLID
- **Single Responsibility**: Cada clase tiene una responsabilidad única
- **Open/Closed**: Abierto a extensión, cerrado a modificación
- **Dependency Inversion**: Dependencia de abstracciones (ITicketService)

## Endpoints Simulados

El servicio `TicketService` simula los siguientes endpoints:

### GET /tickets
Obtiene todos los tickets
```csharp
Task<List<Ticket>> GetAllTicketsAsync()
```

### GET /tickets/{id}
Obtiene un ticket específico por ID
```csharp
Task<Ticket?> GetTicketByIdAsync(int id)
```

### PATCH /tickets/{id}
Actualiza el estado de un ticket
```csharp
Task<bool> UpdateTicketStatusAsync(int id, string newStatus)
```

## Modelo de Datos

### Ticket
```csharp
public class Ticket
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

### Estados Válidos
- **Abierto**: Ticket recién creado o reabierto
- **En progreso**: Ticket en proceso de resolución
- **Cerrado**: Ticket resuelto

## Instalación y Ejecución

### Prerrequisitos
- .NET 9.0 SDK o superior
- Visual Studio 2022, VS Code o cualquier editor compatible

### Pasos para Ejecutar

1. **Clonar el repositorio**
```bash
git clone <url-del-repositorio>
cd TicketManagementApp
```

2. **Restaurar dependencias**
```bash
dotnet restore
```

3. **Ejecutar la aplicación**
```bash
dotnet run
```

4. **Abrir en el navegador**
```
https://localhost:5001/tickets
```

### Compilar para Producción
```bash
dotnet publish -c Release -o ./publish
```

## Funcionalidades Implementadas

### ✅ Requisitos Obligatorios
- [x] Dashboard con diseño limpio y moderno
- [x] Lista de tickets con información relevante
- [x] Filtrado por estado (sin recarga de página)
- [x] Búsqueda por título y descripción
- [x] Panel lateral modal para ver detalle
- [x] Cambio de estado mediante botones
- [x] Consumo de API mock con HttpClient simulado
- [x] Manejo de errores y estados de carga
- [x] Diseño responsivo para móviles y tablets
- [x] Estructura de carpetas organizada
- [x] Tipado correcto de modelos
- [x] Separación de lógica UI/negocio
- [x] Uso de inyección de dependencias

### 🎨 Características de UI/UX
- Indicadores visuales de estado con colores
- Animaciones suaves en transiciones
- Estados de carga con spinner
- Mensajes de error informativos
- Estadísticas en tiempo real (cards con contadores)
- Truncado inteligente de descripciones largas
- Formato de fechas relativas (hace Xh, Xd)

### 🔧 Características Técnicas
- Componentes Blazor reutilizables
- CSS Isolated para evitar conflictos de estilos
- Programación asíncrona con async/await
- LINQ para filtrado y búsqueda eficiente
- Logging integrado para debugging
- Simulación de latencia de red

## Posibles Mejoras Futuras

### Extensiones Opcionales Sugeridas
- [ ] **SignalR**: Actualizaciones en tiempo real entre múltiples usuarios
- [ ] **Notificaciones**: Sistema de notificaciones al cambiar estados
- [ ] **Paginación**: Para manejar grandes volúmenes de tickets
- [ ] **Ordenamiento**: Por fecha, prioridad, etc.
- [ ] **Asignación**: Asignar tickets a usuarios específicos
- [ ] **Comentarios**: Añadir comentarios a los tickets
- [ ] **Historial**: Ver cambios históricos del ticket
- [ ] **Adjuntos**: Subir archivos relacionados al ticket

### Mejoras de Arquitectura
- [ ] API REST real con ASP.NET Core Web API
- [ ] Base de datos (SQL Server, PostgreSQL)
- [ ] Entity Framework Core para ORM
- [ ] Autenticación y autorización
- [ ] Unit tests con xUnit o NUnit
- [ ] Integration tests para componentes
- [ ] Docker containerization

## Capturas de Pantalla

### Vista Principal - Dashboard
- Muestra todos los tickets con sus estados
- Cards con estadísticas de tickets por estado
- Barra de búsqueda y filtros interactivos

### Panel de Detalle
- Modal lateral deslizable desde la derecha
- Información completa del ticket
- Botones para cambiar el estado
- Animaciones suaves de entrada/salida

### Vista Móvil
- Layout adaptado para pantallas pequeñas
- Menú de navegación colapsable
- Cards apiladas verticalmente
- Modal de detalle en pantalla completa

## Contacto y Soporte

Para preguntas o sugerencias sobre este proyecto, por favor abre un issue en el repositorio.

---

**Desarrollado como prueba técnica para demostrar conocimientos en Blazor, C# y buenas prácticas de desarrollo web.**
