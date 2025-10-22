# Lista de Verificación - Prueba Técnica Blazor

## ✅ Checklist de Funcionalidades

### 1. UI / Maquetación

#### 1.1 Interfaz tipo dashboard
- [ ] ¿Hay una barra de navegación visible?
- [ ] ¿El diseño se ve limpio y profesional?
- [ ] ¿Hay una jerarquía clara de información?
- [ ] ¿Los colores y espaciados son consistentes?

**Cómo verificar:**
- Navega a `/tickets`
- Observa el diseño general del dashboard
- ¿Se ve profesional? ¿Los elementos están bien organizados?

#### 1.2 Diseño Responsivo
- [ ] **Desktop**: Layout con múltiples columnas
- [ ] **Tablet** (768px): Cards se adaptan a 2 columnas
- [ ] **Móvil** (480px): Cards en una sola columna
- [ ] Modal de detalle ocupa toda la pantalla en móvil

**Cómo verificar:**
1. Presiona `F12` en el navegador (DevTools)
2. Haz clic en el ícono de dispositivo móvil (Toggle Device Toolbar)
3. Prueba estos tamaños:
   - Desktop: 1920x1080
   - Tablet: 768x1024
   - Móvil: 375x667 (iPhone)

#### 1.3 Accesibilidad Básica
- [ ] Los botones tienen texto descriptivo
- [ ] Los inputs tienen placeholders
- [ ] Los colores tienen buen contraste
- [ ] Los estados están claramente identificados

---

### 2. Lista de Tickets

#### 2.1 Visualización
- [ ] Se muestran 10 tickets al cargar la página
- [ ] Cada ticket muestra:
  - [ ] Título
  - [ ] Descripción (truncada a 100 caracteres)
  - [ ] Estado (badge con color)
  - [ ] ID del ticket
  - [ ] Fecha (formato relativo: "Hace Xh" o "Hace Xd")

**Cómo verificar:**
- Cuenta los tickets mostrados (deben ser 10)
- Verifica que cada card tiene toda la información

#### 2.2 Estadísticas
- [ ] Card "Total" muestra 10
- [ ] Card "Abiertos" muestra número correcto
- [ ] Card "En Progreso" muestra número correcto
- [ ] Card "Cerrados" muestra número correcto
- [ ] Los números suman 10 en total

**Cómo verificar:**
- Observa los 4 cards en la parte superior
- Suma manualmente: Abiertos + En Progreso + Cerrados = Total

---

### 3. Filtrado por Estado

#### 3.1 Filtros Funcionales
- [ ] Botón "Todos" muestra los 10 tickets
- [ ] Botón "Abiertos" filtra solo tickets abiertos
- [ ] Botón "En Progreso" filtra solo tickets en progreso
- [ ] Botón "Cerrados" filtra solo tickets cerrados
- [ ] El botón activo se resalta visualmente
- [ ] **NO recarga la página** (sin parpadeo)

**Cómo verificar:**
1. Haz clic en "Abiertos" → Cuenta los tickets mostrados
2. Haz clic en "En Progreso" → Cuenta los tickets mostrados
3. Haz clic en "Cerrados" → Cuenta los tickets mostrados
4. Haz clic en "Todos" → Deben aparecer los 10 tickets
5. Observa que NO hay recarga de página (sin parpadeo blanco)

**Datos esperados del JSON:**
- Abiertos: Tickets #1, #3, #6, #9, #10 (5 tickets)
- En Progreso: Tickets #2, #4, #8 (3 tickets)
- Cerrados: Tickets #5, #7 (2 tickets)

---

### 4. Búsqueda

#### 4.1 Búsqueda en Tiempo Real
- [ ] Al escribir, los tickets se filtran inmediatamente
- [ ] Busca en el título del ticket
- [ ] Busca en la descripción del ticket
- [ ] Es case-insensitive (no importan mayúsculas)
- [ ] Funciona en combinación con filtros de estado

**Cómo verificar:**
1. Escribe "sesión" en el buscador
   - Debe mostrar el ticket #1: "No puedo iniciar sesión"
2. Borra y escribe "error"
   - Debe mostrar tickets que contengan "error" en título o descripción
3. Borra y escribe "app"
   - Debe mostrar tickets con "app" o "aplicación"
4. Prueba combinación:
   - Filtra por "Abiertos"
   - Busca "no" en el buscador
   - Deben aparecer solo tickets abiertos que contengan "no"

---

### 5. Panel de Detalle

#### 5.1 Apertura del Modal
- [ ] Al hacer clic en una card de ticket, se abre un panel lateral
- [ ] El panel aparece desde la derecha con animación
- [ ] Se muestra un overlay oscuro detrás
- [ ] El modal NO recarga la página

