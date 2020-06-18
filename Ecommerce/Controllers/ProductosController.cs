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
using System.IO;
using Microsoft.AspNet.Identity;

namespace Ecommerce.Controllers
{
    public class ProductosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize(Roles ="Empleado")]
        // GET: Productos
        public async Task<ActionResult> Index()
        {
            if (User.Identity.IsAuthenticated) {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control de almacen" ) || user.Puesto.Equals("Director Administrativo"))) {
                    return View(await db.Productos.ToListAsync());

                }
                return RedirectToAction("Denegate","Empleados",user);
            }
            return View();
           
        }

        // GET: Productos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = await db.Productos.FindAsync(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            List<Catalogos> Catalogos = db.Catalogos.ToList();
            ViewBag.catalogos = Catalogos;
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int[] Catalogos, HttpPostedFileBase file,
            [Bind(Include = "Nombre,Descripcion,Sabor,Marca,Costo_unitario,Porcentage_descuento,,Time_Mount,Time_Day,Precio_final")] Productos productos)
        {
            
            List<Catalogos> catalogosP = new List<Catalogos>();
            foreach (int catalog in Catalogos)
            {
                catalogosP.Add(db.Catalogos.Find(catalog));
            }

            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                productos.Url_image = "img/" + fileName+extension;
                fileName = Path.Combine(Server.MapPath("~/Content/img/"), fileName+extension);
                file.SaveAs(fileName);
                productos.Catalogos = catalogosP;
                productos.activo = true;
                productos.Cantidad_ventas = 0;
                productos.stock = 1;
                db.Productos.Add(productos);
                await db.SaveChangesAsync();
                
            }

            return RedirectToAction("Index");
        }

        // GET: Productos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            List<Catalogos> Catalogos = db.Catalogos.ToList();
            ViewBag.catalogos = Catalogos;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = await db.Productos.FindAsync(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            
            return View(productos);
            
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int[] Catalogos,[Bind(Include = "Id,Nombre,Descripcion,Sabor,activo,Marca,Costo_unitario,Porcentage_descuento,Time_Mount,Time_Day,Precio_final")] Productos productos,HttpPostedFileBase file)
        {
            List<Catalogos> catalogosP = new List<Catalogos>();
            if (Catalogos != null)
            {
                foreach (int catalog in Catalogos)
                {
                    catalogosP.Add(db.Catalogos.Find(catalog));
                }
                productos.Catalogos = catalogosP;
            }
            if (file != null) {
                string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                productos.Url_image = "img/" + fileName + extension;
                fileName = Path.Combine(Server.MapPath("~/Content/img/"), fileName + extension);
                file.SaveAs(fileName);
            }
          
            if (ModelState.IsValid)
            {
               
               

                db.Entry(productos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                
            }
            return RedirectToAction("Index");
        }

        // GET: Productos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Productos productos = await db.Productos.FindAsync(id);
            if (productos == null)
            {
                return HttpNotFound();
            }
            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Productos productos = await db.Productos.FindAsync(id);
            db.Productos.Remove(productos);
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

        public ActionResult Cambio(int id)
        {
            Productos prod = db.Productos.Find(id);
            prod.activo = !prod.activo;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }

}
