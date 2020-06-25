using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Net.Mail;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();

                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

                if (userManager.IsInRole(iduser, "Empleado"))
                {
                    
                    return RedirectToAction("Home", "Empleados");
                }
                else
                {
                    //Cliente logeado
                    ViewBag.Sales = db.Productos.Where(p => p.Cantidad_ventas > 0).OrderBy(p => p.Cantidad_ventas).ToList();
                    ViewBag.Sale = db.Productos.Where(p => p.Cantidad_ventas > 0).OrderBy(p => p.Cantidad_ventas).First();
                    ViewBag.Offers = db.Productos.Where(p => p.Porcentage_descuento > 0).OrderBy(p => p.Porcentage_descuento).ToList();
                    ViewBag.Offer = db.Productos.Where(p => p.Porcentage_descuento > 0).OrderBy(p => p.Porcentage_descuento).First();
                    return View();
                }

            }
            else {
                ViewBag.Sales = db.Productos.Where(p => p.Cantidad_ventas > 0).OrderBy(p => p.Cantidad_ventas).ToList();
                ViewBag.Sale = db.Productos.Where(p => p.Cantidad_ventas > 0).OrderBy(p => p.Cantidad_ventas).First();
                ViewBag.Offers = db.Productos.Where(p => p.Porcentage_descuento > 0).OrderBy(p => p.Porcentage_descuento).ToList();
                ViewBag.Offer = db.Productos.Where(p => p.Porcentage_descuento > 0).OrderBy(p => p.Porcentage_descuento).First();
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.productos = db.Productos.ToList();
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.productos = db.Productos.ToList();
            return View();
        }

        [HttpPost]
        // public String Contact(string correoo, string asunto, string mensaje, string nombre, string apellido)
        // {

        //   try
        //  {
        //  MailMessage correo = new MailMessage();
        //   correo.From = new MailAddress("luisdla626@gmail.com");
        // correo.To.Add("luisdla626@gmail.com");
        // correo.Subject = asunto;
        // String body = nombre +" " + apellido+ " " + "Se puso en contacto con nosotros este es su mensaje" + "\n" + correoo + "\n" + mensaje;
        // correo.Body = body;
        // correo.IsBodyHtml = true;
        // correo.Priority = MailPriority.Normal;

        //   SmtpClient smtp = new SmtpClient();
        //   smtp.Host = "smtp.gmail.com";
        // smtp.Port = 25;
        //smtp.EnableSsl = true;
        //smtp.UseDefaultCredentials = true;
        //String sCeuntaCorreo = "luisdla626@gmail.com";
        // String sPasswordCorreo = "";
        //smtp.Credentials = new System.Net.NetworkCredential(sCeuntaCorreo, sPasswordCorreo);

        //                smtp.Send(correo);
        //ViewBag.Mensaje = "Mensaje enviado";
        // return ViewBag.Mensaje;


        //      }
        //           catch (Exception e)
        //  {
        //        Console.WriteLine(e.StackTrace);
        //          ViewBag.Error = e.Message;
        //            return ViewBag.Error;

        //          }

        //  }


        public ActionResult Tienda(int? Id)
        {
            ICollection<Productos> productos;
            if (Id.HasValue)
            {
                var catalog = db.Catalogos.Find(Id);
                ViewBag.active = Id;
                productos = catalog.Productos;
            }
            else
            {
               productos = db.Productos.ToList();
            }
            
            ViewBag.catalogos = db.Catalogos.ToList();
            ViewBag.productos = productos.ToList();
            ViewBag.metodos = new MetodosPago().MetodoPago;
            return View();
        }
    }
}