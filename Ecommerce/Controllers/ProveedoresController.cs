using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Text.RegularExpressions;

namespace Ecommerce.Controllers
{
    public class ProveedoresController : Controller
    {
        // GET: Proveedores
        [Authorize(Roles = "Empleado")]
        public ActionResult Historial_Compras()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control finanzas") || user.Puesto.Equals("Director Administrativo")))
                {
                    ViewBag.proveedores = db.Provedores.ToList();
                    return View();
                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        // GET: Proveedores
        [Authorize(Roles = "Empleado")]
        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control finanzas") || user.Puesto.Equals("Director Administrativo")))
                {

                    ViewBag.proveedores = db.Provedores.ToList();

                    return View();
                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empleado")]
        public async Task<ActionResult> Crear([Bind(Include = "Id,Nombre,Telefono,Correo,Compras")] Provedores proveedor)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control finanzas") || user.Puesto.Equals("Director Administrativo")))
                {
                    if (ModelState.IsValid)
                    {
                        db.Provedores.Add(proveedor);
                        await db.SaveChangesAsync();

                        return RedirectToAction("Index");
                    }

                    return View(proveedor);
                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        [Authorize(Roles = "Empleado")]
        public ActionResult Crear()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control finanzas") || user.Puesto.Equals("Director Administrativo")))
                {
                    return View();
                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        // GET: Provedores/Edit
        [Authorize(Roles = "Empleado")]
        public async Task<ActionResult> Edit(int? id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control finanzas") || user.Puesto.Equals("Director Administrativo")))
                {


                    if (id == null)
                    {

                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Provedores provedores = await db.Provedores.FindAsync(id);
                    if (provedores == null)
                    {

                        return HttpNotFound();
                    }

                    return View(provedores);

                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
        // POST: Productos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Empleado")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nombre,Telefono,Correo,Compras")] Provedores proveedor)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control finanzas") || user.Puesto.Equals("Director Administrativo")))
                {

                    if (ModelState.IsValid)
                    {
                        db.Entry(proveedor).State = EntityState.Modified;
                        await db.SaveChangesAsync();

                        return RedirectToAction("Index");
                    }

                    return View(proveedor);

                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
        // GET: Productos/Delete/5
        [Authorize(Roles = "Empleado")]
        public async Task<ActionResult> Delete(int? id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control finanzas") || user.Puesto.Equals("Director Administrativo")))
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Provedores provedor = await db.Provedores.FindAsync(id);
                    if (provedor == null)
                    {
                        return HttpNotFound();
                    }
                    return View(provedor);
                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }


        }


        //GET
        [Authorize(Roles = "Empleado")]
        public async Task<ActionResult> Compra_proveedor(int? id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control finanzas") || user.Puesto.Equals("Director Administrativo")))
                {


                    if (id == null)
                    {
                        if (Session["proveedores"] == null)
                        {
                            return HttpNotFound();
                        }
                        else
                        {
                            ViewBag.Compra_proveedor = Session["proveedores"];

                            ViewBag.Compra_detalles = Session["detalle_compras"];

                            return View();
                        }

                    }
                    else
                    {

                        Provedores provee = await db.Provedores.FindAsync(id);
                        Session["proveedores"] = provee;
                        ViewBag.Compra_proveedor = Session["proveedores"];

                        if (provee == null)
                        {
                            return HttpNotFound();
                        }
                        return View();
                    }

                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        //GET
        [Authorize(Roles = "Empleado")]
        public async Task<ActionResult> Agregar_Producto()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control finanzas") || user.Puesto.Equals("Director Administrativo")))
                {

                    var productos = db.Productos.AsQueryable();
                    return View(await productos.ToListAsync());

                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
        // POST: Productos/Comprar
        [HttpPost, ActionName("Comprar")]
        [Authorize(Roles = "Empleado")]
        public ActionResult Comprar()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();
                if (user.Active && (user.Puesto.Equals("Control finanzas") || user.Puesto.Equals("Director Administrativo")))
                {
                    List<Carrito> detalle_proxy = (List<Carrito>)Session["detalle_compras"];

                    double total = 0;
                    ICollection<DetalleCompras> compras_list = new List<DetalleCompras>();
                    foreach (Carrito det_proxy in detalle_proxy)
                    {
                        Productos producto = db.Productos.Find(det_proxy.Productos.Id);
                        total += (det_proxy.Productos.Costo_unitario * det_proxy.Cantidad);
                        DateTime vencimiento = DateTime.Now;
                        vencimiento = vencimiento.AddMonths(producto.Time_Mount);
                        vencimiento = vencimiento.AddDays(producto.Time_Day);
                        DetalleCompras dcompra = new DetalleCompras
                        {
                            Fecha_vencimiento = vencimiento,
                            Cantidad = det_proxy.Cantidad,
                            PorcentajeDescuento = 0,
                            PorcentajeIncremnto = 0,
                            Productos = producto,
                            SubTotal = (det_proxy.Productos.Precio_final * det_proxy.Cantidad)
                        };
                        compras_list.Add(dcompra);
                    }
                    Provedores prove = (Provedores)Session["proveedores"];
                    Provedores prueba = db.Provedores.Where(x => x.Id == prove.Id).FirstOrDefault();
                    Compras compras = new Compras
                    {
                        DetallesCompras = compras_list,
                        Provedores = prueba,
                        FechaCompra = DateTime.Now,
                        Status = 1,
                        TipoPago = 2,
                        Total = total
                    };

                    db.Compras.Add(compras);

                    db.SaveChangesAsync();

                    Session["proveedores"] = null;
                    Session["detalle_compras"] = null;

                    return Redirect("/Compras/Index");

                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
        //GET
        [Authorize(Roles = "Empleado")]
        public async Task<ActionResult> Agregar_Compra(int? id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control finanzas") || user.Puesto.Equals("Director Administrativo")))
                {

                    var productos = db.Productos.AsQueryable();
                    if (id == null)
                    {

                        return View(await productos.ToListAsync());
                    }

                    productos = productos.Where(x => x.Id == id);
                    if (Session["detalle_compras"] == null)
                    {
                        List<Carrito> carrito = new List<Carrito>();
                        carrito.Add(new Carrito { Productos = productos.FirstOrDefault(), Cantidad = 1 });
                        Session["detalle_compras"] = carrito;
                    }
                    else
                    {
                        List<Carrito> carrito = (List<Carrito>)Session["detalle_compras"];
                        int index = isExist(id);
                        if (index != -1)
                        {
                            carrito[index].Cantidad++;
                        }
                        else
                        {
                            carrito.Add(new Carrito { Productos = productos.FirstOrDefault(), Cantidad = 1 });
                        }
                        Session["detalle_compras"] = carrito;
                    }

                    return RedirectToAction("Compra_proveedor");

                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        private int isExist(int? id)
        {
            List<Carrito> carro = (List<Carrito>)Session["detalle_compras"];
            for (int i = 0; i < carro.Count; i++)
            {
                if (carro[i].Productos.Id.Equals(id))
                    return i;
            }
            return -1;
        }
        [Authorize(Roles = "Empleado")]
        public ActionResult Eliminar_compra(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();
                if (user.Active && (user.Puesto.Equals("Control finanzas") || user.Puesto.Equals("Director Administrativo")))
                {
                    List<Carrito> carro = (List<Carrito>)Session["detalle_compras"];
                    int index = isExist(id);
                    carro.RemoveAt(index);
                    Session["detalle_compras"] = carro;
                    return RedirectToAction("Compra_proveedor");
                }
                return RedirectToAction("Denegate", "Empleados", user);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
        [Authorize(Roles = "Empleado")]
        public ActionResult Editar_compra(int? id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control finanzas") || user.Puesto.Equals("Director Administrativo")))
                {
                    List<Carrito> carro = (List<Carrito>)Session["detalle_compras"];
                    int index = isExist(id);
                    if (index == -1)
                    {
                        return HttpNotFound();
                    }
                    Session["editar"] = carro[index];
                    return View();
                }
                return RedirectToAction("Denegate", "Empleados", user);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        [Authorize(Roles = "Empleado")]
        [HttpPost, ActionName("Editar_compra")]
        public ActionResult Editar_compra(string cantidad)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control finanzas") || user.Puesto.Equals("Director Administrativo")))
                {

                    Carrito carrito = (Carrito)Session["editar"];
                    List<Carrito> carro = (List<Carrito>)Session["detalle_compras"];
                    int index = isExist(carrito.Productos.Id);
                    int status;
                    if (Regex.IsMatch(cantidad, @"^\d+$"))
                    {
                        status = int.Parse(cantidad);
                    }
                    else
                    {
                        ViewBag.Error = "La cantidad debe ser numerico y no debe ser vacio";
                        return View();
                    }
                    carro[index].Cantidad = status;
                    Session["detalle_compras"] = carro;
                    Session["editar"] = null;
                    return RedirectToAction("Compra_proveedor");

                }
                return RedirectToAction("Denegate", "Empleados", user);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}