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
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ecommerce.Controllers
{
    public class EmpleadosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Empleados
        /*if (User.Identity.IsAuthenticated) {
        var iduser = User.Identity.GetUserId();
        Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Administrador de recursos humanos") || user.Puesto.Equals("Director Administrativo"))) { 
                
                }
                return RedirectToAction("Denegate","Empleados", user);

            }
            return View();*/

        [Authorize (Roles ="Empleado")]
        public async Task<ActionResult> Home()
        {

            var iduser = User.Identity.GetUserId();
            Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

            return View(user);
        }

        [Authorize(Roles = "Empleado, Administrador")]
        public async Task<ActionResult> Index()
        {
            //Administrador de recursos humanos Director Administrativo

            if (User.Identity.IsAuthenticated) {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && user.Puesto.Equals("Director Administrativo")) {
                    ViewBag.Usuarios = db.Users.ToList();
                    ViewBag.Roles = db.Roles.ToList();

                    return View("index", await db.Empleados.ToListAsync());
                }
                return RedirectToAction("Denegate", "Empleados", user);
            }
            return View();
        }

        [Authorize(Roles = "Empleado")]
        public async Task<ActionResult> IndexRH()
        {
            //Administrador de recursos humanos Director Administrativo

            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Administrador de recursos humanos") || user.Puesto.Equals("Director Administrativo")))
                {
                    return View("indexRH", await db.Empleados.ToListAsync());
                }
                return RedirectToAction("Denegate","Empleados", user);
            }
            return View();

        }

        public ActionResult Denegate(Empleados user) {
            return View("Denegate", user);
        }

        // GET: Empleados/Details/5
        [Authorize(Roles = "Empleado")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();


                if (user.Active && (user.Puesto.Equals("Administrador de recursos humanos") || user.Puesto.Equals("Director Administrativo")))
                {
                    ViewBag.privilegio = "ninguno";
                    if (user.Area.Equals("Recursos Humanos"))
                    {
                        ViewBag.privilegio = "RH";
                    }
                    else if (user.Puesto.Equals("Director Administrativo"))
                    {
                        ViewBag.privilegio = "AD";
                    }

                    Empleados empleados = await db.Empleados.FindAsync(id);
                    if (empleados == null)
                    {
                        return HttpNotFound();
                    }
                    return View(empleados);
                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            return View();

        }

        public ActionResult ModificaEstado(int id)
        {
            Empleados empleado = db.Empleados.Find(id);

            empleado.Active = !empleado.Active;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Empleados/Create

        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && user.Puesto.Equals("Director Administrativo"))
                {
                    ViewBag.Roles = db.Roles.ToList();
                    return View();
                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            return View();
            
        }

        // POST: Empleados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Create([Bind(Include = "Id,Id_users,Nombre,Sexo,Salario,Puesto,Area,Fecha_Nacimeinto,Estado,Municipio,CodigoPostal,Colonia,Calle,NoInterior,NoExterior,Referencia,Active,Registro_completo")] Empleados empleados, string[] roles, string Email, string Password, string ConfirmPassword, string UserName)
        {
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && user.Puesto.Equals("Director Administrativo"))
                {
                    if (ModelState.IsValid)
                    {
                        if (Password.Equals(ConfirmPassword))
                        {
                            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

                            var user2 = new ApplicationUser { UserName = UserName, Email = Email };
                            var result = userManager.Create(user2, Password);



                            if (result.Succeeded)
                            {

                                if (roles != null)
                                {
                                    for (int i = 0; i < roles.Length; i++)
                                    {
                                        userManager.AddToRole(user2.Id, roles[i]);
                                    }
                                }


                                empleados.Active = true;
                                empleados.Id_users = user2.Id;
                                empleados.Fecha_Nacimeinto = new DateTime(1997, 1, 1);
                                db.Empleados.Add(empleados);
                                await db.SaveChangesAsync();
                            }

                        }

                        return RedirectToAction("Index");
                    }
                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            return View(empleados);
        }

        // GET: Empleados/Edit/5
        [Authorize(Roles = "Empleado")]
        public async Task<ActionResult> EditRH(int? id)
        {

            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Administrador de recursos humanos") || user.Puesto.Equals("Director Administrativo")))
                {
                    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    Empleados empleados = await db.Empleados.FindAsync(id);

                    if (empleados == null)
                    {
                        return HttpNotFound();
                    }
                    return View("EditRH", empleados);
                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            return View();
        }
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && user.Puesto.Equals("Director Administrativo"))
                {
                    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    Empleados empleados = await db.Empleados.FindAsync(id);
                    List<string> rolesA = new List<string>();
                    List<string> rolesB = new List<string>();

                    foreach (IdentityRole rol in db.Roles.ToList())
                    {
                        if (userManager.IsInRole(empleados.Id_users, rol.Name))
                        {
                            rolesA.Add(rol.Name);
                        }
                        else
                        {
                            rolesB.Add(rol.Name);
                        }
                    }
                    ViewBag.Usuarios = db.Users.ToList();
                    ViewBag.rolesActuales = rolesA;
                    ViewBag.rolesFaltantes = rolesB;

                    if (empleados == null)
                    {
                        return HttpNotFound();
                    }
                    return View("Edit", empleados);
                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            return View();

            
        }

        // POST: Empleados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Id_users,Nombre,Sexo,Salario,Puesto,Area,Fecha_Nacimeinto,Estado,Municipio,CodigoPostal,Colonia,Calle,NoInterior,NoExterior,Referencia,Active,Registro_completo")] Empleados empleados, string[] roles, string Password, string ConfirmPassword, string UserName)
        {
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Administrador de recursos humanos") || user.Puesto.Equals("Director Administrativo")))
                {
                    if (ModelState.IsValid)
                    {
                        if (Password != null && ConfirmPassword != null && UserName != null)
                        {
                            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

                            var user2 = userManager.FindById(empleados.Id_users);
                            user2.UserName = UserName;

                            if (!Password.Equals(""))
                            {
                                if (Password.Equals(ConfirmPassword))
                                {
                                    userManager.RemovePassword(user2.Id);
                                    userManager.AddPassword(user2.Id, Password);
                                }
                            }

                            if (roles != null)
                            {
                                foreach (IdentityRole rol in db.Roles.ToList())
                                {
                                    if (userManager.IsInRole(user2.Id, rol.Name))
                                    {
                                        userManager.RemoveFromRole(user2.Id, rol.Name);
                                    }
                                }

                                for (int i = 0; i < roles.Length; i++)
                                {

                                    userManager.AddToRole(user2.Id, roles[i]);
                                }
                            }
                        }


                        if (empleados.Salario != 0 && empleados.Puesto != null && empleados.Area != null && empleados.Estado != null && empleados.Municipio != null && empleados.CodigoPostal != 0 && empleados.Colonia != null && empleados.Calle != null)
                        {
                            empleados.Registro_Completo = true;
                        }

                        db.Entry(empleados).State = EntityState.Modified;


                        await db.SaveChangesAsync();

                        var iduser2 = User.Identity.GetUserId();
                        Empleados employee = db.Empleados.Where(p => p.Id_users.Equals(iduser2)).First();

                        if (employee.Area.Equals("Recursos Humanos"))
                        {
                            return RedirectToAction("IndexRH");
                        }
                        else if (employee.Puesto.Equals("Director Administrativo"))
                        {
                            return RedirectToAction("Index");
                        }

                    }
                    return View(empleados);
                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            return View();

            
        }

        // GET: Empleados/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = await db.Empleados.FindAsync(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Empleados empleados = await db.Empleados.FindAsync(id);
            db.Empleados.Remove(empleados);
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
