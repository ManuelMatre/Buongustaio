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
using Newtonsoft.Json.Linq;

namespace Buongustaio.Controllers.EntitiesControllers
{
    public class ComprobantesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comprobantes
        public ActionResult Index()
        {
           
            return View(db.Comprobantes.ToList());
        }

        // GET: Comprobantes/Details/5
        public ActionResult Details(String Id)
        {
            
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comprobantes comprobantes = db.Comprobantes.Find(Id);
            Pagos pago = db.Pagos.Find(comprobantes.Pago_Id);
            Pedidos pedido = db.Pedidos.Find(pago.Pedido);
            ViewBag.pedido = JObject.Parse(pedido.Pedido);

            if (comprobantes == null)
            {
                return HttpNotFound();
            }
            return View(comprobantes);
        }

        // GET: Comprobantes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comprobantes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*[Bind(Include = "Folio,Fechayhora,Orden,Subtotal,Total,Descuento,TerminacionTC,Transaccion,Cliente")] Comprobantes comprobantes,*/Pagos pago,Pedidos pedidos)
        {
            


            //if (ModelState.IsValid)
            //{
                
            //    db.Comprobantes.Add(comprobantes);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            return View();
        }

        // GET: Comprobantes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comprobantes comprobantes = db.Comprobantes.Find(id);
            if (comprobantes == null)
            {
                return HttpNotFound();
            }
            return View(comprobantes);
        }

        // POST: Comprobantes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Folio,Fechayhora,Orden,Subtotal,Total,Descuento,TerminacionTC,Transaccion,Cliente")] Comprobantes comprobantes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comprobantes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comprobantes);
        }

        // GET: Comprobantes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comprobantes comprobantes = db.Comprobantes.Find(id);
            if (comprobantes == null)
            {
                return HttpNotFound();
            }
            return View(comprobantes);
        }

        // POST: Comprobantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Comprobantes comprobantes = db.Comprobantes.Find(id);
            db.Comprobantes.Remove(comprobantes);
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
