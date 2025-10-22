# Guía de Verificación - Funcionalidades de la Prueba Técnica

## ✅ Checklist Rápido (5 minutos)

Sigue estos pasos en orden para verificar todas las funcionalidades:

---

### 1. 📊 Verificar Vista Inicial (30 segundos)

**Lo que debes ver:**
```
✓ Título: "Sistema de Tickets de Soporte"
✓ 4 Cards de estadísticas:
  - Total: 10
  - Abiertos: 5
  - En Progreso: 3
  - Cerrados: 2
✓ Barra de búsqueda con placeholder "Buscar tickets..."
✓ 4 botones de filtro: [Todos] [Abiertos] [En Progreso] [Cerrados]
✓ 10 tickets mostrados en formato card
```

**Verifica:**
- [ ] Los números de las estadísticas suman 10
- [ ] Cada ticket muestra: Título, Descripción, Estado (badge), ID, Fecha
- [ ] Las descripciones están truncadas a ~100 caracteres con "..."

---

### 2. 🔍 Probar Filtros (1 minuto)

**Paso a paso:**

1. **Haz clic en "Abiertos"**
   - ✓ El botón se resalta (fondo verde)
   - ✓ Aparecen SOLO 5 tickets
   - ✓ Todos tienen badge verde "Abierto"
   - ✓ La página NO se recarga (sin parpadeo blanco)

2. **Haz clic en "En Progreso"**
   - ✓ El botón se resalta (fondo naranja)
   - ✓ Aparecen SOLO 3 tickets
   - ✓ Todos tienen badge naranja "En progreso"

3. **Haz clic en "Cerrados"**
   - ✓ El botón se resalta (fondo gris)
   - ✓ Aparecen SOLO 2 tickets
   - ✓ Todos tienen badge gris "Cerrado"

4. **Haz clic en "Todos"**
   - ✓ Vuelven a aparecer los 10 tickets
   - ✓ El botón "Todos" se resalta (fondo azul)

**Verifica:**
- [ ] Los filtros funcionan sin recargar la página
- [ ] El botón activo siempre está resaltado
- [ ] Los contadores en las estadísticas NO cambian (siguen mostrando totales)

---

### 3. 🔎 Probar Búsqueda (1 minuto)

**Pruebas específicas:**

1. **Busca "sesión"** (sin hacer clic en nada, solo escribe)
   - ✓ Aparece SOLO 1 ticket: "No puedo iniciar sesión"
   - ✓ Se filtra en tiempo real (mientras escribes)
   - ✓ No necesitas presionar Enter

2. **Borra y busca "error"**
   - ✓ Aparecen múltiples tickets que contienen "error"
   - ✓ Busca en título Y descripción

3. **Busca "ERROR" (mayúsculas)**
   - ✓ Funciona igual (case-insensitive)

4. **Borra el texto**
   - ✓ Vuelven a aparecer todos los tickets

**Combinación de filtros + búsqueda:**

5. **Haz clic en "Abiertos"** → Luego busca "no"
   - ✓ Muestra solo tickets Abiertos que contienen "no"
   - ✓ La combinación funciona correctamente

**Verifica:**
- [ ] Búsqueda en tiempo real (sin botón de buscar)
- [ ] Busca en título y descripción
- [ ] No distingue mayúsculas/minúsculas
- [ ] Se combina con filtros de estado

---

### 4. 📋 Probar Panel de Detalle (1.5 minutos)

**Abrir el modal:**

1. **Haz clic en el primer ticket** ("No puedo iniciar sesión")
   - ✓ Se abre un panel lateral desde la derecha
   - ✓ Hay animación de deslizamiento (slide-in)
   - ✓ Aparece un fondo oscuro (overlay) detrás del panel

**Contenido del detalle:**

2. **Verifica que se muestra:**
   - ✓ ID: #1
   - ✓ Título completo: "No puedo iniciar sesión"
   - ✓ Descripción COMPLETA (sin truncar): "Intento entrar con mi cuenta y no me deja. He probado resetear la contraseña pero sigo sin poder acceder."
   - ✓ Estado actual: badge "Abierto" (verde)
   - ✓ Fecha: "16/10/2025 10:30"
   - ✓ 3 botones de estado:
     - [Abierto] - activo (deshabilitado, resaltado en verde)
     - [En Progreso] - disponible
     - [Cerrado] - disponible

**Cerrar el modal:**

