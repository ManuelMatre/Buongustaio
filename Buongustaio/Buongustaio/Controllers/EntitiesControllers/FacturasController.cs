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
using Newtonsoft.Json.Linq;

namespace Buongustaio.Controllers.EntitiesControllers
{
    public class FacturasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Facturas
        public async Task<ActionResult> Index()
        {
            var facturas = db.Facturas.Include(f => f.DatosFiscales);
            return View(await facturas.ToListAsync());
        }

        // GET: Facturas/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturas facturas = await db.Facturas.FindAsync(id);
            if (facturas == null)
            {
                return HttpNotFound();
            }
            Comprobantes miComprobante = db.Comprobantes.Find(facturas.Comprobante_Folio);
            Pagos miPago = db.Pagos.Find(miComprobante.Pago_Id);
            Pedidos miPedido = db.Pedidos.Find(miPago.Pedido);

            ViewBag.Comprobante = JObject.Parse(miPedido.Pedido);
            return View(facturas);
        }

        // GET: Facturas/Create
        public ActionResult Create()
        {
            //ViewBag.RFC = new SelectList(db.DatosFiscales, "RFC", "RFC");
            return View();
        }

        // POST: Facturas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Folio,Comprobante_Folio,RFC")] Facturas facturas)
        {
            facturas.Folio = IdUnico.GetUniqueKey();
            if (ModelState.IsValid)
            {
                db.Facturas.Add(facturas);
                await db.SaveChangesAsync();
                return RedirectToAction("Details/"+facturas.Folio);
                //await Details(facturas.Folio);
            }

            //ViewBag.RFC = new SelectList(db.DatosFiscales, "RFC", "Razon_social", facturas.RFC);
            return View(facturas);
        }

        // GET: Facturas/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturas facturas = await db.Facturas.FindAsync(id);
            if (facturas == null)
            {
                return HttpNotFound();
            }
            ViewBag.RFC = new SelectList(db.DatosFiscales, "RFC", "Razon_social", facturas.RFC);
            return View(facturas);
        }

        // POST: Facturas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Folio,Comprobante_Id,RFC")] Facturas facturas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facturas).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RFC = new SelectList(db.DatosFiscales, "RFC", "Razon_social", facturas.RFC);
            return View(facturas);
        }

        // GET: Facturas/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facturas facturas = await db.Facturas.FindAsync(id);
            if (facturas == null)
            {
                return HttpNotFound();
            }
            return View(facturas);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Facturas facturas = await db.Facturas.FindAsync(id);
            db.Facturas.Remove(facturas);
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
