using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Buongustaio.Models;
using Buongustaio.Classes;

namespace Buongustaio.Controllers.EntitiesControllers
{
    public class SeguimientoOrdenController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SeguimientoOrden
        public ActionResult Index()
        {
            return View(db.Retroalimentacions.ToList());
        }

        // GET: SeguimientoOrden/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Retroalimentacion retroalimentacion = db.Retroalimentacions.Find(id);
            if (retroalimentacion == null)
            {
                return HttpNotFound();
            }
            return View(retroalimentacion);
        }

        // GET: SeguimientoOrden/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SeguimientoOrden/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Estado_orden,Nivel_satisfaccion,Cliente,Fecha,Orden")] Retroalimentacion retroalimentacion)
        {

            
            

            if (ModelState.IsValid)
            {

                //db.Retroalimentacions.Add(retroalimentacion);
                //db.SaveChanges();
                
                return RedirectToAction("../SeguimientoOrden/Edit/"+retroalimentacion.Id);
            }

            return View(retroalimentacion);
        }

        // GET: SeguimientoOrden/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Retroalimentacion retroalimentacion = db.Retroalimentacions.Find(id);
            if (retroalimentacion == null)
            {
                return HttpNotFound();
            }
            return View(retroalimentacion);
        }

        // POST: SeguimientoOrden/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Estado_orden,Nivel_satisfaccion,Cliente,Fecha,Orden")] Retroalimentacion retroalimentacion)
        {
            if (ModelState.IsValid)
            {
                
                
                db.Entry(retroalimentacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(retroalimentacion);
        }

        // GET: SeguimientoOrden/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Retroalimentacion retroalimentacion = db.Retroalimentacions.Find(id);
            if (retroalimentacion == null)
            {
                return HttpNotFound();
            }
            return View(retroalimentacion);
        }

        // POST: SeguimientoOrden/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Retroalimentacion retroalimentacion = db.Retroalimentacions.Find(id);
            db.Retroalimentacions.Remove(retroalimentacion);
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
