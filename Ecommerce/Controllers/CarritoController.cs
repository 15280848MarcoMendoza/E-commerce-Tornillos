using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ecommerce.Controllers
{
    public class CarritoController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Carrito
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var user = userManager.FindById(User.Identity.GetUserId());
                Cliente cliente = db.Cliente.Where(c => c.Id_users == user.Id).FirstOrDefault();
                ViewBag.cliente = cliente;
            }
            ViewBag.message = TempData["Compra"];
            ViewBag.metodo = new MetodosPago().MetodoPago;
            return View();
        }

        public ActionResult Comprar(int id)
        {
            Productos productos = db.Productos.Find(id);
            if (Session["carro"] == null)
            {
                List<Carrito> carro = new List<Carrito>();
                carro.Add(new Carrito{ Productos = productos, Cantidad = 1 });
                Session["carro"] = carro;
            }
            else
            {
                List<Carrito> carro = (List<Carrito>)Session["carro"];
                int index = isExist(id);
                if (index != -1)
                {
                    carro[index].Cantidad++;
                }
                else
                {
                    carro.Add(new Carrito { Productos = productos, Cantidad = 1 });
                }
                Session["carro"] = carro;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id)
        {
            List<Carrito> carro = (List<Carrito>)Session["carro"];
            int index = isExist(id);
            carro.RemoveAt(index);
            Session["carro"] = carro;
            return RedirectToAction("Index");
        }

        public ActionResult Cantidad(int Id, bool Mas)
        {
            List<Carrito> carro = (List<Carrito>)Session["carro"];
            int index = isExist(Id);
            if (index != -1)
            {
                if (Mas)
                {
                    carro[index].Cantidad++;
                }
                else
                {
                    carro[index].Cantidad--;
                }
            }
            return RedirectToAction("Index");
        }

        private int isExist(int id)
        {
            List<Carrito> carro = (List<Carrito>)Session["carro"];
            for (int i = 0; i < carro.Count; i++)
            {
                if (carro[i].Productos.Id.Equals(id))
                    return i;
            }
            return -1;
        }

        public ActionResult TerminarCompra(int tipoPago)
        {
            List<Carrito> carro = (List<Carrito>)Session["carro"];
            double total = 0;
            ICollection<DetalleVenta> detalle = new List<DetalleVenta>();
            foreach (Carrito car in carro)
            {
                Productos producto = db.Productos.Find(car.Productos.Id);
                total += (car.Productos.Precio_final * car.Cantidad);
                DetalleVenta dventa = new DetalleVenta
                {
                    
                    Cantidad = car.Cantidad,
                    Porcentaje_Descuento = 0,
                    Porcentaje_Incrmento = 0,
                    Producto = producto,
                    Subtotal = (car.Productos.Precio_final * car.Cantidad),
                };
                detalle.Add(dventa);
            }
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.FindById(User.Identity.GetUserId());
            Cliente cliente = db.Cliente.Where(c => c.Id_users == user.Id).FirstOrDefault();
            //int metodoPago = int.Parse(Request.Form["metodoPago"]);
            //int metodoP = int.Parse(tipoPago);
            Ventas venta = new Ventas
            {
                Cliente = cliente,
                Status = 1,
                DetalleVentas = detalle,
                FechaVenta = DateTime.Now,
                Total = (decimal) total,
                TipoPago = tipoPago

            };
            carro.Clear();
            db.Ventas.Add(venta);
            db.SaveChangesAsync();
            Session["carro"] = null;
            TempData["Compra"] = "Compra completa";
            return RedirectToAction("Index");
        }

       

    }
}