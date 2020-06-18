using Ecommerce.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class FinanzasController : Controller
    {
        // GET: Finanzas
        [Authorize(Roles = "Empleado")]
        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control Finanzas") || user.Puesto.Equals("Director Administrativo")))
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
        public async Task<ActionResult> BalanceGeneral()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control Finanzas") || user.Puesto.Equals("Director Administrativo")))
                {

                    ViewBag.Compras = db.Compras.ToList();
                    ViewBag.Ventas = db.Ventas.ToList();

                    return View();

                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
        public async Task<ActionResult> AsignacionPrecios()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control Finanzas") || user.Puesto.Equals("Director Administrativo")))
                {

                    var compras = db.Productos.AsQueryable();

                    return View(await compras.ToListAsync());

                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
        public async Task<ActionResult> AprobacionPromociones()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control Finanzas") || user.Puesto.Equals("Director Administrativo")))
                {

                    var compras = db.Promociones.AsQueryable();

                    return View(await compras.ToListAsync());

                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
        public async Task<ActionResult> AprobacionCompras(string searchBy, string search, string sortBy)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control Finanzas") || user.Puesto.Equals("Director Administrativo")))
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


        public ActionResult Details(int? id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();
                if (user.Active && (user.Puesto.Equals("Control Finanzas") || user.Puesto.Equals("Director Administrativo")))
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    Compras compra = db.Compras.Find(id);
                    ViewBag.AprobacionCompras = compra.DetallesCompras.ToList();

                    if (ViewBag.AprobacionCompras == null)
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

        public ActionResult DetalleVentas(int Id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control Finanzas") || user.Puesto.Equals("Director Administrativo")))
                {
                    ViewBag.Detalle = db.Ventas.Find(Id).DetalleVentas;
                    return View();
                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            return View();
        }
        public ActionResult DetallePrecios()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control Finanzas") || user.Puesto.Equals("Director Administrativo")))
                {
                    ViewBag.DetallePrecios = db.DetallePrecios.ToList();
                    return View();
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