**Cómo verificar:**
1. Haz clic en cualquier ticket
2. Observa la animación de entrada (slide-in)
3. Verifica que hay un fondo oscuro (overlay)

#### 5.2 Contenido del Detalle
- [ ] Muestra el ID del ticket
- [ ] Muestra el título completo
- [ ] Muestra la descripción completa (sin truncar)
- [ ] Muestra el estado actual
- [ ] Muestra la fecha de creación (formato: dd/MM/yyyy HH:mm)
- [ ] Muestra 3 botones para cambiar estado

**Cómo verificar:**
1. Abre el ticket #1
2. Verifica que se muestra toda la información
3. Compara con `Data/tickets.json` para confirmar exactitud

#### 5.3 Cierre del Modal
- [ ] Botón "✕" cierra el modal
- [ ] Hacer clic fuera del panel (en el overlay) cierra el modal
- [ ] El modal se cierra con animación

**Cómo verificar:**
1. Abre un ticket
2. Haz clic en el botón "✕" → Se debe cerrar
3. Abre otro ticket
4. Haz clic en el fondo oscuro → Se debe cerrar

---

### 6. Cambio de Estado (PATCH)

#### 6.1 Actualización de Estado
- [ ] Los 3 botones de estado están visibles
- [ ] El botón del estado actual está deshabilitado y resaltado
- [ ] Al hacer clic en otro estado:
  - [ ] Aparece mensaje "Actualizando estado..."
  - [ ] El estado se actualiza después de ~400ms (latencia simulada)
  - [ ] El badge de estado cambia en el detalle
  - [ ] El badge cambia en la lista de tickets
  - [ ] Las estadísticas se actualizan
  - [ ] Los botones se deshabilitan durante la actualización

**Cómo verificar:**
1. Abre el ticket #1 (Estado: "Abierto")
2. Haz clic en "En Progreso"
3. Observa:
   - Mensaje "Actualizando estado..." aparece
   - Después de ~400ms, el estado cambia
   - El badge en el modal cambia de color
4. Cierra el modal
5. Verifica en la lista que el ticket #1 ahora dice "En Progreso"
6. Verifica que las estadísticas se actualizaron:
   - Abiertos: ahora debería ser 4 (antes era 5)
   - En Progreso: ahora debería ser 4 (antes era 3)

#### 6.2 Persistencia del Cambio
- [ ] Si cambias el estado y cierras el modal
- [ ] Al reabrir el mismo ticket, el nuevo estado persiste
- [ ] Los filtros funcionan con el nuevo estado

**Cómo verificar:**
1. Cambia el ticket #1 a "Cerrado"
2. Cierra el modal
3. Haz clic en filtro "Cerrados"
4. El ticket #1 debe aparecer en la lista de cerrados
5. Abre el ticket #1 de nuevo
6. Confirma que el estado sigue siendo "Cerrado"

---

### 7. Diseño Responsivo Detallado

#### 7.1 Desktop (> 768px)
- [ ] Stats cards en 4 columnas
- [ ] Tickets en lista vertical con cards amplias
- [ ] Modal de detalle ocupa ~500px de ancho a la derecha

#### 7.2 Tablet (768px)
- [ ] Stats cards en 2 columnas (2x2)
- [ ] Filtros se adaptan a 2 filas
- [ ] Modal de detalle ocupa más espacio (90vw)

#### 7.3 Móvil (< 480px)
- [ ] Stats cards en 1 columna (4 cards apiladas)
- [ ] Filtros en 2 columnas
- [ ] Modal ocupa toda la pantalla (100vw)
- [ ] Botones de estado en columna (no fila)

**Cómo verificar:**
1. Abre DevTools (F12)
2. Activa modo responsive
3. Prueba cada tamaño mencionado
4. Abre un ticket en cada tamaño para ver el modal

---

### 8. Estados de Carga y Errores

#### 8.1 Estado de Carga Inicial
- [ ] Al cargar la página, se muestra un spinner
- [ ] Aparece el texto "Cargando tickets..."
- [ ] Dura aproximadamente 300ms (latencia simulada)

**Cómo verificar:**
1. Refresca la página (F5)
2. Observa el spinner y mensaje de carga
3. Debe desaparecer cuando los tickets se cargan

#### 8.2 Estado de Actualización
- [ ] Al cambiar estado de un ticket:
  - Los botones se deshabilitan
  - Aparece texto "Actualizando estado..."
  - Dura ~400ms

**Cómo verificar:**
1. Abre cualquier ticket
2. Cambia su estado
3. Observa el comportamiento descrito

#### 8.3 Estado Vacío
- [ ] Si filtras por un estado sin tickets:
  - Aparece ícono de documento
  - Mensaje "No hay tickets"
  - Texto explicativo

**Cómo verificar:**
1. Cambia todos los tickets a "Cerrado"
2. Filtra por "Abiertos"
3. Debe aparecer el estado vacío

