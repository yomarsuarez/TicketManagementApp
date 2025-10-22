# Sistema de Gestión de Tickets - Blazor Server

Prueba técnica desarrollada en Blazor Server para la gestión de tickets de soporte técnico.

---

## 📋 Descripción de la Solución

### Resumen
Aplicación web interactiva que permite al equipo de soporte visualizar, filtrar, buscar y actualizar el estado de tickets de manera eficiente a través de una interfaz moderna y responsiva, sin recargar la página.

### Características Implementadas

#### ✅ Funcionalidades Obligatorias
- **Vista de Lista de Tickets**: Muestra 10 tickets de soporte con título, descripción, estado, ID y fecha
- **Filtrado por Estado**: Permite filtrar tickets por "Abierto", "En Progreso" o "Cerrado" sin recargar la página
- **Búsqueda en Tiempo Real**: Busca tickets por título o descripción mientras se escribe
- **Panel de Detalle**: Modal lateral que muestra información completa del ticket al hacer clic
- **Cambio de Estado (PATCH)**: Actualiza el estado del ticket con simulación de latencia de red (~400ms)
- **Dashboard con Estadísticas**: Cards que muestran contadores en tiempo real (Total, Abiertos, En Progreso, Cerrados)
- **Diseño Responsivo**: Adaptable a móviles, tablets y desktop

#### 🎨 Características Adicionales de UI/UX
- **Indicadores Visuales**: Badges de colores según el estado del ticket
- **Estados de Carga**: Spinner durante la carga inicial y feedback "Actualizando estado..."
- **Animaciones Suaves**: Transiciones en modal y hover effects
- **Formato de Fechas Inteligente**: Muestra fechas relativas ("Hace 2h", "Hace 3d") para tickets recientes
- **Truncado de Texto**: Descripciones limitadas a 100 caracteres en la lista con "..."
- **Empty State**: Mensaje cuando no hay tickets que mostrar con los filtros aplicados

### Arquitectura y Patrones

#### Estructura del Proyecto
```
TicketManagementApp/
├── Program.cs                        # Punto de entrada, configuración DI
├── Components/
│   ├── Layout/
│   │   ├── MainLayout.razor          # Layout principal de la app
│   │   └── NavMenu.razor             # Menú de navegación
│   ├── Pages/
│   │   ├── Home.razor                # Página de inicio
│   │   └── Tickets.razor             # Dashboard principal de tickets
│   └── Tickets/                      # Componentes reutilizables
│       ├── TicketFilters.razor       # Búsqueda y filtros
│       ├── TicketList.razor          # Lista de tickets
│       └── TicketDetail.razor        # Modal de detalle
├── Models/
│   ├── Ticket.cs                     # Modelo de datos del ticket
│   └── TicketStatus.cs               # Enum de estados
├── Services/
│   ├── ITicketService.cs             # Interfaz del servicio
│   └── TicketService.cs              # Implementación con API mock
└── Data/
    └── tickets.json                  # 10 tickets de ejemplo
```

#### Separación de Responsabilidades
- **Models**: Definición de estructuras de datos (POCOs)
- **Services**: Lógica de negocio y simulación de API REST
- **Components**: Lógica de presentación e interactividad de la UI

#### Patrones de Diseño
- **Dependency Injection**: Servicios registrados en `Program.cs` e inyectados en componentes
- **Repository Pattern**: `TicketService` abstrae el acceso a datos
- **Component Pattern**: Componentes reutilizables y desacoplados (Filters, List, Detail)
- **Observer Pattern**: Comunicación hijo → padre mediante `EventCallback`

#### Principios SOLID
- **Single Responsibility**: Cada componente/clase tiene una única responsabilidad
- **Open/Closed**: Abierto a extensión (ej: cambiar JSON por API real)
- **Dependency Inversion**: Componentes dependen de `ITicketService` (abstracción), no de implementación concreta

### Endpoints Simulados

El servicio `TicketService.cs` simula una API REST con los siguientes endpoints:

| Método | Endpoint | Descripción | Latencia Simulada |
|--------|----------|-------------|-------------------|
| **GET** | `/tickets` | Obtiene todos los tickets | 300ms |
| **GET** | `/tickets/{id}` | Obtiene un ticket por ID | 200ms |
| **PATCH** | `/tickets/{id}` | Actualiza el estado de un ticket | 400ms |

**Implementación**:
```csharp
// Ejemplo de método con latencia simulada
public async Task<bool> UpdateTicketStatusAsync(int id, string newStatus)
{
    await Task.Delay(400); // Simula latencia de red
    var ticket = _tickets.FirstOrDefault(t => t.Id == id);
    if (ticket == null) return false;

    ticket.Status = newStatus;
    return true;
}
```

### Modelo de Datos

#### Ticket
```csharp
public class Ticket
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }  // "Abierto", "En progreso", "Cerrado"
    public DateTime CreatedAt { get; set; }
}
```

