using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RazorMVC.Models;
namespace RazorMVC.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            List<ClienteCLS> listaCliente = null;
            using (var bd = new BDPasajeEntities())
            {
                listaCliente = ( 
                    from cliente in bd.Cliente
                    where cliente.BHABILITADO == 1
                    select new ClienteCLS
                    {
                        idCliente = cliente.IIDCLIENTE,
                        nombreCliente = cliente.NOMBRE,
                        APaterno = cliente.APPATERNO,
                        AMaterno = cliente.APMATERNO,
                        telefonoFijo = cliente.TELEFONOFIJO
                    }).ToList();
            }

            return View(listaCliente);
        }

        public ActionResult Agregar()
        {
            LlenarSexo();
            ViewBag.listaSexo = listaSexo;
            return View();
        }

        List<SelectListItem> listaSexo;

        private void LlenarSexo()
        {
            using (var bd = new BDPasajeEntities())
            {
                listaSexo = (
                    from sexo in bd.Sexo
                    where sexo.BHABILITADO == 1
                    select new SelectListItem
                    {
                        Text = sexo.NOMBRE,
                        Value = sexo.IIDSEXO.ToString()
                    }).ToList();

                listaSexo.Insert(0, new SelectListItem { Text = "Seleccione su sexo", Value = "" });
            }
        }

        [HttpPost]
        public ActionResult Agregar(ClienteCLS clienteCLS)
        {

            int nRegistrosEncontrados = 0;
            string nombre = clienteCLS.nombreCliente;
            string apellidoP = clienteCLS.APaterno;
            string apellidoM = clienteCLS.AMaterno;
            using (var bd = new BDPasajeEntities())
            {
                nRegistrosEncontrados = bd.Cliente.Where(p => p.NOMBRE.Equals(nombre) && p.APPATERNO.Equals(apellidoP) && p.APMATERNO.Equals(apellidoM)).Count();

            }

            if (!ModelState.IsValid || nRegistrosEncontrados >=1)
            {
                if(nRegistrosEncontrados>=1)
                {
                    clienteCLS.mensajeError = "Ya existe este cliente";
                }
                LlenarSexo();
                ViewBag.listaSexo = listaSexo;
                return View(clienteCLS);
            }
            using (var bd = new BDPasajeEntities())
            {
                Cliente cliente = new Cliente();
                cliente.NOMBRE = clienteCLS.nombreCliente;
                cliente.APPATERNO = clienteCLS.APaterno;
                cliente.APMATERNO = clienteCLS.AMaterno;
                cliente.EMAIL = clienteCLS.Email;
                cliente.DIRECCION = clienteCLS.Direccion;
                cliente.IIDSEXO = clienteCLS.IdSexo;
                cliente.TELEFONOCELULAR = clienteCLS.telefonoCelular;
                cliente.TELEFONOFIJO = clienteCLS.telefonoFijo;
                cliente.BHABILITADO = 1;
                bd.Cliente.Add(cliente);
                bd.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            ClienteCLS clienteCLS = new ClienteCLS();
            using (var bd = new BDPasajeEntities())
            {
                // Se tiene que llenar el combobox
                LlenarSexo();
                ViewBag.listaSexo = listaSexo;


                Cliente cliente = bd.Cliente.Where(p => p.IIDCLIENTE.Equals(id)).First();

                clienteCLS.idCliente = cliente.IIDCLIENTE;
                clienteCLS.nombreCliente = cliente.NOMBRE;
                clienteCLS.nombreCliente = cliente.NOMBRE;
                clienteCLS.APaterno = cliente.APPATERNO;
                clienteCLS.AMaterno = cliente.APMATERNO;
                clienteCLS.Direccion = cliente.DIRECCION;
                clienteCLS.Email = cliente.EMAIL;
                clienteCLS.IdSexo = (int)cliente.IIDSEXO;
                clienteCLS.telefonoCelular = cliente.TELEFONOCELULAR;
                clienteCLS.telefonoFijo = cliente.TELEFONOFIJO;

            }
            return View(clienteCLS);
        }

        [HttpPost]
        public ActionResult Editar(ClienteCLS clienteCLS)
        {
            int idCliente = clienteCLS.idCliente;

            int nRegistrosEncontrados = 0;
            string nombre = clienteCLS.nombreCliente;
            string apellidoP = clienteCLS.APaterno;
            string apellidoM = clienteCLS.AMaterno;
            using (var bd = new BDPasajeEntities())
            {
                nRegistrosEncontrados = bd.Cliente.Where(p => p.NOMBRE.Equals(nombre) && p.APPATERNO.Equals(apellidoP) && p.APMATERNO.Equals(apellidoM) && !p.IIDCLIENTE.Equals(idCliente)).Count();
            }

            if (!ModelState.IsValid || nRegistrosEncontrados>=1)
            {
                if(nRegistrosEncontrados >=1)
                {
                    clienteCLS.mensajeError = "Ya existe este cliente";
                }
                LlenarSexo();
                return View(clienteCLS);
            }

            else
            {
                using (var bd = new BDPasajeEntities())
                {
                    Cliente cliente = bd.Cliente.Where(p => p.IIDCLIENTE.Equals(idCliente)).First();
                    cliente.NOMBRE = clienteCLS.nombreCliente;
                    cliente.APPATERNO = clienteCLS.APaterno;
                    cliente.APMATERNO = clienteCLS.AMaterno;
                    cliente.EMAIL = clienteCLS.Email;
                    cliente.DIRECCION = clienteCLS.Direccion;
                    cliente.IIDSEXO = clienteCLS.IdSexo;
                    cliente.TELEFONOFIJO = clienteCLS.telefonoFijo;
                    cliente.TELEFONOCELULAR = clienteCLS.telefonoCelular;

                    bd.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Eliminar(int idCliente)
        {
            using (var bd = new BDPasajeEntities())
            {
                Cliente cliente = bd.Cliente.Where(p => p.IIDCLIENTE.Equals(idCliente)).First();
                cliente.BHABILITADO = 0;
                bd.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}