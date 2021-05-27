using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPF_Plataforma.Controllers
{
    public class TareasController : Controller
    {
        private BD_escuelaFinalEntities db = new BD_escuelaFinalEntities();

        // GET: Tareas
        public ActionResult Index()
        {
            var tareas = db.Tareas.Include(t => t.Materias);
            return View(tareas.ToList());
        }

        // GET: Tareas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tareas tareas = db.Tareas.Find(id);
            if (tareas == null)
            {
                return HttpNotFound();
            }
            return View(tareas);
        }

        // GET: Tareas/Create
        public ActionResult Create()
        {
            ViewBag.idMateria = new SelectList(db.Materias, "idMateria", "nombreMateria");
            return View();
        }

        // POST: Tareas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idTarea,idMateria,descripcionTarea,fechaInicio,fechaFinal,tipoTarea")] Tareas tareas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Tareas.Add(tareas);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

            ViewBag.idMateria = new SelectList(db.Materias, "idMateria", "nombreMateria", tareas.idMateria);
            return View(tareas);
        }

        // GET: Tareas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tareas tareas = db.Tareas.Find(id);
            if (tareas == null)
            {
                return HttpNotFound();
            }
            ViewBag.idMateria = new SelectList(db.Materias, "idMateria", "nombreMateria", tareas.idMateria);
            return View(tareas);
        }

        // POST: Tareas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idTarea,idMateria,descripcionTarea,fechaInicio,fechaFinal,tipoTarea")] Tareas tareas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tareas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idMateria = new SelectList(db.Materias, "idMateria", "nombreMateria", tareas.idMateria);
            return View(tareas);
        }

        // GET: Tareas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tareas tareas = db.Tareas.Find(id);
            if (tareas == null)
            {
                return HttpNotFound();
            }
            return View(tareas);
        }

        // POST: Tareas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tareas tareas = db.Tareas.Find(id);
            db.Tareas.Remove(tareas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}