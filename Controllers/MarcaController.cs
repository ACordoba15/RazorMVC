using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RazorMVC.Models;

namespace RazorMVC.Controllers
{
    public class MarcaController : Controller
    {
        // GET: Marca
        public ActionResult Index()
        {
            // Se crea una conexión con la base de datos que extrae los datos
            // de la tabla Marca.
            List<MarcaCLS> listaMarca = null;
            using (var bd = new BDPasajeEntities())
            {
                // Recorre cada fila que tiene la tabla marca.
                // variable de rango. 
                listaMarca = (
                    from marca in bd.Marca
                    where marca.BHABILITADO == 1
                    select new MarcaCLS
                    {
                        idMarca = marca.IIDMARCA,
                        nombreMarca = marca.NOMBRE,
                        descripcionMarca = marca.DESCRIPCION,

                    }).ToList();
            }

            return View(listaMarca);
        }

        // Crea la vista Agregar, al llamar con el action link se llama a esta vista
        public ActionResult Agregar()
        {
            return View();
        }


        // Hace la inserción
        [HttpPost]
        public ActionResult Agregar(MarcaCLS marcaCLS)
        {
            // Verifico cuantas veces existe el nombre que queremos agregar a la bd
            int nRegistrosEncontrados = 0;
            string nombreMarca = marcaCLS.nombreMarca;
            using (var bd = new BDPasajeEntities())
            {
                nRegistrosEncontrados = bd.Marca.Where(p => p.NOMBRE.Equals(nombreMarca)).Count();
            }

            // --------------

            if (!ModelState.IsValid || nRegistrosEncontrados >=1)
            {
                if(nRegistrosEncontrados >=1)
                {
                    marcaCLS.mensajeError = "El nombre marca ya existe";
                }
                return View(marcaCLS);
            }

            else
            {
                using (var bd = new BDPasajeEntities())
                {
                    Marca marca = new Marca();
                    marca.NOMBRE = marcaCLS.nombreMarca;
                    marca.DESCRIPCION = marcaCLS.descripcionMarca;
                    marca.BHABILITADO = 1;
                    bd.Marca.Add(marca);
                    bd.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        // Este método pone los datos en la vista editar
        public ActionResult Editar(int id)
        {
            MarcaCLS marcaCLS = new MarcaCLS();
            using (var bd = new BDPasajeEntities())
            {
                // where devuelde una lista, se pone first para el primer elemento
                Marca marca = bd.Marca.Where(p => p.IIDMARCA.Equals(id)).First();
                marcaCLS.idMarca = marca.IIDMARCA;
                marcaCLS.nombreMarca = marca.NOMBRE;
                marcaCLS.descripcionMarca = marca.DESCRIPCION;
            }

            return View(marcaCLS);
        }


        // Este método guarda los cambios
        [HttpPost]
        public ActionResult Editar(MarcaCLS marcaCLS)
        {
            // Verifico cuantas veces existe el nombre que queremos agregar a la bd
            int nRegistrosEncontrados = 0;
            string nombreMarca = marcaCLS.nombreMarca;
            int id = marcaCLS.idMarca;
            using (var bd = new BDPasajeEntities())
            {
                nRegistrosEncontrados = bd.Marca.Where(p => p.NOMBRE.Equals(nombreMarca) && !p.IIDMARCA.Equals(id)).Count();
            }

            if (!ModelState.IsValid || nRegistrosEncontrados >= 1)
            {
                if(nRegistrosEncontrados >= 1)
                {
                    marcaCLS.mensajeError = "Ya se encuentra registrada la marca";
                }
                return View(marcaCLS);
            }

            else
            {
                int idMarca = marcaCLS.idMarca;
                using (var bd = new BDPasajeEntities())
                {
                    Marca marca = bd.Marca.Where(p => p.IIDMARCA.Equals(idMarca)).First();
                    marca.NOMBRE = marcaCLS.nombreMarca;
                    marca.DESCRIPCION = marcaCLS.descripcionMarca;
                    bd.SaveChanges();
                }

                return RedirectToAction("Index");
            }
        }

        public ActionResult Eliminar(int id)
        {
            using (var bd = new BDPasajeEntities())
            {
                Marca marca = bd.Marca.Where(p => p.IIDMARCA.Equals(id)).First();
                marca.BHABILITADO = 0;
                bd.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}