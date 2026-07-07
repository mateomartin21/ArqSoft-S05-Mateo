# Diagramas de arquitectura C4 — CitasApp

Estos diagramas reflejan el estado real del código de esta rama.

## Nivel 1 — Contexto del sistema

```mermaid
C4Context
    title Nivel 1 - Contexto del sistema (CitasApp)

    Person(personal, "Personal de la clinica", "Recepcionista / staff que registra pacientes, medicos y citas")

    System(citasApp, "CitasApp", "Sistema de gestion de citas medicas (ASP.NET Core MVC / .NET 10)")

    Rel(personal, citasApp, "Usa", "HTTPS")
```

## Nivel 2 — Contenedores

```mermaid
C4Container
    title Nivel 2 - Contenedores (CitasApp)

    Person(personal, "Personal de la clinica", "Recepcionista / staff")

    System_Boundary(citasApp, "CitasApp") {
        Container(web, "CitasApp.Web", "ASP.NET Core MVC (.NET 10)", "Sirve las vistas y controla pacientes, medicos y citas. Program.cs actua como composition root")
        Container(api, "CitasApp.Api", "ASP.NET Core Web API", "Proyecto de API REST dentro de la misma solucion")
        ContainerDb(datos, "Almacenamiento de datos", "Archivos JSON / CSV / SQLite", "El Factory elige Json o Sqlite para Paciente; Csv fijo para Medico y Cita")
    }

    Rel(personal, web, "Usa", "HTTPS")
    Rel(web, datos, "Lee/escribe")
    Rel(api, datos, "Lee/escribe")
```

## Nivel 3 — Componentes dentro de CitasApp.Web

```mermaid
C4Component
    title Nivel 3 - Componentes dentro de CitasApp.Web

    Container_Boundary(web, "CitasApp.Web") {
        Component(citaCtrl, "CitaController", "ASP.NET Core Controller", "Lista, crea, filtra y elimina citas")
        Component(pacienteCtrl, "PacienteController", "ASP.NET Core Controller", "Lista y muestra detalle de pacientes")
        Component(medicoCtrl, "MedicoController", "ASP.NET Core Controller", "Lista y muestra detalle de medicos")
        Component(program, "Program.cs", "Composition Root", "Configura la inyeccion de dependencias")
    }

    Container_Boundary(domain, "CitasApp.Domain") {
        Component(iPacRepo, "IPacienteRepository", "Interface")
        Component(iMedRepo, "IMedicoRepository", "Interface")
        Component(iCitaRepo, "ICitaRepository", "Interface")
        Component(iObs, "ICitaObserver", "Interface")
    }

    Container_Boundary(app, "CitasApp.Application") {
        Component(services, "Paciente/Medico/CitaService", "Application Services", "Registrados en DI; aun no consumidos por los Controllers MVC")
    }

    Container_Boundary(infra, "CitasApp.Infrastructure") {
        Component(factory, "RepositoryFactory", "Factory Method", "Crea Json o Sqlite segun el entorno")
        Component(logging, "LoggingPacienteRepository", "Decorator", "Agrega logging sobre IPacienteRepository")
        Component(csvMed, "CsvMedicoRepository", "Repository", "Instanciado directo, sin Factory")
        Component(csvCita, "CsvCitaRepository", "Repository", "Instanciado directo, sin Factory")
        Component(emailObs, "EmailObserver", "Observer", "Implementa ICitaObserver; aun sin conectar a un subject")
        Component(smsObs, "SmsObserver", "Observer", "Implementa ICitaObserver; aun sin conectar a un subject")
    }

    Rel(citaCtrl, iCitaRepo, "Usa")
    Rel(pacienteCtrl, iPacRepo, "Usa")
    Rel(medicoCtrl, iMedRepo, "Usa")

    Rel(program, factory, "Configura (Bloque B activo)")
    Rel(program, logging, "Envuelve resultado del Factory")
    Rel(program, csvMed, "Instancia directamente")
    Rel(program, csvCita, "Instancia directamente")
    Rel(program, services, "Registra en DI (AddScoped)")

    Rel(factory, iPacRepo, "Retorna implementacion de")
    Rel(logging, iPacRepo, "Implementa")
    Rel(csvMed, iMedRepo, "Implementa")
    Rel(csvCita, iCitaRepo, "Implementa")
    Rel(emailObs, iObs, "Implementa")
    Rel(smsObs, iObs, "Implementa")
```
