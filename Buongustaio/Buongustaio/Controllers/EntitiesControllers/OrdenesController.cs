using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Buongustaio.Models;
using Buongustaio.Classes;

namespace Buongustaio.Controllers.EntitiesControllers
{
    public class OrdenesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Ordenes
        public async Task<ActionResult> Index()
        {
            return View(await db.Ordenes.ToListAsync());
        }

        // GET: Ordenes/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordenes ordenes = await db.Ordenes.FindAsync(id);
            if (ordenes == null)
            {
                return HttpNotFound();
            }
            return View(ordenes);
        }

        // GET: Ordenes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ordenes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Cliente,Pedido,Fecha")] Ordenes ordenes)
        {
            ordenes.Id = IdUnico.GetUniqueKey();
            ordenes.Fecha = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Ordenes.Add(ordenes);
                await db.SaveChangesAsync();
                var result = new { url = "Successed", ordenId = ordenes.Id };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            var error = new { url = "Error", ordenId = "Datos Inválidos" };
            return Json(error, JsonRequestBehavior.AllowGet);
        }

        // GET: Ordenes/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordenes ordenes = await db.Ordenes.FindAsync(id);
            if (ordenes == null)
            {
                return HttpNotFound();
            }
            return View(ordenes);
        }

        // POST: Ordenes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Cliente,Pedido,Fecha")] Ordenes ordenes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordenes).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ordenes);
        }

        // GET: Ordenes/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordenes ordenes = await db.Ordenes.FindAsync(id);
            if (ordenes == null)
            {
                return HttpNotFound();
            }
            return View(ordenes);
        }

        // POST: Ordenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Ordenes ordenes = await db.Ordenes.FindAsync(id);
            db.Ordenes.Remove(ordenes);
            await db.SaveChangesAsync();
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
