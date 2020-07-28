using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RazorMVC.Models;

namespace RazorMVC.Controllers
{
    public class BusController : Controller
    {
        // GET: Bus
        public ActionResult Index()
        {
            List<BusCLS> listaBus = null;
            using (var bd = new BDPasajeEntities())
            {
                listaBus = (
                    from bus in bd.Bus
                    join sucursal in bd.Sucursal
                    on bus.IIDSUCURSAL equals sucursal.IIDSUCURSAL
                    join tipoBus in bd.TipoBus
                    on bus.IIDTIPOBUS equals tipoBus.IIDTIPOBUS
                    join tipoModelo in bd.Modelo
                    on bus.IIDMODELO equals tipoModelo.IIDMODELO
                    where bus.BHABILITADO == 1
                    select new BusCLS
                    {
                        idBus = bus.IIDBUS,
                        nombreModelo = tipoModelo.NOMBRE,
                        nombreSucursal = sucursal.NOMBRE,
                        nombreTipoBus = tipoBus.NOMBRE,
                        placaBus = bus.PLACA
                    }).ToList();
            }
                return View(listaBus);
        }

        // Crea la vista
        public ActionResult Agregar()
        {
            ListarCombos();
            return View();
        }

        // Agrega en la base de datos
        [HttpPost]
        public  ActionResult Agregar(BusCLS busCLS)
        {
            int nRegistrosEncontrados = 0;
            string placa = busCLS.placaBus;
            using (var bd = new BDPasajeEntities())
            {
                nRegistrosEncontrados = bd.Bus.Where(p => p.PLACA.Equals(placa)).Count();
            }
            if (!ModelState.IsValid || nRegistrosEncontrados >= 1)
            {
                if(nRegistrosEncontrados>=1)
                {
                    busCLS.mensajeError = "Esta placa ya se encuentra registrada";
                }
                ListarCombos();
                return View(busCLS);
            }

            else
            {
                using (var bd = new BDPasajeEntities())
                {
                    Bus bus = new Bus();
                    bus.BHABILITADO = 1;
                    bus.IIDSUCURSAL = busCLS.idSucursal;
                    bus.IIDTIPOBUS = busCLS.idTipoBus;
                    bus.PLACA = busCLS.placaBus;
                    bus.FECHACOMPRA = busCLS.fechaCompra;
                    bus.IIDMODELO = busCLS.idModelo;
                    bus.NUMEROFILAS = busCLS.numFilas;
                    bus.NUMEROCOLUMNAS = busCLS.numColumnas;
                    bus.DESCRIPCION = busCLS.descripcion;
                    bus.OBSERVACION = busCLS.observacion;
                    bus.IIDMARCA = busCLS.idMarca;
                    bd.Bus.Add(bus);
                    bd.SaveChanges();

                }
                return RedirectToAction("Index");
            }
        }

        public ActionResult Editar(int id)
        {
            BusCLS busCLS = new BusCLS();
            using (var bd = new BDPasajeEntities())
            {
                ListarCombos();

                Bus bus = bd.Bus.Where(p => p.IIDBUS.Equals(id)).First();
                busCLS.idBus = bus.IIDBUS;
                busCLS.idSucursal = (int) bus.IIDSUCURSAL;
                busCLS.idTipoBus = (int) bus.IIDTIPOBUS;
                busCLS.placaBus = bus.PLACA;
                busCLS.fechaCompra = (DateTime) bus.FECHACOMPRA;
                busCLS.idModelo = (int)bus.IIDMODELO;
                busCLS.numFilas = (int) bus.NUMEROFILAS;
                busCLS.numColumnas = (int) bus.NUMEROCOLUMNAS;
                busCLS.descripcion = bus.DESCRIPCION;
                busCLS.observacion = bus.OBSERVACION;
                busCLS.idMarca = (int) bus.IIDMARCA;
            }

            return View(busCLS);
        }

