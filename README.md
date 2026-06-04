# CitasApp 🏥
 
Aplicación web para la gestión de citas médicas, desarrollada con **ASP.NET Core MVC** y almacenamiento en archivos JSON. Permite administrar pacientes, médicos y citas de forma sencilla y sin necesidad de base de datos.
 
---
 
## Tecnologías
 
- **ASP.NET Core MVC** (.NET 8)
- **C#**
- **Bootstrap 5**
- Almacenamiento en **JSON** (sin base de datos)
---
 
## Estructura del proyecto
 
```
CitasApp/
├── Controllers/        # Lógica de cada módulo (Cita, Médico, Paciente)
├── Interfaces/         # Contratos de los repositorios
├── Models/             # Modelos de dominio y de serialización JSON
├── Repositories/       # Implementaciones que leen y escriben JSON
├── Views/              # Vistas Razor por módulo
├── data/               # Archivos JSON (citas.json, medicos.json, pacientes.json)
└── wwwroot/            # Archivos estáticos (CSS, JS, Bootstrap)
```
 
---
 
## Funcionalidades
 
| Módulo | Funcionalidades |
|---|---|
| **Citas** | Listar todas las citas, filtrar por paciente, registrar nueva cita, eliminar cita |
| **Médicos** | Listar médicos, ver detalle |
| **Pacientes** | Listar pacientes, ver detalle |
 
---
 
## Cómo ejecutar
 
**Requisitos:** .NET 8 SDK
 
```bash
# Clonar el repositorio
git clone https://github.com/tu-usuario/CitasApp.git
cd CitasApp
 
# Ejecutar la aplicación
dotnet run
```
 
La app estará disponible en `https://localhost:5001` (o el puerto que indique la consola).
 
> **Nota:** El archivo `data/citas.json` debe ser un array plano `[{...}, {...}]`. Si fue generado con una versión anterior del proyecto, asegúrate de corregir el anidamiento.
 
---
 
## Arquitectura
 
El proyecto sigue el patrón **Repository + Interfaces** para desacoplar la fuente de datos de la lógica de negocio. Esto permite reemplazar el almacenamiento JSON por una base de datos real en el futuro sin modificar los controladores ni las vistas.
 
```
Controller → Interface → Repository (JSON)
```
 
La inyección de dependencias está configurada en `Program.cs`.
 
---
 
## Datos de prueba
 
Los archivos en `/data` incluyen registros de ejemplo para médicos, pacientes y citas, listos para usar al levantar la aplicación por primera vez.
 
---
 
## Cláusula de uso de Inteligencia Artificial
 
Parte del código y la documentación de este proyecto fueron desarrollados con asistencia de herramientas de inteligencia artificial (Claude, de Anthropic). El uso de estas herramientas se limitó a apoyo en la generación de código, estructura de archivos y redacción técnica. Todo el contenido fue revisado, adaptado y validado por el autor del proyecto, quien asume la responsabilidad sobre el resultado final.
 
