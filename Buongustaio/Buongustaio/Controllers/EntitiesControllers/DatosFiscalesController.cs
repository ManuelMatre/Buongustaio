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

namespace Buongustaio.Controllers.EntitiesControllers
{
    public class DatosFiscalesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DatosFiscales
        public async Task<ActionResult> Index()
        {
            return View(await db.DatosFiscales.ToListAsync());
        }

        // GET: DatosFiscales/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatosFiscales datosFiscales = await db.DatosFiscales.FindAsync(id);
            if (datosFiscales == null)
            {
                return HttpNotFound();
            }
            return View(datosFiscales);
        }

        // GET: DatosFiscales/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DatosFiscales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RFC,Razon_social,Domicilio,Telefono,Correo")] DatosFiscales datosFiscales)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.DatosFiscales.Add(datosFiscales);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }catch(System.Data.Entity.Infrastructure.DbUpdateException ex)
                {
                    //throw new HttpException(404, "La clave RFC ya está registrada.");
                    ViewBag.ErrorMessage = "La clave RFC ya está registrada.";
                    return View(datosFiscales);
                }
                
            }

            return View(datosFiscales);
        }

        // GET: DatosFiscales/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatosFiscales datosFiscales = await db.DatosFiscales.FindAsync(id);
            if (datosFiscales == null)
            {
                return HttpNotFound();
            }
            return View(datosFiscales);
        }

        // POST: DatosFiscales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RFC,Razon_social,Domicilio,Telefono,Correo")] DatosFiscales datosFiscales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(datosFiscales).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(datosFiscales);
        }

        // GET: DatosFiscales/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatosFiscales datosFiscales = await db.DatosFiscales.FindAsync(id);
            if (datosFiscales == null)
            {
                return HttpNotFound();
            }
            return View(datosFiscales);
        }

        // POST: DatosFiscales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            DatosFiscales datosFiscales = await db.DatosFiscales.FindAsync(id);
            db.DatosFiscales.Remove(datosFiscales);
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
