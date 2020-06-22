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
using Microsoft.AspNet.Identity;

namespace Ecommerce.Controllers
{
    public class Catalogos1Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize(Roles ="Empleado")]
        // GET: Catalogos1
        public async Task<ActionResult> Index()
        {
            if (User.Identity.IsAuthenticated) {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();
                if (user.Active && (user.Puesto.Equals("Control de almacen") || user.Puesto.Equals("Director Administrativo"))) {
                    return View(await db.Catalogos.ToListAsync());
                }
                return RedirectToAction("Denegate", "Empleados", user);
            }
            return View();
        }

        // GET: Catalogos1/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalogos catalogos = await db.Catalogos.FindAsync(id);
            if (catalogos == null)
            {
                return HttpNotFound();
            }
            return View(catalogos);
        }

        // GET: Catalogos1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Catalogos1/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,name")] Catalogos catalogos)
        {
            if (ModelState.IsValid)
            {
                db.Catalogos.Add(catalogos);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(catalogos);
        }

        // GET: Catalogos1/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalogos catalogos = await db.Catalogos.FindAsync(id);
            if (catalogos == null)
            {
                return HttpNotFound();
            }
            return View(catalogos);
        }

        // POST: Catalogos1/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,name")] Catalogos catalogos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(catalogos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(catalogos);
        }

        // GET: Catalogos1/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catalogos catalogos = await db.Catalogos.FindAsync(id);
            if (catalogos == null)
            {
                return HttpNotFound();
            }
            return View(catalogos);
        }

        // POST: Catalogos1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Catalogos catalogos = await db.Catalogos.FindAsync(id);
            db.Catalogos.Remove(catalogos);
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