        [HttpPost]
        public ActionResult Editar(BusCLS busCLS)
        {
            int id = busCLS.idBus;
            int nRegistrosEncontrados = 0;
            string placa = busCLS.placaBus;
            using (var bd = new BDPasajeEntities())
            {
                nRegistrosEncontrados = bd.Bus.Where(p => p.PLACA.Equals(placa) && !p.IIDBUS.Equals(id)).Count();
            }
            if (!ModelState.IsValid || nRegistrosEncontrados>=1)
            {
                if(nRegistrosEncontrados>=1)
                {
                    busCLS.mensajeError = "Esta placa ya se encuentra registrada";
                }
                ListarCombos();
                return View(busCLS);
            }

            else
            {
                using (var bd = new BDPasajeEntities())
                {
                    Bus bus = bd.Bus.Where(p => p.IIDBUS.Equals(id)).First();
                    bus.IIDBUS = busCLS.idBus;
                    bus.IIDSUCURSAL = busCLS.idSucursal;
                    bus.IIDTIPOBUS = busCLS.idTipoBus;
                    bus.PLACA = busCLS.placaBus;
                    bus.FECHACOMPRA = busCLS.fechaCompra;
                    bus.IIDMODELO = busCLS.idModelo;
                    bus.NUMEROFILAS = busCLS.numFilas;
                    bus.NUMEROCOLUMNAS = busCLS.numColumnas;
                    bus.DESCRIPCION = busCLS.descripcion;
                    bus.OBSERVACION = busCLS.observacion;
                    bus.IIDMARCA = busCLS.idMarca;

                    bd.SaveChanges();
                }

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Eliminar (int idBus)
        {
            using (var bd = new BDPasajeEntities())
            {
                Bus bus = bd.Bus.Where(p => p.IIDBUS.Equals(idBus)).First();
                bus.BHABILITADO = 0;
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public void ListarCombos()
        {
            ListaComboMarca();
            ListaComboNombreModelo();
            ListaComboNombreSucursal();
            ListaComboTipoBus();
        }

        public void ListaComboMarca()
        {
            // LLena los combo, se hace una lista de tipo listItem para pasarlos al combo
            // Agregar
            List<SelectListItem> listaMarca;
            using (var bd = new BDPasajeEntities())
            {
                listaMarca = (
                    from marca in bd.Marca
                    where marca.BHABILITADO == 1
                    select new SelectListItem
                    {
                        Text = marca.NOMBRE,
                        Value = marca.IIDMARCA.ToString()
                    }).ToList();
                //  Inserta en la primera posición
                listaMarca.Insert(0, new SelectListItem { Text = "Seleccione una marca ", Value = "" });
                ViewBag.listaMarca = listaMarca;
            }
        }

        public void ListaComboTipoBus()
        {
            // LLena los combo, se hace una lista de tipo listItem para pasarlos al combo
            // Agregar
            List<SelectListItem> listaTipoBus;
            using (var bd = new BDPasajeEntities())
            {
                listaTipoBus = (
                    from tipoBus in bd.TipoBus
                    where tipoBus.BHABILITADO == 1
                    select new SelectListItem
                    {
                        Text = tipoBus.NOMBRE,
                        Value = tipoBus.IIDTIPOBUS.ToString()
                    }).ToList();
                //  Inserta en la primera posición
                listaTipoBus.Insert(0, new SelectListItem { Text = "Seleccione un tipo de bus ", Value = "" });
                ViewBag.listaTipoBus = listaTipoBus;
            }
        }

        public void ListaComboNombreModelo()
        {
            // LLena los combo, se hace una lista de tipo listItem para pasarlos al combo
            // Agregar
            List<SelectListItem> listaModelo;
            using (var bd = new BDPasajeEntities())
            {
                listaModelo = (
                    from modelo in bd.Modelo
                    where modelo.BHABILITADO == 1
                    select new SelectListItem
                    {
                        Text = modelo.NOMBRE,
                        Value = modelo.IIDMODELO.ToString()
                    }).ToList();
                //  Inserta en la primera posición
                listaModelo.Insert(0, new SelectListItem { Text = "Seleccione un modelo ", Value = "" });
                ViewBag.listaModelo = listaModelo;
            }
        }

        public void ListaComboNombreSucursal()
        {
            // LLena los combo, se hace una lista de tipo listItem para pasarlos al combo
            // Agregar
            List<SelectListItem> listaSucursal;
            using (var bd = new BDPasajeEntities())
            {
                listaSucursal = (
                    from sucursal in bd.Sucursal
                    where sucursal.BHABILITADO == 1
                    select new SelectListItem
                    {
                        Text = sucursal.NOMBRE,
                        Value = sucursal.IIDSUCURSAL.ToString()
                    }).ToList();
                //  Inserta en la primera posición
                listaSucursal.Insert(0, new SelectListItem { Text = "Seleccione una sucursal ", Value = "" });
                ViewBag.listaSucursal = listaSucursal;
            }
        }
    }
}