#### Estados Válidos
- **Abierto**: Ticket recién creado o reabierto
- **En progreso**: Ticket en proceso de resolución
- **Cerrado**: Ticket resuelto

---

## 📦 Librerías Adicionales Utilizadas

### Framework Principal
- **.NET 9.0 SDK** - Framework de desarrollo
- **Blazor Server** - Framework de UI con renderizado del lado del servidor

### Librerías Incluidas en .NET (Sin dependencias externas)
- **System.Text.Json** - Serialización/deserialización de JSON para `tickets.json`
- **Microsoft.AspNetCore.Components** - Componentes Razor interactivos
- **Microsoft.Extensions.Logging** - Sistema de logging integrado
- **Microsoft.Extensions.DependencyInjection** - Inyección de dependencias

### Estilos
- **CSS Puro** - Sin frameworks CSS (Bootstrap solo para iconos base)
- **CSS Isolated** - Scoped styles por componente (`*.razor.css`)

### Características de C# Utilizadas
- **Async/Await** - Todas las operaciones I/O son asíncronas
- **LINQ** - Filtrado y consultas sobre colecciones
- **Pattern Matching** - Switch expressions en métodos helper
- **Nullable Reference Types** - Prevención de `NullReferenceException`
- **EventCallback<T>** - Comunicación entre componentes Blazor

**Nota**: El proyecto NO utiliza librerías externas de terceros como MudBlazor, Radzen o Tailwind. Todo está implementado con CSS personalizado.

---

## 🚀 Instrucciones de Instalación y Ejecución

