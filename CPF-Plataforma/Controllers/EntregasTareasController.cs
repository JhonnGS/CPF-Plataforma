using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CPF_Plataforma.Controllers
{
    public class EntregasTareasController : Controller
    {
        private BD_escuelaFinalEntities db = new BD_escuelaFinalEntities();

        // GET: EntregasTareas
        public ActionResult Index()
        {
            var entregasTareas = db.EntregasTareas.Include(e => e.Alumnos).Include(e => e.Tareas);
            return View(entregasTareas.ToList());
        }

        // GET: EntregasTareas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntregasTareas entregasTareas = db.EntregasTareas.Find(id);
            if (entregasTareas == null)
            {
                return HttpNotFound();
            }
            return View(entregasTareas);
        }

        // GET: EntregasTareas/Create
        public ActionResult Create()
        {
            ViewBag.idAlumno = new SelectList(db.Alumnos, "idAlumno", "nombre");
            ViewBag.idTarea = new SelectList(db.Tareas, "idTarea", "descripcionTarea");
            return View();
        }

        // POST: EntregasTareas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idEntregasTarea,idTarea,idAlumno,tarea,fechaHoraEntrega,puntos")] EntregasTareas entregasTareas)
        {
            if (ModelState.IsValid)
            {
                db.EntregasTareas.Add(entregasTareas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idAlumno = new SelectList(db.Alumnos, "idAlumno", "nombre", entregasTareas.idAlumno);
            ViewBag.idTarea = new SelectList(db.Tareas, "idTarea", "descripcionTarea", entregasTareas.idTarea);
            return View(entregasTareas);
        }

        // GET: EntregasTareas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntregasTareas entregasTareas = db.EntregasTareas.Find(id);
            if (entregasTareas == null)
            {
                return HttpNotFound();
            }
            ViewBag.idAlumno = new SelectList(db.Alumnos, "idAlumno", "nombre", entregasTareas.idAlumno);
            ViewBag.idTarea = new SelectList(db.Tareas, "idTarea", "descripcionTarea", entregasTareas.idTarea);
            return View(entregasTareas);
        }

        // POST: EntregasTareas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEntregasTarea,idTarea,idAlumno,tarea,fechaHoraEntrega,puntos")] EntregasTareas entregasTareas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entregasTareas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idAlumno = new SelectList(db.Alumnos, "idAlumno", "nombre", entregasTareas.idAlumno);
            ViewBag.idTarea = new SelectList(db.Tareas, "idTarea", "descripcionTarea", entregasTareas.idTarea);
            return View(entregasTareas);
        }

        // GET: EntregasTareas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntregasTareas entregasTareas = db.EntregasTareas.Find(id);
            if (entregasTareas == null)
            {
                return HttpNotFound();
            }
            return View(entregasTareas);
        }

        // POST: EntregasTareas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EntregasTareas entregasTareas = db.EntregasTareas.Find(id);
            db.EntregasTareas.Remove(entregasTareas);
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