using CitasApp.Application.Services;
using CitasApp.Controllers;
using CitasApp.Domain.Interfaces;
using CitasApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CitasApp.Tests.Controllers
{
    // --------------------------------------------------------------------
    // Fakes en memoria — mismos Ports que usa el proyecto real,
    // pero con datos controlados para la prueba.
    // --------------------------------------------------------------------
    public class CitaRepositoryFake : ICitaRepository
    {
        private readonly List<Cita> _citas;

        public CitaRepositoryFake(List<Cita> citas) => _citas = citas;

        public List<Cita> ObtenerTodos() => _citas;

        public List<Cita> ObtenerPorPaciente(int pacienteId)
            => _citas.Where(c => c.PacienteId == pacienteId).ToList();

        public void Agregar(Cita cita) => _citas.Add(cita);

        public void Eliminar(int id)
        {
            var cita = _citas.FirstOrDefault(c => c.Id == id);
            if (cita != null) _citas.Remove(cita);
        }
    }

    public class PacienteRepositoryFake : IPacienteRepository
    {
        private readonly List<Paciente> _pacientes;

        public PacienteRepositoryFake(List<Paciente> pacientes) => _pacientes = pacientes;

        public List<Paciente> ObtenerTodos() => _pacientes;

        public Paciente? ObtenerPorId(int id) => _pacientes.FirstOrDefault(p => p.Id == id);
    }

    public class MedicoRepositoryFake : IMedicoRepository
    {
        private readonly List<Medico> _medicos;

        public MedicoRepositoryFake(List<Medico> medicos) => _medicos = medicos;

        public List<Medico> ObtenerTodos() => _medicos;

        public Medico? ObtenerPorId(int id) => _medicos.FirstOrDefault(m => m.Id == id);
    }

    // --------------------------------------------------------------------
    // Pruebas de CitaController
    // --------------------------------------------------------------------
    public class CitaControllerTests
    {
        private CitaController CrearControllerConDatosDePrueba(out List<Cita> citasEsperadas)
        {
            citasEsperadas = new List<Cita>
            {
                new Cita { Id = 1, PacienteId = 10, MedicoId = 1, Estado = "Pendiente" },
                new Cita { Id = 2, PacienteId = 20, MedicoId = 1, Estado = "Confirmada" },
                new Cita { Id = 3, PacienteId = 10, MedicoId = 1, Estado = "Pendiente" }
            };

            var pacientes = new List<Paciente>
            {
                new Paciente { Id = 10, Nombre = "Ana", Apellido = "Lopez", Email = "paciente1@correo.com" },
                new Paciente { Id = 20, Nombre = "Luis", Apellido = "Diaz", Email = "paciente2@correo.com" }
            };

            var medicos = new List<Medico>
            {
                new Medico { Id = 1, Nombre = "Jorge", Apellido = "Perez" }
            };

            var citaRepo = new CitaRepositoryFake(citasEsperadas);
            var pacienteRepo = new PacienteRepositoryFake(pacientes);
            var medicoRepo = new MedicoRepositoryFake(medicos);

            return new CitaController(citaRepo, pacienteRepo, medicoRepo);
        }

        [Fact]
        public void Index_RegresaTodasLasCitas()
        {
            var controller = CrearControllerConDatosDePrueba(out var citasEsperadas);

            var resultado = controller.Index() as ViewResult;
            var modelo = resultado?.Model as List<Cita>;

            Assert.NotNull(modelo);
            Assert.Equal(citasEsperadas.Count, modelo.Count);
            Assert.Equal(citasEsperadas, modelo);
        }

        [Fact]
        public void Index_IncluyeCitasDeMasDeUnPaciente()
        {
            var controller = CrearControllerConDatosDePrueba(out _);

            var resultado = controller.Index() as ViewResult;
            var modelo = resultado?.Model as List<Cita>;

            Assert.NotNull(modelo);
            var pacientesDistintos = modelo.Select(c => c.PacienteId).Distinct().Count();
            Assert.True(pacientesDistintos > 1);
        }

        [Fact]
        public void Index_CargaCatalogosDePacientesYMedicosEnViewBag()
        {
            var controller = CrearControllerConDatosDePrueba(out _);

            controller.Index();

            Assert.NotNull(controller.ViewBag.Pacientes);
            Assert.NotNull(controller.ViewBag.Medicos);
        }

        [Fact]
        public void PorPaciente_FiltraSoloLasCitasDeEsePaciente()
        {
            var controller = CrearControllerConDatosDePrueba(out _);

            var resultado = controller.PorPaciente(10) as ViewResult;
            var modelo = resultado?.Model as List<Cita>;

            Assert.NotNull(modelo);
            Assert.All(modelo, c => Assert.Equal(10, c.PacienteId));
        }

        [Fact]
        public void Eliminar_QuitaLaCitaDelRepositorio()
        {
            var controller = CrearControllerConDatosDePrueba(out var citasEsperadas);

            controller.Eliminar(1);

            Assert.DoesNotContain(citasEsperadas, c => c.Id == 1);
        }
    }
}