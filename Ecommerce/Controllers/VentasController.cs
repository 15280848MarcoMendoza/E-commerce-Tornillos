using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ecommerce.Controllers
{
    public class VentasController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Ventas
        [Authorize(Roles = "Empleado")]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control finanzas") || user.Puesto.Equals("Director Administrativo")))
                {
                    ViewBag.Ventas = db.Ventas.ToList();
                    return View();
                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            return View();
        }

        [Authorize(Roles = "Empleado")]
        public  ActionResult DetalleVentas(int Id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control finanzas") || user.Puesto.Equals("Director Administrativo")))
                {
                    ViewBag.Detalle = db.Ventas.Find(Id).DetalleVentas;
                    return View();
                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            return View();
        }
    }
}