### Prerrequisitos
- **.NET 9.0 SDK** o superior ([Descargar aquí](https://dotnet.microsoft.com/download))
- Editor de código: **Visual Studio 2022**, **VS Code** o **Rider**

### Pasos para Ejecutar

1. **Clonar el repositorio**
   ```bash
   git clone https://github.com/TU_USUARIO/TicketManagementApp.git
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
   - La aplicación se ejecutará en: `https://localhost:5001` o `http://localhost:5000`
   - Navega a: `https://localhost:5001/tickets` para ver el dashboard

### Comandos Útiles

```bash
# Compilar el proyecto
dotnet build

# Ejecutar con auto-recarga (hot reload)
dotnet watch run

# Publicar para producción
dotnet publish -c Release -o ./publish

# Limpiar archivos generados
dotnet clean
```

---

## 🎯 Cómo Usar la Aplicación

### 1. Dashboard Principal
Al iniciar, verás:
- **4 Cards de estadísticas**: Total, Abiertos, En Progreso, Cerrados
- **Barra de búsqueda**: Escribe para filtrar en tiempo real
- **Botones de filtro**: Filtra por estado
- **10 tickets**: Listados en formato card

### 2. Filtrar Tickets
- Haz clic en **"Abiertos"** → Muestra solo tickets con estado "Abierto" (5 tickets)
- Haz clic en **"En Progreso"** → Muestra solo tickets en progreso (3 tickets)
- Haz clic en **"Cerrados"** → Muestra solo tickets cerrados (2 tickets)
- Haz clic en **"Todos"** → Muestra todos los tickets (10 tickets)

**Nota**: Los filtros funcionan sin recargar la página.

### 3. Buscar Tickets
- Escribe en el cuadro de búsqueda (ej: "sesión")
- Los resultados se filtran en tiempo real mientras escribes
- La búsqueda funciona en **título** y **descripción**
- Es **case-insensitive** (no distingue mayúsculas/minúsculas)

### 4. Ver Detalle de Ticket
- Haz clic en cualquier tarjeta de ticket
- Se abre un **modal lateral** desde la derecha con animación
- Muestra información completa: ID, título, descripción completa, estado, fecha

### 5. Cambiar Estado de Ticket (PATCH)
1. Abre el detalle de un ticket
2. Haz clic en uno de los 3 botones de estado:
   - **Abierto** (verde)
   - **En Progreso** (naranja)
   - **Cerrado** (gris)
3. Verás el mensaje **"Actualizando estado..."**
4. Después de ~400ms, el estado se actualiza
5. El badge cambia de color tanto en el modal como en la lista
6. Las estadísticas se actualizan automáticamente

### 6. Responsive Design
- **Desktop** (> 1024px): Layout completo con múltiples columnas
- **Tablet** (768px - 1024px): Adaptación intermedia
- **Móvil** (< 768px): Vista apilada, modal full-screen

Prueba presionando `F12` → Toggle Device Toolbar → Selecciona "iPhone 12 Pro"

---

## 📸 Capturas de Pantalla

### Vista Principal - Dashboard
![Dashboard Principal](docs/screenshots/dashboard.png)
*Dashboard con lista de tickets, estadísticas, búsqueda y filtros*

### Filtrado por Estado
![Filtrado por Estado](docs/screenshots/filters.png)
*Filtros funcionando sin recargar la página*

### Panel de Detalle
![Panel de Detalle](docs/screenshots/detail-modal.png)
*Modal lateral con información completa del ticket y botones de cambio de estado*

### Cambio de Estado
![Cambio de Estado](docs/screenshots/updating-status.png)
*Feedback visual durante la actualización del estado (~400ms de latencia)*

### Vista Móvil
![Vista Móvil](docs/screenshots/mobile-responsive.png)
*Diseño adaptado para dispositivos móviles*

---

## ✅ Requisitos Cumplidos

### UI / Maquetación
- [x] Interfaz tipo dashboard con barra lateral
- [x] Diseño limpio y moderno con CSS personalizado
- [x] Responsiva (móvil, tablet, desktop)
- [x] Coherencia visual y jerarquía clara

### Integración con Endpoints
- [x] GET `/tickets` - Obtiene todos los tickets
- [x] GET `/tickets/{id}` - Obtiene ticket por ID
- [x] PATCH `/tickets/{id}` - Actualiza estado del ticket
- [x] Correcto consumo con métodos asíncronos
- [x] Manejo de errores con try-catch
- [x] Simulación de latencia de red

### Funcionalidad Requerida
- [x] Mostrar lista de tickets con toda la información
- [x] Filtrar por estado sin recargar página
- [x] Mostrar detalle al hacer clic en ticket
- [x] Cambiar estado desde el detalle (PATCH simulado)
- [x] **Búsqueda por título** (funcionalidad opcional implementada)

### Buenas Prácticas
- [x] Estructura de carpetas organizada (Components/, Pages/, Services/, Models/)
- [x] Tipado correcto de modelos con propiedades y métodos
- [x] Separación de lógica UI (Components) vs lógica de negocio (Services)
- [x] Uso correcto de `@inject` para inyección de dependencias
- [x] Nombrado coherente en inglés y español
- [x] Patrón de estado con variables privadas en componentes

---

## 🔧 Decisiones Técnicas

### ¿Por qué Blazor Server?
- **Código C# en el servidor**: Mayor seguridad, lógica de negocio no expuesta al cliente
- **Menos payload inicial**: Mejor rendimiento en first load comparado con WebAssembly
- **Debugging más fácil**: Breakpoints funcionan directamente en Visual Studio
- **SignalR integrado**: Comunicación en tiempo real sin configuración adicional

### ¿Por qué CSS Isolated?
- **Sin conflictos**: Estilos con scope automático por componente
- **Mantenibilidad**: Estilos junto al componente que los usa
- **Refactoring**: Fácil mover/eliminar componentes con sus estilos

### ¿Por qué Singleton para TicketService?
- Simula una base de datos compartida en memoria
- Todos los usuarios ven los mismos cambios
- En producción con API real, se usaría `Scoped` o `Transient`

---

## 🚧 Mejoras Futuras

### Extensiones Sugeridas (Opcional en la Prueba)
- [ ] **SignalR Hub**: Actualizaciones en tiempo real entre múltiples usuarios conectados
- [ ] **Notificaciones Push**: Sistema de notificaciones cuando cambia el estado
- [ ] **Paginación**: Para manejar grandes volúmenes de tickets (100+)
- [ ] **Ordenamiento**: Por fecha, prioridad, asignado, etc.
- [ ] **Comentarios**: Sistema de conversación en cada ticket
- [ ] **Adjuntos**: Subida de archivos PDF/imágenes

### Arquitectura para Producción
- [ ] API REST real con ASP.NET Core Web API
- [ ] Base de datos SQL Server o PostgreSQL
- [ ] Entity Framework Core para ORM
- [ ] Autenticación con ASP.NET Core Identity
- [ ] Unit Tests con xUnit y Moq
- [ ] Integration Tests con WebApplicationFactory
- [ ] CI/CD con GitHub Actions
- [ ] Docker containerization

---

## 📚 Documentación Adicional

Este repositorio incluye documentación técnica detallada:

- **`ARQUITECTURA_TECNICA.md`**: Explicación profunda de la arquitectura, patrones y flujo de datos
- **`ORDEN_ARCHIVOS_PRESENTACION.md`**: Guía para presentar el proyecto técnicamente
- **`QUICK_START.md`**: Guía rápida de inicio y comandos útiles
- **`GUIA_VERIFICACION.md`**: Checklist completo para verificar funcionalidades

---

## 👨‍💻 Autor

**Desarrollado por**: [Tu Nombre]
**Fecha**: Octubre 2025
**Prueba Técnica**: Blazor - Sistema de Gestión de Tickets

---

## 📄 Licencia

Este proyecto fue desarrollado como parte de una prueba técnica y es de uso educativo.

---

**¿Preguntas o sugerencias?** Abre un issue en este repositorio.
