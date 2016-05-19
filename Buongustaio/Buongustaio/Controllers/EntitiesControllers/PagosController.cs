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
using System.Web.Script.Serialization;

namespace Buongustaio.Controllers.EntitiesControllers
{
    public class PagosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pagos
        public async Task<ActionResult> Index()
        {
            return View(await db.Pagos.ToListAsync());
        }

        // GET: Pagos/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pagos pagos = await db.Pagos.FindAsync(id);
            if (pagos == null)
            {
                return HttpNotFound();
            }
            return View(pagos);
        }

        // GET: Pagos/Create
        public async Task<ActionResult> Create(string id)
        {
            var orden = (Ordenes)db.Ordenes.Where(x => x.Id == id).FirstOrDefault();
            var pedido = JObject.Parse(orden.Pedido);

            float total = 0;
            foreach (var item in pedido)
            {
                if(item.Value.HasValues == true)
                {
                    int cantidad = int.Parse(item.Value["cantidad"].ToString());
                    float precio = float.Parse(item.Value["platillo"]["Precio"].ToString());
                    total += cantidad * precio;
                }
            }
            Pagos pagos = new Pagos();
            pagos.Cantidad = total;
            pagos.Pedido = orden.Id;
            return View(pagos);
        }

        // POST: Pagos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,NumTarjeta,Expiracion,Propietario,Clave,Cantidad,Transaccion,Pedido")] Pagos pagos)
        {
            pagos.Id = IdUnico.GetUniqueKey();
            pagos.Transaccion = IdUnico.GetUniqueKey();
            var orden = (Ordenes)db.Ordenes.Where(x => x.Id == pagos.Pedido).FirstOrDefault();
            if (ModelState.IsValid)
            {
                if(true/*Agregar método para conexion con banco*/)
                {
                    PedidosController nvoPedido = new PedidosController();
                    var url = await nvoPedido.Create(orden, (float)pagos.Cantidad);
                    db.Pagos.Add(pagos);
                    await db.SaveChangesAsync();
                    return url;
                }
            }

            return View(pagos);
        }

        // GET: Pagos/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pagos pagos = await db.Pagos.FindAsync(id);
            if (pagos == null)
            {
                return HttpNotFound();
            }
            return View(pagos);
        }

        // POST: Pagos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,NumTarjeta,Expiracion,Propietario,Clave,Cantidad,Transaccion")] Pagos pagos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pagos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pagos);
        }

        // GET: Pagos/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pagos pagos = await db.Pagos.FindAsync(id);
            if (pagos == null)
            {
                return HttpNotFound();
            }
            return View(pagos);
        }

        // POST: Pagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Pagos pagos = await db.Pagos.FindAsync(id);
            db.Pagos.Remove(pagos);
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
