namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Ecommerce.Models;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<Ecommerce.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Ecommerce.Models.ApplicationDbContext context)
        {
            SeedCatagoProductos(context);           
            SeedEmpleados(context);
        }

        private void SeedEmpleados(ApplicationDbContext db) {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            roleManager.Create(new IdentityRole("Administrador"));
            roleManager.Create(new IdentityRole("Empleado"));



            var user_al = new ApplicationUser();
            user_al.Email = "almacen@gmail.com";
            user_al.UserName = "almacen";
            var al = userManager.Create(user_al, "almacen");

            if (al.Succeeded)
            {

                userManager.AddToRole(user_al.Id, "Empleado");

                Empleados w_rh = new Empleados();
                w_rh.Id_users = user_al.Id;
                w_rh.Nombre = "Luis Daniel Lopez Arias";
                w_rh.Sexo = true;
                w_rh.Salario = 8500;
                w_rh.Puesto = "Control de almacen";
                w_rh.Area = "Almacen";
                w_rh.Fecha_Nacimeinto = new DateTime(1995, 10, 30);
                w_rh.Estado = "Mexico";
                w_rh.Municipio = "Metepec";
                w_rh.CodigoPostal = 52156;
                w_rh.Colonia = "Metepec";
                w_rh.Calle = "25 de mayo";
                w_rh.NoInterior = 2;
                w_rh.NoExterior = 5;
                w_rh.Referencia = "Puerta amarilla";
                w_rh.Registro_Completo = true;
                w_rh.Active = true;

                db.Empleados.AddOrUpdate(w_rh);
            }

            var user_ad = new ApplicationUser();
            user_ad.Email = "administrador@gmail.com";
            user_ad.UserName = "administrador";
            var ad = userManager.Create(user_ad, "administrador");

            if (ad.Succeeded)
            {

                userManager.AddToRoles(user_ad.Id, "Administrador", "Empleado");

                Empleados w_rh = new Empleados();
                w_rh.Id_users = user_ad.Id;
                w_rh.Nombre = "Administrador";
                w_rh.Sexo = false;
                w_rh.Salario = 11500;
                w_rh.Puesto = "Director Administrativo";
                w_rh.Area = "Direccion";
                w_rh.Fecha_Nacimeinto = new DateTime(1987, 1, 25);
                w_rh.Estado = "Mexico";
                w_rh.Municipio = "Metepec";
                w_rh.CodigoPostal = 52789;
                w_rh.Colonia = "Metepec";
                w_rh.Calle = "25 de enero";
                w_rh.NoInterior = 9;
                w_rh.NoExterior = 9;
                w_rh.Referencia = "Puerta morada";
                w_rh.Registro_Completo = true;
                w_rh.Active = true;

                db.Empleados.AddOrUpdate(w_rh);
            }

        }

        private void SeedCatagoProductos(ApplicationDbContext db)
        {
            Catalogos ordinarios = new Catalogos { Id = 1, name = "orndinarios" };
            Catalogos calibrados = new Catalogos { Id = 2, name = "calibrados" };
            
            db.Catalogos.AddOrUpdate(ordinarios);
            db.Catalogos.AddOrUpdate(calibrados);
            
            
           

            Productos producto1 = new Productos
            {
                Id = 1,
                Nombre = "Tornillo 3/4",
                Descripcion = "tornillo para madera de 3/4",
                Url_image = "images/tornillosnegros.jpg",
                Marca = "HILLMAN",
                Costo_unitario = 6.5,
                Porcentage_descuento = 5,
                Status = 1,
                Precio_final = 8,
                activo = true,
                Cantidad_ventas = 3,
                Time_Mount = 12,
                Time_Day = 31,
                Catalogos = new List<Catalogos> {ordinarios}
            };

          

            db.Productos.AddOrUpdate(producto1);
         

        }
        
       
        private void SeedProveedores(ApplicationDbContext db)
        {
            Provedores prove1 = new Provedores();
            prove1.Id = 1;
            prove1.Nombre = "Hillman";
            prove1.Telefono = "7224124088";
            prove1.Correo = "hierbasmed@hotmail.com";
            Provedores prove2 = new Provedores();
            prove2.Id = 2;
            prove2.Nombre = "Phillips";
            prove2.Telefono = "7171717171";
            prove2.Correo = "oleoespeciasy@hotmail.com";
            db.Provedores.AddOrUpdate(prove1);
            db.Provedores.AddOrUpdate(prove2);
        }

       
        

    }
}
