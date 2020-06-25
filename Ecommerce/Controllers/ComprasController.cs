using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PagedList;
using Microsoft.AspNet.Identity;

namespace Ecommerce.Controllers
{
    public class ComprasController : Controller
    {
        // GET: Compras
        [Authorize(Roles = "Empleado")]
        public async Task<ActionResult> Index(string searchBy, string search,string sortBy)
        {
              ApplicationDbContext db = new ApplicationDbContext();
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control finanzas") || user.Puesto.Equals("Director Administrativo")))
                {

                    ViewBag.StatusSort = String.IsNullOrEmpty(sortBy) ? "Status desc" : "";
                    ViewBag.TipoPagoSort = sortBy == "TipoPago" ? "TipoPago desc" : "TipoPago";
                    ViewBag.FechaSort = sortBy == "Fecha" ? "Fecha desc" : "Fecha";
                    ViewBag.ProveedorSort = sortBy == "Proveedor" ? "Proveedor desc" : "Proveedor";
                    var compras = db.Compras.AsQueryable();

                    if (searchBy == "TipoPago")
                    {
                        int? status = null;
                        search = search.ToUpper();
                        if (search == "CONTADO" || search == "CREDITO")
                        {

                            switch (search)
                            {
                                case "CONTADO":
                                    status = 2;
                                    break;
                                case "CREDITO":
                                    status = 1;
                                    break;
                            }
                        }
                        else if (Regex.IsMatch(search, @"^\d+$"))
                        {
                            status = int.Parse(search);
                        }
                        else
                        {
                            status = 0;
                        }
                        compras = compras.Where(x => x.TipoPago.ToString() == status.ToString() || status == null);
                    }
                    else if (searchBy == "Status")
                    {
                        int? status = null;
                        search = search.ToUpper();
                        if (search == "PEDIDO" || search == "PAGADO" || search == "RECIBIDO")
                        {

                            switch (search)
                            {
                                case "PEDIDO":
                                    status = 1;
                                    break;
                                case "PAGADO":
                                    status = 2;
                                    break;
                                case "RECIBIDO":
                                    status = 3;
                                    break;
                            }
                        }
                        else if (Regex.IsMatch(search, @"^\d+$"))
                        {
                            status = int.Parse(search);
                        }
                        else
                        {
                            status = 0;
                        }

                        compras = compras.Where(x => x.Status.ToString() == status.ToString() || status == null);

                    }
                    else if (searchBy == "Provedor")
                    {
                        compras = compras.Where(x => x.Provedores.Nombre.ToString().StartsWith(search) || search == null);
                    }

                    switch (sortBy)
                    {
                        case "TipoPago":
                            compras = compras.OrderBy(x => x.TipoPago);
                            break;
                        case "TipoPago desc":
                            compras = compras.OrderByDescending(x => x.TipoPago);
                            break;
                        case "Status":
                            compras = compras.OrderBy(x => x.Status);
                            break;
                        case "Status desc":
                            compras = compras.OrderByDescending(x => x.Status);
                            break;
                        case "Proveedor":
                            compras = compras.OrderBy(x => x.Provedores.Nombre);
                            break;
                        case "Proveedor desc":
                            compras = compras.OrderByDescending(x => x.Provedores.Nombre);
                            break;
                        case "Fecha":
                            compras = compras.OrderBy(x => x.FechaCompra);
                            break;
                        case "Fecha desc":
                            compras = compras.OrderByDescending(x => x.FechaCompra);
                            break;
                        default:
                            compras = compras.OrderBy(x => x.Id);
                            break;
                    }

                    return View(await compras.ToListAsync());

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

                    List<SelectListItem> lst = new List<SelectListItem>();
                    lst.Add(new SelectListItem() { Text = "PEDIDO", Value = "1" });
                    lst.Add(new SelectListItem() { Text = "PAGADO", Value = "2" });
                    lst.Add(new SelectListItem() { Text = "RECIBIDO", Value = "3" });

                    ViewBag.Opciones = lst;
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Compras compras = await db.Compras.FindAsync(id);
                    if (compras == null)
                    {
                        return HttpNotFound();
                    }

                    return View(compras);

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
        public async Task<ActionResult> Edit([Bind(Include = "Id,DetallesCompras,Provedores,FechaCompra,Status,TipoPago,Total")] Compras compras)
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
                        db.Entry(compras).State = EntityState.Modified;
                        await db.SaveChangesAsync();

                        return RedirectToAction("Index");
                    }
                    return View(compras);
                }
                return RedirectToAction("Denegate", "Empleados", user);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
        // GET: Compras/Details
        [Authorize(Roles = "Empleado")]
        public ActionResult Details(int? id)
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
                    Compras compra = db.Compras.Find(id);
                    ViewBag.Detalles_Compra = compra.DetallesCompras.ToList();

