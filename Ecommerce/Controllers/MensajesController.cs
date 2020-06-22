using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecommerce.Models;

namespace Ecommerce.Controllers
{
    public class MensajesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Mensajes
        public async Task<ActionResult> Index()
        {
            return View(await db.Mensajes.ToListAsync());
        }

        // GET: Mensajes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mensajes mensajes = await db.Mensajes.FindAsync(id);
            if (mensajes == null)
            {
                return HttpNotFound();
            }
            return View(mensajes);
        }

        // GET: Mensajes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mensajes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,NombrePaqueteria,FechaEntrega,Direccion,Remitente,Cliente,Telefono,Correo,Peso,Producto")] Mensajes mensajes)
        {
            if (ModelState.IsValid)
            {
                db.Mensajes.Add(mensajes);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(mensajes);
        }

        // GET: Mensajes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mensajes mensajes = await db.Mensajes.FindAsync(id);
            if (mensajes == null)
            {
                return HttpNotFound();
            }
            return View(mensajes);
        }

        // POST: Mensajes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,NombrePaqueteria,FechaEntrega,Direccion,Remitente,Cliente,Telefono,Correo,Peso,Producto")] Mensajes mensajes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mensajes).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mensajes);
        }

        // GET: Mensajes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mensajes mensajes = await db.Mensajes.FindAsync(id);
            if (mensajes == null)
            {
                return HttpNotFound();
            }
            return View(mensajes);
        }

        // POST: Mensajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Mensajes mensajes = await db.Mensajes.FindAsync(id);
            db.Mensajes.Remove(mensajes);
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
