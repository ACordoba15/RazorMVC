using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RazorMVC.Models;

namespace RazorMVC.Controllers
{
    public class SucursalController : Controller
    {
        // GET: Sucursal
        public ActionResult Index()
        {
            List<SucursalCLS> listaSucursal = null;
            using (var bd = new BDPasajeEntities())
            {
                listaSucursal = (
                    from sucursal in bd.Sucursal
                    where sucursal.BHABILITADO == 1
                    select new SucursalCLS
                    {
                        idSucursal = sucursal.IIDSUCURSAL,
                        nombreSucursal = sucursal.NOMBRE,
                        telefonoSucursal = sucursal.TELEFONO.ToString(),
                        emailSucursal = sucursal.EMAIL,
                        fechaApertura = (DateTime)sucursal.FECHAAPERTURA
                    }).ToList();
            }
            return View(listaSucursal);
        }

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(SucursalCLS sucursalCLS)
        {
            int nRegistrosEncontrados = 0;
            string nombre = sucursalCLS.nombreSucursal;
            using (var bd = new BDPasajeEntities())
            {
                nRegistrosEncontrados = bd.Sucursal.Where(p => p.NOMBRE.Equals(nombre)).Count();
            }

            // Si no es valido, retornará a la misma ventana
            if (!ModelState.IsValid || nRegistrosEncontrados >= 1)
            {
                if(nRegistrosEncontrados >=1)
                {
                    sucursalCLS.mensajeError = "Ya existe este nombre";
                }
                return View(sucursalCLS);
            }

            else
            {
                using (var bd = new BDPasajeEntities())
                {
                    Sucursal sucursal = new Sucursal();
                    sucursal.NOMBRE = sucursalCLS.nombreSucursal;
                    sucursal.DIRECCION = sucursalCLS.direccionSucursal;
                    sucursal.TELEFONO = sucursalCLS.telefonoSucursal;
                    sucursal.EMAIL = sucursalCLS.emailSucursal;
                    sucursal.FECHAAPERTURA = sucursalCLS.fechaApertura;
                    sucursal.BHABILITADO = 1;
                    bd.Sucursal.Add(sucursal);
                    bd.SaveChanges();
                }
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            SucursalCLS sucursalCLS = new SucursalCLS();
            using (var bd = new BDPasajeEntities())
            {
                Sucursal sucursal = bd.Sucursal.Where(p => p.IIDSUCURSAL.Equals(id)).First();
                sucursalCLS.idSucursal = sucursal.IIDSUCURSAL;
                sucursalCLS.nombreSucursal = sucursal.NOMBRE;
                sucursalCLS.direccionSucursal = sucursal.DIRECCION;
                sucursalCLS.telefonoSucursal = sucursal.TELEFONO;
                sucursalCLS.emailSucursal = sucursal.EMAIL;
                sucursalCLS.fechaApertura = (DateTime) sucursal.FECHAAPERTURA;
            }
                
            return View(sucursalCLS);
        }


        [HttpPost]
        public ActionResult Editar(SucursalCLS sucursalCLS)
        {
            int nRegistrosEncontrados = 0;
            string nombre = sucursalCLS.nombreSucursal;
            int id = sucursalCLS.idSucursal;
            using (var bd = new BDPasajeEntities())
            {
                nRegistrosEncontrados = bd.Sucursal.Where(p => p.NOMBRE.Equals(nombre) && !p.IIDSUCURSAL.Equals(id)).Count();
            }

            if (!ModelState.IsValid || nRegistrosEncontrados >= 1)
            {
                if (nRegistrosEncontrados >= 1)
                {
                    sucursalCLS.mensajeError = "Ya existe este nombre";
                }
                return View(sucursalCLS);
            }

            else
            {
                int idSucursal = sucursalCLS.idSucursal;

                using (var bd = new BDPasajeEntities())
                {
                    Sucursal sucursal = bd.Sucursal.Where(p => p.IIDSUCURSAL.Equals(idSucursal)).First();
                    sucursal.NOMBRE = sucursalCLS.nombreSucursal;
                    sucursal.DIRECCION = sucursalCLS.direccionSucursal;
                    sucursal.TELEFONO = sucursalCLS.telefonoSucursal;
                    sucursal.EMAIL = sucursalCLS.emailSucursal;
                    sucursal.FECHAAPERTURA = sucursalCLS.fechaApertura;
                    bd.SaveChanges();
                }

                return RedirectToAction("Index");
            }

        }

        public ActionResult Eliminar(int id)
        {
            using (var bd = new BDPasajeEntities())
            {
                Sucursal sucursal = bd.Sucursal.Where(p => p.IIDSUCURSAL.Equals(id)).First();
                sucursal.BHABILITADO = 0;
                bd.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}