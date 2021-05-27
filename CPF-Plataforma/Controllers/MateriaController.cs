using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPF_Plataforma.Controllers
{
    public class MateriaController : Controller
    {
        // GET: Materia
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Crear()
        {
            BD_escuelaFinalEntities bd = new BD_escuelaFinalEntities();
            MateriaViewModel viewModel = new MateriaViewModel();
            viewModel.maestros = bd.Maestros.ToList();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Crear(MateriaViewModel model)
        {
            BD_escuelaFinalEntities bd = new BD_escuelaFinalEntities();
            bd.Materias.Add(model.materia);
            bd.SaveChanges();
            return RedirectToAction("Principal_Maestro", "Home");
        }
    }
}