3. **Haz clic en la "✕" (botón de cerrar)**
   - ✓ El modal se cierra
   - ✓ Vuelves a ver la lista de tickets

4. **Abre otro ticket → Haz clic en el fondo oscuro (fuera del panel)**
   - ✓ El modal se cierra

**Verifica:**
- [ ] Modal se abre con animación suave
- [ ] Muestra toda la información del ticket
- [ ] Descripción completa (no truncada)
- [ ] Botón del estado actual está deshabilitado y resaltado
- [ ] Se puede cerrar con "✕" o haciendo clic fuera

---

### 5. ✏️ Probar Cambio de Estado - PATCH (2 minutos)

**Caso de prueba completo:**

1. **Abre el ticket #1** ("No puedo iniciar sesión")
   - Estado actual: "Abierto"

2. **Haz clic en el botón "En Progreso"**
   - ✓ Aparece texto "Actualizando estado..."
   - ✓ Los 3 botones se deshabilitan temporalmente
   - ✓ Después de ~400ms (latencia simulada):
     - Badge cambia a "En progreso" (naranja)
     - Botón "En Progreso" ahora está activo/deshabilitado
     - Botón "Abierto" ahora está habilitado
     - Desaparece el mensaje "Actualizando estado..."

3. **Cierra el modal** (haz clic en ✕)

4. **Verifica en la lista:**
   - ✓ El ticket #1 ahora muestra badge "En progreso" (naranja)
   - ✓ El cambio persiste en la lista

5. **Verifica las estadísticas:**
   - ✓ "Abiertos" ahora dice 4 (antes era 5)
   - ✓ "En Progreso" ahora dice 4 (antes era 3)
   - ✓ Total sigue siendo 10

6. **Vuelve a abrir el ticket #1**
   - ✓ El estado sigue siendo "En progreso"
   - ✓ El cambio es persistente (en memoria)

7. **Cambia a "Cerrado"**
   - ✓ Funciona el mismo proceso
   - ✓ Badge cambia a gris

8. **Filtra por "Cerrados"**
   - ✓ El ticket #1 ahora aparece en la lista de cerrados

**Verifica:**
- [ ] Aparece mensaje "Actualizando estado..." durante ~400ms
- [ ] Los botones se deshabilitan durante la actualización
- [ ] El badge cambia de color en el modal
- [ ] El cambio se refleja en la lista principal
- [ ] Las estadísticas se actualizan correctamente
- [ ] El cambio persiste al reabrir el ticket
- [ ] Los filtros funcionan con el nuevo estado

---

### 6. 📱 Verificar Diseño Responsivo (1 minuto)

**Probar diferentes tamaños:**

1. **Presiona F12** (DevTools)

2. **Haz clic en el ícono de móvil** (Toggle Device Toolbar)

3. **Selecciona "iPhone 12 Pro"** o similar (390x844)
   - ✓ Stats cards se muestran en 2 columnas (2x2)
   - ✓ Filtros se adaptan a 2 filas
   - ✓ Tickets en 1 columna vertical

4. **Abre un ticket en móvil**
   - ✓ El modal ocupa TODA la pantalla
   - ✓ Botones de estado en columna (vertical)
   - ✓ Todo es fácil de tocar

5. **Cambia a "iPad"** (768x1024)
   - ✓ Layout se adapta
   - ✓ Modal más estrecho que en móvil

6. **Cambia a "Desktop" (1920x1080)**
   - ✓ Stats en 4 columnas
   - ✓ Modal de ~500px de ancho a la derecha

**Verifica:**
- [ ] Móvil (< 480px): Todo en 1 columna
- [ ] Tablet (768px): Adaptación intermedia
- [ ] Desktop (> 768px): Layout completo con múltiples columnas
- [ ] Modal se adapta a cada tamaño

---

### 7. 🎨 Verificar Detalles de UX (30 segundos)

**Animaciones y efectos:**

1. **Pasa el mouse sobre las cards de tickets**
   - ✓ La card se eleva ligeramente
   - ✓ Cambia la sombra

2. **Pasa el mouse sobre los botones de filtro**
   - ✓ Cambian de color suavemente

3. **Observa el formato de fechas**
   - Tickets recientes: "Hace Xh" o "Hace Xd"
   - Tickets antiguos: "dd/MM/yyyy"

4. **Verifica los colores de los badges:**
   - Abierto: Verde claro con texto verde oscuro
   - En Progreso: Naranja claro con texto naranja oscuro
   - Cerrado: Gris claro con texto gris oscuro

