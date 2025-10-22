# Guía Rápida de Inicio

## Ejecutar la Aplicación

### Opción 1: Usando dotnet CLI

```bash
cd TicketManagementApp
dotnet run
```

La aplicación estará disponible en:
- HTTPS: `https://localhost:5001/tickets`
- HTTP: `http://localhost:5000/tickets`

### Opción 2: Usando Visual Studio
1. Abrir `TicketManagementApp.csproj`
2. Presionar F5 o hacer clic en el botón "Run"

### Opción 3: Usando VS Code
1. Abrir la carpeta del proyecto
2. Presionar F5
3. Seleccionar ".NET Core" cuando se solicite

## Navegación

La aplicación tiene las siguientes rutas:

- `/` - Página de inicio (Home)
- `/tickets` - Dashboard de tickets (página principal)
- `/counter` - Página de ejemplo Counter
- `/weather` - Página de ejemplo Weather

## Uso de la Aplicación

### Ver Tickets
1. Navega a `/tickets` desde el menú o directamente
2. Verás una lista de todos los tickets con sus estados

### Filtrar Tickets
- Haz clic en los botones "Todos", "Abiertos", "En Progreso" o "Cerrados"
- Los tickets se filtrarán instantáneamente sin recargar la página

### Buscar Tickets
- Escribe en el cuadro de búsqueda en la parte superior
- La búsqueda se realiza en tiempo real sobre título y descripción

### Ver Detalle de Ticket
- Haz clic en cualquier tarjeta de ticket
- Se abrirá un panel lateral con toda la información

### Cambiar Estado
1. Abre el detalle de un ticket
2. Haz clic en uno de los tres botones de estado
3. El estado se actualizará automáticamente

## Estructura del Proyecto

```
TicketManagementApp/
├── Components/
│   ├── Pages/Tickets.razor           # Página principal
│   └── Tickets/                      # Componentes del módulo
│       ├── TicketFilters.razor       # Filtros y búsqueda
│       ├── TicketList.razor          # Lista de tickets
│       └── TicketDetail.razor        # Modal de detalle
├── Models/
│   └── Ticket.cs                     # Modelo de datos
├── Services/
│   └── TicketService.cs              # Lógica de negocio
└── Data/
    └── tickets.json                  # Datos mock
```

## Personalizar los Datos

Puedes editar el archivo `Data/tickets.json` para agregar o modificar tickets:

```json
{
  "id": 11,
  "title": "Tu título aquí",
  "description": "Tu descripción aquí",
  "status": "Abierto",
  "createdAt": "2025-10-22T10:00:00Z"
}
```

Estados válidos: `"Abierto"`, `"En progreso"`, `"Cerrado"`

## Comandos Útiles

### Compilar el proyecto
```bash
dotnet build
```

### Ejecutar en modo watch (auto-recarga)
```bash
dotnet watch run
```

### Limpiar archivos generados
```bash
dotnet clean
```

### Publicar para producción
```bash
dotnet publish -c Release -o ./publish
```

## Solución de Problemas

### Error: No se puede conectar a https://localhost:5001
1. Asegúrate de tener el certificado de desarrollo instalado:
```bash
dotnet dev-certs https --trust
```

### Error: Puerto en uso
- Modifica el puerto en `Properties/launchSettings.json`
- O cierra la aplicación que está usando el puerto

### Los datos no se cargan
- Verifica que el archivo `Data/tickets.json` existe
- Revisa los logs de la consola para ver errores

## Próximos Pasos

1. **Explora el código**: Revisa los componentes en `Components/Tickets/`
2. **Modifica estilos**: Los archivos `.razor.css` contienen los estilos de cada componente
3. **Agrega funcionalidad**: Implementa las extensiones sugeridas en el README.md
4. **Conecta una API real**: Reemplaza `TicketService` con llamadas HTTP reales

## Recursos de Aprendizaje

- [Documentación oficial de Blazor](https://docs.microsoft.com/es-es/aspnet/core/blazor/)
- [Tutorial de Blazor](https://dotnet.microsoft.com/learn/aspnet/blazor-tutorial)
- [Ejemplos de Blazor](https://github.com/aspnet/samples/tree/main/samples/aspnetcore/blazor)
