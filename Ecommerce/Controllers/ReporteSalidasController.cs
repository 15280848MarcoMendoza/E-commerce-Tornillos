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
    public class ReporteSalidasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ReporteSalidas
        public async Task<ActionResult> Index()
        {
            return View(await db.ReporteSalida.ToListAsync());
        }

        // GET: ReporteSalidas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReporteSalida reporteSalida = await db.ReporteSalida.FindAsync(id);
            if (reporteSalida == null)
            {
                return HttpNotFound();
            }
            return View(reporteSalida);
        }

        // GET: ReporteSalidas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReporteSalidas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nombre,Fecha,Producto,Cantidad")] ReporteSalida reporteSalida)
        {
            if (ModelState.IsValid)
            {
                db.ReporteSalida.Add(reporteSalida);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(reporteSalida);
        }

        // GET: ReporteSalidas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReporteSalida reporteSalida = await db.ReporteSalida.FindAsync(id);
            if (reporteSalida == null)
            {
                return HttpNotFound();
            }
            return View(reporteSalida);
        }

        // POST: ReporteSalidas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nombre,Fecha,Producto,Cantidad")] ReporteSalida reporteSalida)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reporteSalida).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(reporteSalida);
        }

        // GET: ReporteSalidas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReporteSalida reporteSalida = await db.ReporteSalida.FindAsync(id);
            if (reporteSalida == null)
            {
                return HttpNotFound();
            }
            return View(reporteSalida);
        }

        // POST: ReporteSalidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ReporteSalida reporteSalida = await db.ReporteSalida.FindAsync(id);
            db.ReporteSalida.Remove(reporteSalida);
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