**Verifica:**
- [ ] Hover effects funcionan
- [ ] Animaciones son suaves (no bruscas)
- [ ] Colores son consistentes
- [ ] Formato de fecha es inteligente

---

## 🎯 Casos de Prueba Avanzados

### Caso 1: Flujo Completo de Usuario
```
1. Carga la página → Ver todos los tickets (10)
2. Filtra por "Abiertos" → 5 tickets
3. Busca "sesión" → 1 ticket
4. Haz clic en ese ticket → Abre modal
5. Cambia estado a "En Progreso" → Actualización con latencia
6. Cierra modal
7. Verifica que stats cambió (Abiertos: 4, En Progreso: 4)
8. Filtra por "En Progreso" → Debe aparecer el ticket
9. Borra búsqueda → Ver todos los tickets en progreso (4)
```

### Caso 2: Cambios Múltiples de Estado
```
1. Abre ticket #3 (Abierto)
2. Cambia a "En Progreso"
3. Cambia a "Cerrado"
4. Cambia de vuelta a "Abierto"
5. Verifica que cada cambio funciona correctamente
```

### Caso 3: Búsqueda y Filtros Combinados
```
1. Filtra por "En Progreso" (3 tickets)
2. Busca "pago"
3. Debe mostrar SOLO ticket #4: "Problema con el pago"
4. Cambia filtro a "Cerrados"
5. Busca "búsqueda"
6. Debe mostrar SOLO ticket #7
```

---

## 📊 Resumen de Números Esperados

**Datos iniciales (tickets.json):**
- Total: 10 tickets
- Abiertos: 5 (IDs: 1, 3, 6, 9, 10)
- En Progreso: 3 (IDs: 2, 4, 8)
- Cerrados: 2 (IDs: 5, 7)

**Después de cambiar ticket #1 a "En Progreso":**
- Total: 10 tickets
- Abiertos: 4 (IDs: 3, 6, 9, 10)
- En Progreso: 4 (IDs: 1, 2, 4, 8)
- Cerrados: 2 (IDs: 5, 7)

---

## 🐛 Cosas que NO Deberían Pasar

- ❌ La página se recarga al filtrar (no debe haber parpadeo blanco)
- ❌ Se pueden abrir múltiples modales a la vez
- ❌ Los cambios de estado no se reflejan en la lista
- ❌ Las estadísticas no se actualizan
- ❌ La búsqueda requiere presionar Enter
- ❌ Los filtros no funcionan después de buscar
- ❌ Hacer clic rápidamente múltiples veces en botones causa errores

---

## ✨ Puntos Fuertes a Destacar en la Presentación

1. **Sin recarga de página**: Todo funciona con Blazor Server interactivo
2. **Latencia simulada**: Experiencia realista de API (~300-400ms)
3. **Estados de carga**: "Actualizando estado..." durante operaciones
4. **Combinación de filtros**: Búsqueda + Estado funcionan juntos
5. **Persistencia en memoria**: Los cambios persisten durante la sesión
6. **Animaciones suaves**: UX profesional
7. **Diseño responsivo**: Funciona en móvil, tablet y desktop
8. **Código limpio**: Separación de responsabilidades, componentes reutilizables

---

## 🏁 Checklist Final

Marca cada elemento después de verificarlo:

```
[ ] ✅ Vista inicial correcta (stats + filtros + 10 tickets)
[ ] ✅ Filtrado por estado funciona (sin recarga)
[ ] ✅ Búsqueda en tiempo real funciona
[ ] ✅ Búsqueda + filtros combinados funciona
[ ] ✅ Modal se abre/cierra correctamente
[ ] ✅ Modal muestra información completa
[ ] ✅ Cambio de estado funciona (PATCH)
[ ] ✅ Cambios se reflejan en lista y stats
[ ] ✅ Cambios persisten al reabrir ticket
[ ] ✅ Diseño responsivo (móvil/tablet/desktop)
[ ] ✅ Animaciones y efectos funcionan
[ ] ✅ No hay errores en consola
```

---

## 📝 Notas

- **Tiempo estimado de verificación completa**: ~7-8 minutos
- **Para demo en vivo**: Sigue el "Flujo Completo de Usuario" (Caso 1)
- **Si encuentras algún problema**: Revisa la consola del navegador (F12) y el Output de Visual Studio

---

**¡Éxito en tu prueba técnica!** 🚀