---

### 9. Estructura del Código

#### 9.1 Organización de Carpetas
- [ ] `Models/` contiene Ticket.cs y TicketStatus.cs
- [ ] `Services/` contiene ITicketService.cs y TicketService.cs
- [ ] `Components/Pages/` contiene Tickets.razor
- [ ] `Components/Tickets/` contiene componentes reutilizables
- [ ] `Data/` contiene tickets.json

#### 9.2 Separación de Lógica
- [ ] Los componentes .razor NO tienen lógica de negocio compleja
- [ ] El servicio TicketService maneja toda la lógica de datos
- [ ] Los modelos están correctamente tipados
- [ ] Se usa inyección de dependencias (@inject)

#### 9.3 CSS Isolated
- [ ] Cada componente tiene su .razor.css correspondiente
- [ ] Los estilos NO se filtran entre componentes
- [ ] No hay estilos inline (style="...")

---

### 10. Funcionalidades Adicionales Implementadas

#### 10.1 Animaciones
- [ ] Modal: slide-in desde la derecha
- [ ] Overlay: fade-in
- [ ] Cards: hover con elevación
- [ ] Botones: transiciones suaves

#### 10.2 UX Improvements
- [ ] Formato de fecha relativo ("Hace 2h", "Hace 5d")
- [ ] Truncado de descripciones largas (...)
- [ ] Badges de estado con colores distintivos
- [ ] Stats cards con hover effect

---

## 📊 Resumen de Verificación

### Checklist Rápido
```
[ ] ✅ UI limpia y profesional
[ ] ✅ Lista de 10 tickets se muestra
[ ] ✅ Filtrado por estado funciona
[ ] ✅ Búsqueda en tiempo real funciona
[ ] ✅ Panel de detalle se abre/cierra
[ ] ✅ Cambio de estado funciona (PATCH)
[ ] ✅ Diseño responsivo (3 tamaños probados)
[ ] ✅ Estados de carga/error funcionan
[ ] ✅ Estructura de código organizada
[ ] ✅ Buenas prácticas implementadas
```

---

## 🎯 Casos de Prueba Específicos

### Caso 1: Flujo Completo de Usuario
1. Carga la página → Ver spinner de carga
2. Ver todos los tickets (10)
3. Filtrar por "Abiertos" → Ver 5 tickets
4. Buscar "sesión" → Ver 1 ticket
5. Hacer clic en el ticket → Abrir modal
6. Cambiar estado a "En Progreso" → Ver actualización
7. Cerrar modal
8. Verificar que el filtro y stats se actualizaron

### Caso 2: Responsividad
1. Desktop (1920px) → Todo en vista
2. Tablet (768px) → Adaptación de columnas
3. Móvil (375px) → Stack vertical
4. Abrir modal en cada tamaño → Verificar adaptación

### Caso 3: Combinación de Filtros
1. Filtrar por "En Progreso"
2. Buscar "pago"
3. Debe mostrar solo ticket #4
4. Cambiar a "Cerrado"
5. Buscar "búsqueda"
6. Debe mostrar solo ticket #7

---

## 🐛 Problemas Potenciales a Verificar

- [ ] ¿Los filtros persisten al recargar? (NO deberían, es comportamiento correcto)
- [ ] ¿Se pueden abrir múltiples modales? (NO, solo uno a la vez)
- [ ] ¿El overlay bloquea la interacción? (SÍ, debe bloquear)
- [ ] ¿Los cambios de estado son inmediatos? (NO, hay ~400ms de latencia simulada)
- [ ] ¿Se puede hacer clic rápidamente múltiples veces en botones? (NO, deben deshabilitarse)

---

## ✨ Puntos Extra Implementados

- ✅ Estadísticas en tiempo real (cards con contadores)
- ✅ Búsqueda combinada con filtros
- ✅ Animaciones suaves y profesionales
- ✅ Formato de fecha inteligente
- ✅ Truncado de texto largo
- ✅ Logging en consola (para debugging)
- ✅ Latencia simulada (experiencia realista)
- ✅ Estados visuales claros (loading, empty, error)

---

## 📝 Notas para la Presentación

**Destacar en la presentación:**
1. Arquitectura limpia con separación de responsabilidades
2. Componentes reutilizables y desacoplados
3. CSS Isolated sin dependencias externas
4. Simulación realista de API con latencia
5. Manejo completo de estados (loading, error, empty)
6. Diseño responsivo profesional
7. Buenas prácticas de Blazor (.NET 9.0)

**Si te preguntan sobre mejoras futuras:**
- Implementar API REST real
- Agregar SignalR para tiempo real
- Sistema de notificaciones
- Paginación para grandes volúmenes
- Autenticación y autorización
- Unit tests e integration tests
