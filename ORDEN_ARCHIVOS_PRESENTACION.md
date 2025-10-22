# 🎯 Orden de Archivos para Presentación Técnica

## Guía Rápida: Revisa los Archivos en Este Orden

Esta es la secuencia EXACTA en la que debes abrir y explicar los archivos durante tu presentación.

---

## 📋 CHECKLIST DE PRESENTACIÓN (10 minutos)

### ⏱️ PARTE 1: CONFIGURACIÓN (2 minutos)

#### 1. `Program.cs` ⭐ PUNTO DE ENTRADA
```
Ubicación: TicketManagementApp/Program.cs
```

**Qué explicar:**
- ✅ "Este es el punto de entrada de la aplicación"
- ✅ Configuración de Blazor Server (línea 6-7)
- ✅ Registro de DI: `AddSingleton<ITicketService, TicketService>()` (línea 11)
- ✅ Configuración de componentes interactivos (línea 25-26)

**Conceptos clave:**
- Dependency Injection
- Blazor Server vs WebAssembly
- Singleton lifecycle

---

### ⏱️ PARTE 2: MODELOS (1 minuto)

#### 2. `Models/TicketStatus.cs`
```
Ubicación: TicketManagementApp/Models/TicketStatus.cs
```

**Qué explicar:**
- ✅ Enum para type safety
- ✅ Solo 3 estados válidos: Abierto, EnProgreso, Cerrado

#### 3. `Models/Ticket.cs`
```
Ubicación: TicketManagementApp/Models/Ticket.cs
```

**Qué explicar:**
- ✅ POCO (Plain Old CLR Object)
- ✅ `[JsonPropertyName]` para mapear JSON
- ✅ Métodos helper: `GetTicketStatus()`, `SetTicketStatus()`
- ✅ Nullable reference types: `= string.Empty`

**Conceptos clave:**
- Data Transfer Objects (DTO)
- Serialización JSON
- Pattern matching en switch expressions

---

### ⏱️ PARTE 3: SERVICIOS (2 minutos)

#### 4. `Services/ITicketService.cs`
```
Ubicación: TicketManagementApp/Services/ITicketService.cs
```

**Qué explicar:**
- ✅ "Interfaz que define el contrato del servicio"
- ✅ 3 métodos que simulan una API REST:
  - `GetAllTicketsAsync()` → GET /tickets
  - `GetTicketByIdAsync(id)` → GET /tickets/{id}
  - `UpdateTicketStatusAsync(id, status)` → PATCH /tickets/{id}

#### 5. `Services/TicketService.cs` ⭐ LÓGICA DE NEGOCIO
```
Ubicación: TicketManagementApp/Services/TicketService.cs
```

**Qué explicar:**
- ✅ Constructor con DI (líneas 12-18):
  - `IWebHostEnvironment`: Info del entorno
  - `ILogger`: Logging estructurado
- ✅ Método `LoadTickets()` (líneas 20-43):
  - Carga desde JSON
  - Fallback a datos en memoria si falla
- ✅ `GetAllTicketsAsync()` (líneas 63-68):
  - **Simula latencia de 300ms** con `Task.Delay(300)`
  - Retorna copia con `.ToList()` (defensive programming)
- ✅ `UpdateTicketStatusAsync()` (líneas 77-99):
  - **Simula latencia de 400ms**
  - Validaciones de ticket existente y estado válido
  - Logging del cambio

**Conceptos clave:**
- Repository Pattern
- Async/Await
- Defensive Programming
- Logging

---

### ⏱️ PARTE 4: LAYOUT (1 minuto)

#### 6. `Components/Layout/MainLayout.razor`
```
Ubicación: TicketManagementApp/Components/Layout/MainLayout.razor
```

**Qué explicar:**
- ✅ Layout base para todas las páginas
- ✅ `@Body`: Placeholder donde se insertan las páginas
- ✅ Estructura: Sidebar + Main content

#### 7. `Components/Layout/NavMenu.razor`
```
Ubicación: TicketManagementApp/Components/Layout/NavMenu.razor
```

**Qué explicar:**
- ✅ Componente `<NavLink>`: Navegación de Blazor
- ✅ Clase `active` automática
- ✅ 2 enlaces: Home y Tickets

---

### ⏱️ PARTE 5: PÁGINAS (1 minuto)

#### 8. `Components/Pages/Home.razor`
```
Ubicación: TicketManagementApp/Components/Pages/Home.razor
```

**Qué explicar:**
- ✅ `@page "/"`: Define la ruta
- ✅ `@inject NavigationManager`: Para navegar programáticamente
- ✅ `@rendermode InteractiveServer`: ⭐ CRÍTICO para interactividad
- ✅ Botón con `@onclick` que navega a /tickets

#### 9. `Components/Pages/Tickets.razor` ⭐ COMPONENTE PRINCIPAL
```
Ubicación: TicketManagementApp/Components/Pages/Tickets.razor
```

