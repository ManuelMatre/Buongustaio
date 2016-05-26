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
    public class MiembrosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Miembros
        public ActionResult Index()
        {
            return View(db.Miembros.ToList());
        }

        // GET: Miembros/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Miembros miembros = db.Miembros.Find(id);
            if (miembros == null)
            {
                return HttpNotFound();
            }
            return View(miembros);
        }

        // GET: Miembros/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Miembros/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Estatus,Expiracion,cliente")] Miembros miembros)
        {

            //miembros.Id= IdUnico.GetUniqueKey();
            //miembros.cliente = cliente.Telefono;
            //miembros.Expiracion = DateTime.Today.AddYears(2);
            //miembros.Estatus = "Activo";
            //var telefono =db.Miembros.Where(x => x.cliente == miembros.cliente);
            
            
            
           
            if (ModelState.IsValid)
            {
                //db.Miembros.Add(miembros);
                //db.SaveChanges();
                 
                return RedirectToAction("../Miembros/Edit/" + miembros.cliente);
            }

            return View(miembros);
        }

        // GET: Miembros/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Miembros miembros = db.Miembros.Find(id);
            
            if (miembros == null)
            {
                return HttpNotFound();
            }
            return View(miembros);
        }

        // POST: Miembros/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Estatus,Expiracion,cliente")] Miembros miembros)
        {
            if (ModelState.IsValid)
            {
                Clientes cliente = db.Clientes.Find(miembros.cliente);
                cliente.Es_Miembro = true;
                miembros.Expiracion = DateTime.Today.AddYears(2);
                miembros.Estatus = "Activo";   
                db.Entry(miembros).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return View(miembros);
        }

        // GET: Miembros/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Miembros miembros = db.Miembros.Find(id);
            if (miembros == null)
            {
                return HttpNotFound();
            }
            return View(miembros);
        }

        // POST: Miembros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Miembros miembros = db.Miembros.Find(id);
            db.Miembros.Remove(miembros);
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
