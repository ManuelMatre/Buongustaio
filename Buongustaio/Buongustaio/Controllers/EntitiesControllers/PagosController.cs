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
using System.Text;
using System.Web.Helpers;
using System.IO;

namespace Buongustaio.Controllers.EntitiesControllers
{
    public class PagosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        WebResponse response;
        Stream datastream;
        string responseFromServer;

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
            var orden = (Ordenes)db.Ordenes.Where(x => x.Id == pagos.Pedido).FirstOrDefault();
            var cantidad = pagos.Cantidad;

            if (ModelState.IsValid)
            {
                if(await postBank(pagos))
                {
                    pagos.Transaccion = responseFromServer;
                    PedidosController nvoPedido = new PedidosController();
                    Pedidos pedido = await nvoPedido.Create(orden, pagos);
                    db.Pagos.Add(pagos);

                    await db.SaveChangesAsync();
                    Comprobantes comprobante = new Comprobantes();
                    Retroalimentacion RA = new Retroalimentacion();
                    comprobante.Folio = IdUnico.GetUniqueKey();
                    comprobante.Fechayhora = DateTime.Now;
                    comprobante.Pago_Id = pagos.Id;
                    comprobante.Subtotal = pagos.Cantidad;
                    comprobante.Total = cantidad * 1.16;
                    db.Comprobantes.Add(comprobante);
                    await db.SaveChangesAsync();
                    RA.Id = IdUnico.GetUniqueKey();
                    RA.Fecha = DateTime.Now;
                    RA.Cliente = pedido.Cliente;
                    RA.Orden = pedido.Id;
                    db.Retroalimentacions.Add(RA);
                    await db.SaveChangesAsync();
                    ComprobantesController comprobanteController = new ComprobantesController();
                    comprobanteController.Details(comprobante.Folio);

                    return RedirectToAction("../Comprobantes/Details/" + comprobante.Folio);
                }
                else
                {
                    ViewBag.BankError = "Hubo un problema con sus datos";
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

        private async Task<bool> postBank(Pagos pago)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://192.168.1.156:8080/api/Transaction");

            MiPago userPaymment = new MiPago();
            userPaymment.Amount = pago.Cantidad;
            userPaymment.CardNumber = pago.NumTarjeta;
            userPaymment.ExpirationDate = pago.Expiracion;
            userPaymment.SecurityCode = pago.Clave.ToString();
            userPaymment.Token = "bd4e5b04-9190-49e4-b5f1-b09afbd66f3f";

            var userData = new JavaScriptSerializer().Serialize(userPaymment);
            var data = Encoding.ASCII.GetBytes(userData);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            try
            {
                response = request.GetResponse();
                datastream = response.GetResponseStream();
                StreamReader reader = new StreamReader(datastream);
                responseFromServer = reader.ReadToEnd();
                return true;
            }
            catch (System.Net.WebException ex)
            {
                return false;
            }
        }
    }
}