**Qué explicar:**
- ✅ `@inject ITicketService`: Inyección del servicio
- ✅ Variables de estado (líneas 75-81):
  - `allTickets`: Lista original
  - `filteredTickets`: Lista después de filtros
  - `selectedTicket`: Ticket en el modal
  - `isLoading`: Control de spinner
- ✅ `OnInitializedAsync()` (líneas 83-94):
  - Ciclo de vida del componente
  - Carga inicial de datos
  - Manejo de errores
- ✅ `ApplyFilters()` (líneas 118-134):
  - Filtrado por estado
  - Búsqueda por texto (case-insensitive)
  - LINQ para filtrar
- ✅ `HandleStatusChanged()` (líneas 146-154):
  - Llama al servicio
  - Recarga datos
  - Actualiza modal

**Conceptos clave:**
- Component Lifecycle
- State Management
- Event Handling
- LINQ

---

### ⏱️ PARTE 6: COMPONENTES ESPECÍFICOS (3 minutos)

#### 10. `Components/Tickets/TicketFilters.razor`
```
Ubicación: TicketManagementApp/Components/Tickets/TicketFilters.razor
```

**Qué explicar:**
- ✅ `[Parameter]`: Props del componente
- ✅ `EventCallback`: Comunicación hijo → padre
- ✅ Input con `@oninput` (línea 9):
  - **No usa `@bind`** porque queremos tiempo real
  - Se ejecuta en cada keystroke
- ✅ Botones con lambda (línea 17):
  - `@onclick="@(() => SelectFilter("Abierto"))"`
  - Pasa parámetro al método
- ✅ Clases dinámicas (línea 16):
  - `class="filter-btn @(selectedFilter == "Todos" ? "active" : "")"`
  - Resalta botón activo

**Conceptos clave:**
- Component Parameters
- EventCallback
- Tiempo real vs @bind

#### 11. `Components/Tickets/TicketList.razor`
```
Ubicación: TicketManagementApp/Components/Tickets/TicketList.razor
```

**Qué explicar:**
- ✅ `@foreach` (línea 15): Itera sobre tickets
- ✅ Métodos helper:
  - `TruncateDescription()`: Limita a 100 chars
  - `GetStatusClass()`: Mapea estado a clase CSS
  - `FormatDate()`: "Hace 2h" vs "15/10/2025"
- ✅ Clases dinámicas: `status-@GetStatusClass(ticket.Status)`

**Conceptos clave:**
- Iteration en Blazor
- Helper methods
- UX improvements

#### 12. `Components/Tickets/TicketDetail.razor` ⭐ MODAL
```
Ubicación: TicketManagementApp/Components/Tickets/TicketDetail.razor
```

**Qué explicar:**
- ✅ Estructura modal (líneas 3-6):
  - `modal-overlay`: Fondo oscuro
  - `@onclick="Close"`: Cerrar al hacer clic en overlay
  - `@onclick:stopPropagation="true"`: ⭐ IMPORTANTE
    - Evita que clic en panel cierre el modal
- ✅ Parámetros (líneas 70-77):
  - `Ticket`: Datos a mostrar
  - `OnClose`: Callback para cerrar
  - `OnStatusChanged`: Callback con tuple `(int id, string status)`
- ✅ `ChangeStatus()` (líneas 86-93):
  - Guard clause: Evita doble clic
  - `isUpdating = true`: Deshabilita botones
  - Invoca callback del padre
- ✅ Botones deshabilitados dinámicamente (línea 39):
  - `disabled="@(Ticket.Status == "Abierto" || isUpdating)"`

**Conceptos clave:**
- Modal patterns
- stopPropagation
- Tuples en C#
- Guard clauses

---

### ⏱️ PARTE 7: ESTILOS (30 segundos)

#### 13. Cualquier archivo `.razor.css`
```
Ejemplo: TicketManagementApp/Components/Tickets/TicketList.razor.css
```

**Qué explicar:**
- ✅ CSS Isolated: Estilos solo para este componente
- ✅ Blazor genera scopes automáticos
- ✅ Sin conflictos de nombres

---

## 🎬 DEMO EN VIVO (2 minutos)

### Orden de Demostración:

1. **Carga inicial** (5 seg)
   - "Ven el spinner por 300ms"
   - "Se cargan 10 tickets"

2. **Filtros** (20 seg)
   - Clic "Abiertos" → 5 tickets
   - "Sin recarga de página"
   - Clic "En Progreso" → 3 tickets

3. **Búsqueda** (20 seg)
   - Escribir "sesión"
   - "Búsqueda en tiempo real"
   - "Aparece 1 ticket"

4. **Combinación** (15 seg)
   - Filtro "Abiertos" + buscar "no"
   - "Ambos filtros trabajan juntos"

5. **Detalle y PATCH** (40 seg)
   - Abrir ticket #1
   - "Modal con animación desde la derecha"
   - Cambiar a "En Progreso"
   - "Ven mensaje 'Actualizando...' por 400ms"
   - Badge cambia a naranja
   - Cerrar modal
   - "Badge en lista también cambió"
   - "Stats actualizadas: Abiertos 4, En Progreso 4"

