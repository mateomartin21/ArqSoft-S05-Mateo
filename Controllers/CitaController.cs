using Microsoft.AspNetCore.Mvc;
using CitasApp.Interfaces;
using CitasApp.Models;

namespace CitasApp.Controllers
{
    public class CitaController : Controller
    {
        private readonly ICitaRepository _citaRepo;
        private readonly IPacienteRepository _pacienteRepo;
        private readonly IMedicoRepository _medicoRepo;

        public CitaController(ICitaRepository citaRepo,
                              IPacienteRepository pacienteRepo,
                              IMedicoRepository medicoRepo)
        {
            _citaRepo = citaRepo;
            _pacienteRepo = pacienteRepo;
            _medicoRepo = medicoRepo;
        }

        // ── Listar todas ────────────────────────────────────────────────────
        public IActionResult Index()
        {
            ViewBag.Pacientes = _pacienteRepo.ObtenerTodos();
            ViewBag.Medicos = _medicoRepo.ObtenerTodos();
            return View(_citaRepo.ObtenerTodos());
        }

        // ── Filtrar por paciente ────────────────────────────────────────────
        public IActionResult PorPaciente(int pacienteId)
        {
            ViewBag.Pacientes = _pacienteRepo.ObtenerTodos();
            ViewBag.Medicos = _medicoRepo.ObtenerTodos();
            return View(_citaRepo.ObtenerPorPaciente(pacienteId));
        }

        // ── Crear: formulario ───────────────────────────────────────────────
        [HttpGet]
        public IActionResult Crear()
        {
            ViewBag.Pacientes = _pacienteRepo.ObtenerTodos();
            ViewBag.Medicos = _medicoRepo.ObtenerTodos();
            return View();
        }

        // ── Crear: guardar ──────────────────────────────────────────────────
        [HttpPost]
        public IActionResult Crear(Cita cita)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Pacientes = _pacienteRepo.ObtenerTodos();
                ViewBag.Medicos = _medicoRepo.ObtenerTodos();
                return View(cita);
            }

            _citaRepo.Agregar(cita);
            return RedirectToAction(nameof(Index));
        }

        // ── Eliminar ────────────────────────────────────────────────────────
        [HttpPost]
        public IActionResult Eliminar(int id)
        {
            _citaRepo.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}