                    if (ViewBag.Detalles_Compra == null)
                    {
                        return HttpNotFound();
                    }
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
        public ActionResult Detalle_Compra(string searchBy,string currentsearch, string search, string currentFilter, string sortBy, int? page)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control finanzas") || user.Puesto.Equals("Director Administrativo")))
                {

                    ViewBag.CurrentSort = sortBy;
                    ViewBag.NombreSort = String.IsNullOrEmpty(sortBy) ? "Nombre desc" : "";
                    ViewBag.CostoUnitarioSort = sortBy == "CostoUnitario" ? "CostoUnitario desc" : "CostoUnitario";
                    ViewBag.FechaSort = sortBy == "Fecha" ? "Fecha desc" : "Fecha";
                    ViewBag.CostoVentaSort = sortBy == "CostoVenta" ? "CostoVenta desc" : "CostoVenta";
                    ViewBag.CantidadSort = sortBy == "Cantidad" ? "Cantidad desc" : "Cantidad";
                    var compras = db.DetalleCompras.AsQueryable();
                    if (search != null)
                    {
                        page = 1;
                    }
                    else
                    {
                        search = currentFilter;
                        searchBy = currentsearch;
                    }
                    ViewBag.SearchBy = searchBy;
                    ViewBag.CurrentFilter = search;
                    if (searchBy == "Nombre")
                    {
                        compras = compras.Where(x => x.Productos.Nombre.ToString() == search || search == null);
                    }
                    else if (searchBy == "Cantidad")
                    {
                        int? status = null;
                        if (Regex.IsMatch(search, @"^\d+$"))
                        {
                            status = int.Parse(search);
                        }
                        else
                        {
                            status = 0;
                        }
                        compras = compras.Where(x => x.Cantidad.ToString().StartsWith(status.ToString()) || status == null);
                    }


                    switch (sortBy)
                    {
                        case "Nombre":
                            compras = compras.OrderBy(x => x.Productos.Nombre);
                            break;
                        case "Nombre desc":
                            compras = compras.OrderByDescending(x => x.Productos.Nombre);
                            break;
                        case "CostoUnitario":
                            compras = compras.OrderBy(x => x.Productos.Costo_unitario);
                            break;
                        case "CostoUnitario desc":
                            compras = compras.OrderByDescending(x => x.Productos.Costo_unitario);
                            break;
                        case "Fecha":
                            compras = compras.OrderBy(x => x.Fecha_vencimiento);
                            break;
                        case "Fecha desc":
                            compras = compras.OrderByDescending(x => x.Fecha_vencimiento);
                            break;
                        case "CostoVenta":
                            compras = compras.OrderBy(x => x.Productos.Precio_final);
                            break;
                        case "CostoVenta desc":
                            compras = compras.OrderByDescending(x => x.Productos.Precio_final);
                            break;
                        case "Cantidad":
                            compras = compras.OrderBy(x => x.Cantidad);
                            break;
                        case "Cantidad desc":
                            compras = compras.OrderByDescending(x => x.Cantidad);
                            break;
                        default:
                            compras = compras.OrderBy(x => x.Id);
                            break;
                    }
                    int pageSize = 5;
                    int pageNumber = (page ?? 1);
                    return View(compras.ToPagedList(pageNumber, pageSize));

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