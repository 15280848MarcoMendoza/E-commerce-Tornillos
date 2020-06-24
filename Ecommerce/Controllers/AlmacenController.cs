using System;
using Ecommerce.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Text.RegularExpressions;
using PagedList;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Ecommerce.Controllers
{
    public class AlmacenController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Almacen
        [Authorize(Roles = "Empleado")]
        // GET: Productos
        public async Task<ActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control de almacen") || user.Puesto.Equals("Director Administrativo")))
                {
                    return View(await db.Productos.ToListAsync());

                }
                return RedirectToAction("Denegate", "Empleados", user);
            }
            return View();

        }


        public ActionResult Detalle_Venta(string searchBy, string currentsearch, string search, string currentFilter, int? page)
        {
        
                    var detalleventa = db.DetalleVentas.AsQueryable();
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
                detalleventa = detalleventa.Where(x => x.Nombre.ToString() == search || search == null);
                    }
                    else if (searchBy == "Id")
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
                detalleventa = detalleventa.Where(x => x.Id.ToString().StartsWith(status.ToString()) || status == null);
                    }


            return View(detalleventa.ToList());

        }
       

        // GET: Mensajes/Edit/5
        public async Task<ActionResult> EditMen(int? id)
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
            ViewBag.nombrecliente = db.DetalleVentas.Find(id).Ventas.Cliente.Nombre;
            ViewBag.direccioncliente = db.DetalleVentas.Find(id).Ventas.Cliente.Calle;
            ViewBag.producto = db.DetalleVentas.Find(id).Producto.Nombre;
            ViewBag.municipio = db.DetalleVentas.Find(id).Ventas.Cliente.Municipio;
            ViewBag.estado = db.DetalleVentas.Find(id).Ventas.Cliente.Estado;
            ViewBag.peso = db.DetalleVentas.Find(id).Ventas.Kg;
            ViewBag.telefono = db.DetalleVentas.Find(id).Ventas.Cliente.telefono;
            ViewBag.correo = db.DetalleVentas.Find(id).Ventas.Cliente.correo;
            ViewBag.id = db.DetalleVentas.Find(id).Id;
            return View(mensajes);
        }

        // POST: Mensajes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditMen([Bind(Include = "Id,NombrePaqueteria,FechaEntrega,Direccion,Remitente,Cliente,Telefono,Correo,Peso,Producto")] Mensajes mensajes)
        {
            int id;
            if (ModelState.IsValid)
            {

                db.Entry(mensajes).State = EntityState.Modified;
                id = db.Mensajes.Find(mensajes.Id).Id;
               
                await db.SaveChangesAsync();
                id = db.Mensajes.Find(mensajes.Id).Id;
                return RedirectToAction("ReporteSalida", "Almacen", new { Id = db.DetalleVentas.Find(id).Id, Fecha = db.Mensajes.Find(mensajes.Id).FechaEntrega });
               
            }
            id = db.Mensajes.Find(mensajes.Id).Id;
            return RedirectToAction("ReporteSalida", "Almacen", new { Id = db.DetalleVentas.Find(id).Id });
        }

        public ActionResult ReporteSalidaMenu(string searchBy, string currentsearch, string search, string currentFilter, int? page)
        {

            var detalleventa = db.DetalleVentas.AsQueryable();
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
                detalleventa = detalleventa.Where(x => x.Nombre.ToString() == search || search == null);
            }
            else if (searchBy == "Id")
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
                detalleventa = detalleventa.Where(x => x.Id.ToString().StartsWith(status.ToString()) || status == null);
            }


            return View(detalleventa.ToList());

        }

        // GET: ReporteSalidas/Create
        public ActionResult ReporteSalida(int Id,string Fecha)
        {

            
            ViewBag.cantidad = db.DetalleVentas.Find(Id).Cantidad;
            ViewBag.producto = db.DetalleVentas.Find(Id).Producto.Nombre;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ReporteSalida([Bind(Include = "Id,Nombre,Fecha,Producto,Cantidad")] ReporteSalida reporteSalida,int Id)
        {
           
            ICollection <DetalleVentaReporteSalida> detalle = new List<DetalleVentaReporteSalida>();

            DetalleVenta deVenta = db.DetalleVentas.Find(Id);

            deVenta.DetalleVentaReporteSalida = detalle;

            DetalleVentaReporteSalida dvrp = new DetalleVentaReporteSalida
            {
                DetalleVenta = deVenta
            };

            detalle.Add(dvrp);
            if (ModelState.IsValid)
            {
                
                //db.ReporteSalida.Add(reporteSalida);
                //reporteSalida.DetalleVentaReporteSalida = detalle;
                //await db.SaveChangesAsync();
                return RedirectToAction("ActualizarInventarioMenu");
            }
            return RedirectToAction("ActualizarInventarioMenu");
        }


        public ActionResult ActualizarInventarioMenu(string searchBy, string currentsearch, string search, string currentFilter, int? page)
        {

            var detalleventaReporteSalida = db.DetalleVentaReporteSalida.AsQueryable();
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
                detalleventaReporteSalida = detalleventaReporteSalida.Where(x => x.ReporteSalida.Nombre.ToString() == search || search == null);
            }
            else if (searchBy == "Id")
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
                detalleventaReporteSalida = detalleventaReporteSalida.Where(x => x.Id.ToString().StartsWith(status.ToString()) || status == null);
            }


            return View(detalleventaReporteSalida.ToList());

        }


        public async Task<ActionResult> ActualizarInventario(int? id)
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
        public async Task<ActionResult> ActualizarInventario(int[] Catalogos, [Bind(Include = "Id,Nombre,Descripcion,Marca,Precio_final,Url_image,Cantidad_ventas,stock")] Productos productos)
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
            

            if (ModelState.IsValid)
            {



                db.Entry(productos).State = EntityState.Modified;
                await db.SaveChangesAsync();

            }
            return RedirectToAction("ActualizarInventarioMenu");
        }

    }
}