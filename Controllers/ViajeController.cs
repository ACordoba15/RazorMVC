using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RazorMVC.Models;

namespace RazorMVC.Controllers
{
    public class ViajeController : Controller
    {
        // GET: Viaje
        public ActionResult Index()
        {
            List<ViajeCLS> listaViaje = null;
            using (var bd = new BDPasajeEntities())
            {
                listaViaje = (
                    from viaje in bd.Viaje
                    join lugarOrigen in bd.Lugar
                    on viaje.IIDLUGARORIGEN equals lugarOrigen.IIDLUGAR
                    join lugarDestino in bd.Lugar
                    on viaje.IIDLUGARDESTINO equals lugarDestino.IIDLUGAR
                    join bus in bd.Bus
                    on viaje.IIDBUS equals bus.IIDBUS
                    select new ViajeCLS 
                    { 
                        idViaje = viaje.IIDVIAJE,
                        nombreBus = bus.PLACA,
                        nombreLugarO = lugarOrigen.NOMBRE,
                        nombreLugarD = lugarDestino.NOMBRE
                    }).ToList(); 
            }
                return View(listaViaje);
        }

        public ActionResult Agregar()
        {
            ListasCombos();
            return View();
        }


        public void ListaComboLugar()
        {
            // LLena los combo, se hace una lista de tipo listItem para pasarlos al combo
            // Agregar
            List<SelectListItem> listaLugar;
            using (var bd = new BDPasajeEntities())
            {
                listaLugar = (
                    from lugar in bd.Lugar
                    where lugar.BHABILITADO == 1
                    select new SelectListItem
                    {
                        Text = lugar.NOMBRE,
                        Value = lugar.IIDLUGAR.ToString()
                    }).ToList();
                //  Inserta en la primera posición
                listaLugar.Insert(0, new SelectListItem { Text = "Seleccione un lugar ", Value = "" });
                ViewBag.listaLugar = listaLugar;
            }
        }

        public void ListaComboBus()
        {
            // LLena los combo, se hace una lista de tipo listItem para pasarlos al combo
            // Agregar
            List<SelectListItem> listaBus;
            using (var bd = new BDPasajeEntities())
            {
                listaBus = (
                    from bus in bd.Bus
                    where bus.BHABILITADO == 1
                    select new SelectListItem
                    {
                        Text = bus.PLACA,
                        Value = bus.IIDBUS.ToString()
                    }).ToList();
                //  Inserta en la primera posición
                listaBus.Insert(0, new SelectListItem { Text = "Seleccione un bus ", Value = "" });
                ViewBag.listaBus = listaBus;
            }
        }

        public void ListasCombos()
        {
            ListaComboLugar();
            ListaComboBus();
        }
    }
}