6. **Responsive** (20 seg)
   - F12 → Toggle Device Toolbar
   - "iPhone 12 Pro"
   - "Todo se adapta"
   - Abrir ticket
   - "Modal full-screen en móvil"

---

## 📊 FLUJO DE DATOS A EXPLICAR

### Cuando usuario cambia estado:

```
1. Usuario clic → TicketDetail.razor
   ↓
2. @onclick ejecuta ChangeStatus()
   ↓
3. OnStatusChanged.InvokeAsync()
   ↓
4. Tickets.razor recibe evento
   ↓
5. HandleStatusChanged() llama al servicio
   ↓
6. TicketService.UpdateTicketStatusAsync()
   ↓
7. Simula 400ms de latencia
   ↓
8. Actualiza ticket en memoria
   ↓
9. LoadTickets() recarga lista
   ↓
10. ApplyFilters() recalcula filtrados
   ↓
11. StateHasChanged() automático
   ↓
12. Blazor calcula diff
   ↓
13. SignalR envía cambios al cliente
   ↓
14. DOM actualizado (sin recarga de página)
```

**"Todo esto pasa en ~400ms gracias a Blazor Server y SignalR"**

---

## 🎯 CONCEPTOS TÉCNICOS CLAVE

### Para mencionar durante la presentación:

1. **Blazor Server**
   - "Renderizado del lado del servidor"
   - "SignalR para comunicación en tiempo real"
   - "Solo se envían cambios, no todo el HTML"

2. **Dependency Injection**
   - "Registramos servicios en Program.cs"
   - "Se inyectan automáticamente donde se necesitan"
   - "Desacoplamiento y testabilidad"

3. **CSS Isolated**
   - "Cada componente tiene su CSS"
   - "Blazor genera scopes únicos"
   - "Sin conflictos de estilos"

4. **Async/Await**
   - "Todas las operaciones I/O son asíncronas"
   - "No bloquean el thread"
   - "Mejor rendimiento"

5. **LINQ**
   - "Consultas declarativas sobre colecciones"
   - "Código más limpio y legible"
   - `.Where()`, `.Contains()`, `.FirstOrDefault()`

6. **Pattern Matching**
   - "Switch expressions modernos"
   - "Compilador verifica todos los casos"
   - "Código más conciso"

---

## 💡 TIPS PARA LA PRESENTACIÓN

### DO ✅

- Habla despacio y claro
- Usa los términos técnicos correctos
- Explica el "por qué" no solo el "qué"
- Muestra el código mientras hablas
- Ejecuta la demo en vivo
- Anticipa preguntas

### DON'T ❌

- No leas línea por línea
- No asumas que conocen Blazor
- No te saltes el flujo lógico
- No olvides mencionar buenas prácticas
- No pases demasiado tiempo en detalles menores

---

## 📝 SCRIPT DE 30 SEGUNDOS (Elevator Pitch)

"Desarrollé un sistema de gestión de tickets usando Blazor Server con arquitectura en capas:

- **Models** definen la estructura de datos
- **Services** contienen toda la lógica de negocio y simulan una API REST
- **Components** manejan la UI de forma interactiva sin JavaScript

Implementé inyección de dependencias, CSS isolated, y patrones como Repository.

La app permite filtrar por estado, buscar en tiempo real, y actualizar tickets con feedback visual de 400ms simulando latencia de red.

Todo funciona sin recargar la página gracias a SignalR."

---

## 🏆 PREGUNTAS ESPERADAS Y RESPUESTAS

### "¿Por qué Blazor Server?"
"Permite mantener lógica sensible en el servidor, debugging más fácil en C#, y menor payload inicial comparado con WebAssembly."

### "¿Cómo escalarías esto?"
"En producción usaría API REST real, base de datos SQL con EF Core, Redis para caché, Azure SignalR Service para backplane, y autenticación con Identity."

### "¿Por qué Singleton en el servicio?"
"Como simulo una base de datos en memoria, necesito que todos los usuarios vean los mismos datos. En producción con API real, usaría Scoped."

### "¿Qué mejorarías?"
"Añadiría paginación, SignalR Hub para tiempo real entre usuarios, comentarios en tickets, y tests unitarios con xUnit."

---

## ✅ CHECKLIST PRE-PRESENTACIÓN

```
[ ] App corriendo sin errores
[ ] Todos los filtros funcionan
[ ] Búsqueda funciona en tiempo real
[ ] Modal se abre/cierra correctamente
[ ] Cambio de estado funciona con latencia visible
[ ] Stats se actualizan al cambiar estado
[ ] Responsive funciona (probado con F12)
[ ] No hay errores en consola (F12)
[ ] Visual Studio Output sin errores
[ ] README.md actualizado
[ ] Git commits limpios con buenos mensajes
```

---

## 🚀 ¡Éxito en tu Presentación!

**Recuerda**:
- Conoces el código mejor que nadie porque lo construiste
- Practica la demo 2-3 veces antes
- Respira hondo antes de empezar
- Si te preguntan algo que no sabes, di "Gran pregunta, lo investigaría así..."

**¡Vas a hacerlo increíble!** 💪
