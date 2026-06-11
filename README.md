# CitasApp — Sistema de Gestión de Citas Médicas

**Mateo Martin**  
Tecnológico de Software · TSU en Desarrollo e Innovación de Software  
3er Cuatrimestre · Arquitectura de Software

---

## Descripción

CitasApp es una aplicación web para la gestión de citas médicas de una clínica. Permite registrar pacientes, médicos y citas, con funcionalidades para crear, listar, filtrar y eliminar registros. Los datos se persisten en archivos JSON locales.

Este repositorio contiene la rama `hexagonal`, que refactoriza la aplicación original (arquitectura de una sola capa) hacia una **arquitectura hexagonal multi-proyecto** (Ports & Adapters), separando el dominio, la infraestructura y la capa web en proyectos independientes.

---

## Estructura del proyecto

```
CitasApp/
├── CitasApp.slnx                         # Solución Visual Studio
│
├── CitasApp.Domain/                      # Núcleo de negocio — no depende de nadie
│   ├── Models/
│   │   ├── Cita.cs
│   │   ├── CitaJson.cs
│   │   ├── Medico.cs
│   │   └── Paciente.cs
│   └── Interfaces/                       # Ports de salida
│       ├── ICitaRepository.cs
│       ├── IMedicoRepository.cs
│       └── IPacienteRepository.cs
│
├── CitasApp.Infrastructure/              # Adapters de salida — depende de Domain
│   └── Repositories/
│       ├── JsonCitaRepository.cs
│       ├── JsonMedicoRepository.cs
│       └── JsonPacienteRepository.cs
│
└── CitasApp.Web/                         # Adapter de entrada — depende de Domain e Infrastructure
    ├── Controllers/
    │   ├── CitaController.cs
    │   ├── HomeController.cs
    │   ├── MedicoController.cs
    │   └── PacienteController.cs
    ├── Views/
    ├── data/
    │   ├── citas.json
    │   ├── medicos.json
    │   └── pacientes.json
    └── Program.cs
```

---

## Arquitectura

Este proyecto implementa el estilo **Hexagonal (Ports & Adapters)**, propuesto por Alistair Cockburn. La idea central es aislar el núcleo de negocio de cualquier tecnología externa.

```
Web MVC ──────────▶┌─────────────────────────┐
                   │                         │
API REST ─────────▶│    NÚCLEO DE NEGOCIO    │──▶ JsonCitaRepository
                   │  (Domain + Interfaces)  │
App móvil ────────▶│                         │──▶ JsonMedicoRepository
                   └─────────────────────────┘
        Adapters de entrada        Adapters de salida
```

### Decisión arquitectónica (ADR)

| Sección | Contenido |
|---|---|
| **Contexto** | CitasApp necesita separar la lógica de negocio de la tecnología web para facilitar el cambio de base de datos y la futura adición de una API REST o app móvil. |
| **Decisión** | Arquitectura Hexagonal multi-proyecto con tres capas: Domain, Infrastructure y Web. |
| **Consecuencias positivas** | El núcleo de negocio no cambia al cambiar la tecnología. Se puede agregar un nuevo cliente (app móvil, API) creando solo un nuevo Adapter de entrada. Se puede cambiar de JSON a SQL creando solo un nuevo Adapter de salida. |
| **Consecuencias negativas** | Mayor complejidad inicial de configuración. Requiere gestión de referencias entre proyectos y actualización de namespaces. |

### Referencias entre proyectos

- `CitasApp.Domain` → no referencia a nadie
- `CitasApp.Infrastructure` → referencia a `CitasApp.Domain`
- `CitasApp.Web` → referencia a `CitasApp.Domain` y `CitasApp.Infrastructure`

---

## Tecnologías utilizadas

| Tecnología | Versión | Uso |
|---|---|---|
| .NET | 10.0 | Framework principal |
| ASP.NET Core MVC | 10.0 | Capa de presentación web |
| C# | 13 | Lenguaje de programación |
| JSON (System.Text.Json) | — | Persistencia de datos |
| Visual Studio 2026 | 18.6 | IDE de desarrollo |

---

## Requisitos previos

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022/2026 o VS Code

---

## Instalación y ejecución

1. Clona el repositorio:
```bash
git clone https://github.com/tu-usuario/CitasApp.git
cd CitasApp
git checkout hexagonal
```

2. Restaura las dependencias:
```bash
dotnet restore CitasApp.slnx
```

3. Compila la solución:
```bash
dotnet build CitasApp.Web.csproj
```

4. Ejecuta la aplicación:
```bash
dotnet run --project CitasApp.Web.csproj
```

5. Abre el navegador en `https://localhost:5001`

---

## Funcionalidades

- **Pacientes** — Listar, crear y eliminar pacientes
- **Médicos** — Listar, crear y eliminar médicos  
- **Citas** — Listar todas las citas, crear nueva cita, filtrar por paciente y eliminar

---

## Ramas del repositorio

| Rama | Descripción |
|---|---|
| `main` | Versión original — arquitectura de capas en un solo proyecto |
| `hexagonal` | Refactorización a arquitectura hexagonal multi-proyecto |

---

## Historial de decisiones arquitectónicas

### ADR-01 · Persistencia en JSON
**Decisión:** Usar archivos JSON como mecanismo de persistencia inicial.  
**Justificación:** Simplicidad para el alcance académico del proyecto. Al estar detrás de una interfaz (`ICitaRepository`), puede reemplazarse por una base de datos SQL sin modificar el dominio.

### ADR-02 · Refactorización a Hexagonal
**Decisión:** Migrar de arquitectura de capas a hexagonal multi-proyecto.  
**Justificación:** La arquitectura de capas original no permitía agregar nuevos clientes (app móvil, API REST) sin duplicar la lógica de negocio. La arquitectura hexagonal resuelve esto separando el núcleo de los adapters.

---

## Uso de Inteligencia Artificial

Durante el desarrollo de este proyecto se utilizó **Claude (Anthropic)** como herramienta de asistencia. El uso de IA se limitó a:

- Orientación paso a paso durante la refactorización a arquitectura hexagonal
- Generación y corrección de comandos de PowerShell para automatizar cambios de namespace
- Resolución de errores de compilación (CS0234, CS0579, CS1503, CS0246)
- Redacción de este documento README

Todo el código fue revisado, comprendido y validado por el autor. La lógica de negocio, la estructura del proyecto y las decisiones arquitectónicas son responsabilidad del estudiante. El uso de IA es transparente y se declara en cumplimiento con las políticas académicas del Tecnológico de Software.

---

## Autor

**Mateo Martin**  
Tecnológico de Software  
TSU en Desarrollo e Innovación de Software · Grupo 3A  
Junio 2026
