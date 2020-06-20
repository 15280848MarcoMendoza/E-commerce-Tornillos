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


            var user_rh = new ApplicationUser { UserName = "recursoshumanos", Email = "recursoshumanos@gmail.com" };
            var rh = userManager.Create(user_rh, "recursoshumanos");

            if (rh.Succeeded) {

                userManager.AddToRole(user_rh.Id, "Empleado");

                Empleados w_rh = new Empleados();
                w_rh.Id_users = user_rh.Id;
                w_rh.Nombre = "Daniel Alejandro León Vélez";
                w_rh.Sexo = false;
                w_rh.Salario = 9000;
                w_rh.Puesto = "Administrador de recursos humanos";
                w_rh.Area = "Recursos Humanos";
                w_rh.Fecha_Nacimeinto = new DateTime(1998, 8, 29);
                w_rh.Estado = "Mexico";
                w_rh.Municipio = "Chimalhuaacan";
                w_rh.CodigoPostal = 51680;
                w_rh.Colonia = "Los Patos";
                w_rh.Calle = "5 de mayo";
                w_rh.NoInterior = 1;
                w_rh.NoExterior = 1;
                w_rh.Referencia = "Puerta azul";
                w_rh.Registro_Completo= true;
                w_rh.Active = true;

                db.Empleados.AddOrUpdate(w_rh);
            }

            

            var user_fi = new ApplicationUser();
            user_fi.Email = "finanzas@gmail.com";
            user_fi.UserName = "finanzas";
            var fi = userManager.Create(user_fi, "finanzas");

            if (fi.Succeeded)
            {

                userManager.AddToRole(user_fi.Id, "Empleado");

                Empleados w_rh = new Empleados();
                w_rh.Id_users = user_fi.Id;
                w_rh.Nombre = "Alejandro Gutiérrez Castañeda";
                w_rh.Sexo = true;
                w_rh.Salario = 10500;
                w_rh.Puesto = "Control finanzas";
                w_rh.Area = "Finanzas";
                w_rh.Fecha_Nacimeinto = new DateTime(1985, 6, 15);
                w_rh.Estado = "Mexico";
                w_rh.Municipio = "Metepec";
                w_rh.CodigoPostal = 52156;
                w_rh.Colonia = "Metepec";
                w_rh.Calle = "15 de mayo";
                w_rh.NoInterior = 5;
                w_rh.NoExterior = 2;
                w_rh.Referencia = "Puerta negra";
                w_rh.Registro_Completo = true;
                w_rh.Active = true;

                db.Empleados.AddOrUpdate(w_rh);
            }

            var user_al = new ApplicationUser();
            user_al.Email = "almacen@gmail.com";
            user_al.UserName = "almacen";
            var al = userManager.Create(user_al, "almacen");

            if (al.Succeeded)
            {

                userManager.AddToRole(user_al.Id, "Empleado");

                Empleados w_rh = new Empleados();
                w_rh.Id_users = user_al.Id;
                w_rh.Nombre = "Gustavo Castelán Barrios";
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
                w_rh.Nombre = "Alejandro Gutiérrez Castañeda";
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
            Catalogos preparado = new Catalogos { Id = 1, name = "Preparado" };
            Catalogos cajas = new Catalogos { Id = 2, name = "Cajas" };
            Catalogos diabeticos = new Catalogos { Id = 4, name = "Diabéticos" };
            Catalogos relajantes = new Catalogos { Id = 5, name = "Relajantes" };
            Catalogos medicinales = new Catalogos { Id = 6, name = "Medicinales" };
            
            db.Catalogos.AddOrUpdate(preparado);
            db.Catalogos.AddOrUpdate(cajas);
            db.Catalogos.AddOrUpdate(diabeticos);
            db.Catalogos.AddOrUpdate(relajantes);
            db.Catalogos.AddOrUpdate(medicinales);
           

            Productos producto1 = new Productos
            {
                Id = 1,
                Nombre = "Té de anís",
                Descripcion = "té a base de ingredientes 100% naturales",
                Url_image = "images/teanis.jpg",
                Sabor = "anís",
                Marca = "TÉ-NDULSO",
                Costo_unitario = 6.5,
                Porcentage_descuento = 5,
                Status = 1,
                Precio_final = 8,
                activo = true,
                Cantidad_ventas = 3,
                Time_Mount = 12,
                Time_Day = 31,
                Catalogos = new List<Catalogos> { preparado, diabeticos }
            };

            Productos producto2 = new Productos
            {
                Id = 2,
                Nombre = "Té de canela",
                Descripcion = "té a base de ingredientes 100% naturales",
                Url_image = "images/tecanela.jpg",
                Sabor = "canela",
                Marca = "TÉ-NDULSO",
                Costo_unitario = 10,
                Porcentage_descuento = 0,
                Status = 1,
                Precio_final = 12,
                activo = true,
                Cantidad_ventas = 3,
                Time_Mount = 11,
                Time_Day = 4,
                Catalogos = new List<Catalogos> { preparado, relajantes }


            };

            Productos producto3 = new Productos
            {
                Id = 3,
                Nombre = "Té de boldo",
                Descripcion = "té a base de ingredientes 100% naturales",
                Url_image = "images/teboldo.jpg",
                Sabor = "boldo",
                Marca = "TÉ-NDULSO",
                Costo_unitario = 5,
                Porcentage_descuento = 0,
                Status = 1,
                Precio_final = 10,
                activo = true,
                Cantidad_ventas = 3,
                Time_Mount = 1,
                Time_Day = 1,
                Catalogos = new List<Catalogos> { cajas, preparado,diabeticos }
            };

            Productos producto4 = new Productos
            {
                Id = 4,
                Nombre = "Té de cardamomo",
                Descripcion = "té a base de ingredientes 100% naturales",
                Url_image = "images/tecardamomo.jpg",
                Sabor = "cardamomo",
                Marca = "TÉ-NDULSO",
                Costo_unitario = 6,
                Porcentage_descuento = 0,
                Status = 1,
                Precio_final = 12,
                activo = true,
                Cantidad_ventas = 3,
                Time_Mount = 3,
                Time_Day = 3,
                Catalogos = new List<Catalogos> { preparado, cajas,medicinales }
            };

            Productos producto5 = new Productos
            {
                Id = 5,
                Nombre = "Té de citronela",
                Descripcion = "té a base de ingredientes 100% naturales",
                Url_image = "images/tecitronela.jpg",
                Sabor = "citronela",
                Marca = "TÉ-NDULSO",
                Costo_unitario = 12,
                Porcentage_descuento = 0,
                Status = 1,
                Precio_final = 20,
                activo = true,
                Cantidad_ventas = 3,
                Time_Mount = 5,
                Time_Day = 7,
                Catalogos = new List<Catalogos> { preparado, cajas,medicinales }

            };

            Productos producto6 = new Productos
            {
                Id = 6,
                Nombre = "Té negro",
                Descripcion = "té a base de ingredientes 100% naturales",
                Url_image = "images/tenegro.jpg",
                Sabor = "negro",
                Marca = "TÉ-NDULSO",
                Costo_unitario = 10,
                Porcentage_descuento = 0,
                Status = 1,
                Precio_final = 15,
                activo = true,
                Cantidad_ventas = 3,
                Time_Mount = 3,
                Time_Day = 23,
                Catalogos = new List<Catalogos> { preparado, cajas,diabeticos }
            };

            Productos producto7 = new Productos
            {
                Id = 7,
                Nombre = "Té de jengibre",
                Descripcion = "té a base de ingredientes 100% naturales",
                Url_image = "images/tejengibre.jpg",
                Sabor = "jengibre",
                Marca = "TÉ-NDULSO",
                Costo_unitario = 5,
                Porcentage_descuento = 0,
                Status = 1,
                Precio_final = 9,
                activo = true,
                Cantidad_ventas = 3,
                Time_Mount = 5,
                Time_Day = 23,
                Catalogos = new List<Catalogos> { diabeticos, cajas, preparado }
            };

            Productos producto8 = new Productos
            {
                Id = 8,
                Nombre = "Té de manzanilla",
                Descripcion = "té a base de ingredientes 100% naturales",
                Url_image = "images/temanzanilla.jpg",
                Sabor = "manzanilla",
                Marca = "TÉ-NDULSO",
                Costo_unitario = 10,
                Porcentage_descuento = 0,
                Status = 1,
                Precio_final = 15,
                activo = true,
                Cantidad_ventas = 3,
                Time_Mount = 10,
                Time_Day = 23,
                Catalogos = new List<Catalogos> { cajas, preparado,medicinales }
            };

            Productos producto9 = new Productos
            {
                Id = 9,
                Nombre = "Té de menta",
                Descripcion = "té a base de ingredientes 100% naturales",
                Url_image = "images/tementa.jpg",
                Sabor = "menta",
                Marca = "TÉ-NDULSO",
                Costo_unitario = 6,
                Porcentage_descuento = 0,
                Status = 1,
                Precio_final = 10,
                activo = true,
                Cantidad_ventas = 3,
                Time_Mount = 11,
                Time_Day = 13,
                Catalogos = new List<Catalogos> { cajas, preparado, relajantes }
            };

            Productos producto10 = new Productos
            {
                Id = 10,
                Nombre = "Té de romero",
                Descripcion = "té a base de ingredientes 100% naturales",
                Url_image = "images/teromero.jpg",
                Sabor = "romero",
                Marca = "TÉ-NDULSO",
                Costo_unitario = 15,
                Porcentage_descuento = 0,
                Status = 1,
                Precio_final = 25,
                activo = true,
                Cantidad_ventas = 10,
                Time_Mount = 12,
                Time_Day = 31,
                Catalogos = new List<Catalogos> { cajas, preparado,diabeticos }
            };

            db.Productos.AddOrUpdate(producto1);
            db.Productos.AddOrUpdate(producto2);
            db.Productos.AddOrUpdate(producto3);
            db.Productos.AddOrUpdate(producto4);
            db.Productos.AddOrUpdate(producto5);
            db.Productos.AddOrUpdate(producto6);
            db.Productos.AddOrUpdate(producto7);
            db.Productos.AddOrUpdate(producto8);
            db.Productos.AddOrUpdate(producto9);
            db.Productos.AddOrUpdate(producto10);

        }
        
       
        private void SeedProveedores(ApplicationDbContext db)
        {
            Provedores prove1 = new Provedores();
            prove1.Id = 1;
            prove1.Nombre = "Hierbas medicinales";
            prove1.Telefono = "7224124088";
            prove1.Correo = "hierbasmed@hotmail.com";
            Provedores prove2 = new Provedores();
            prove2.Id = 2;
            prove2.Nombre = "Oleoespecias";
            prove2.Telefono = "7171717171";
            prove2.Correo = "oleoespeciasy@hotmail.com";
            db.Provedores.AddOrUpdate(prove1);
            db.Provedores.AddOrUpdate(prove2);
        }
   
    }
}
