using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RazorMVC.Models;

namespace RazorMVC.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        public ActionResult Index()
        {
            List<EmpleadoCLS> listaEmpleados = null;
            using (var bd = new BDPasajeEntities())
            {
                // Va a buscar en la tabla empleados, hace un join para sacar el dato de la tabla
                // tipoUsuario con el id, así como el dato de la tabla tipoContrato
                listaEmpleados = (
                    from empleado in bd.Empleado
                    join tipoUsuario in bd.TipoUsuario
                    on empleado.IIDTIPOUSUARIO equals tipoUsuario.IIDTIPOUSUARIO
                    join tipoContrato in bd.TipoContrato
                    on empleado.IIDTIPOCONTRATO equals tipoContrato.IIDTIPOCONTRATO
                    where empleado.BHABILITADO == 1
                    select new EmpleadoCLS
                    {
                        idEmpleado = empleado.IIDEMPLEADO,
                        nombreEmpleado = empleado.NOMBRE,
                        aPaterno =  empleado.APPATERNO,
                        nombreTipoUsuario = tipoUsuario.NOMBRE,
                        nombreTipoContrato = tipoContrato.NOMBRE,
                        fechaContrato = (DateTime)empleado.FECHACONTRATO
                    }).ToList();
            }
                return View(listaEmpleados);
        }

        public ActionResult Agregar()
        {
            ListarCombos();
            return View();
        }

        public void ListaComboSexo()
        {
            // Agregar
            List<SelectListItem> listaSexo;
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
                //  Inserta en la primera posición
                listaSexo.Insert(0, new SelectListItem { Text = "Seleccione un sexo ", Value = "" });
                ViewBag.listaSexo = listaSexo;    
            }
        }

        public void ListaComboTipoContrato()
        {
            // LLena los combo, se hace una lista de tipo listItem para pasarlos al combo
            // Agregar
            List<SelectListItem> listaContrato;
            using (var bd = new BDPasajeEntities())
            {
                listaContrato = (
                    from tipoContrato in bd.TipoContrato
                    where tipoContrato.BHABILITADO == 1
                    select new SelectListItem
                    {
                        Text = tipoContrato.NOMBRE,
                        Value = tipoContrato.IIDTIPOCONTRATO.ToString()
                    }).ToList();
                //  Inserta en la primera posición
                listaContrato.Insert(0, new SelectListItem { Text = "Seleccione un tipo de contrato ", Value = "" });
                ViewBag.listaContrato = listaContrato;
            }
        }

        public void ListaComboTipoUsuario()
        {
            // Agregar
            List<SelectListItem> listaUsuario;
            using (var bd = new BDPasajeEntities())
            {
                listaUsuario = (
                    from tipoUsuario in bd.TipoUsuario
                    where tipoUsuario.BHABILITADO == 1
                    select new SelectListItem
                    {
                        Text = tipoUsuario.NOMBRE,
                        Value = tipoUsuario.IIDTIPOUSUARIO.ToString()
                    }).ToList();
                //  Inserta en la primera posición
                listaUsuario.Insert(0, new SelectListItem { Text = "Seleccione un tipo de usuario ", Value = "" });
                ViewBag.listaUsuario = listaUsuario;
            }
        }

        public void ListarCombos()
        {
            ListaComboSexo();
            ListaComboTipoContrato();
            ListaComboTipoUsuario();
        }

        [HttpPost]
        public ActionResult Agregar(EmpleadoCLS empleadoCLS)
        {
            int nRegistrosEncontrados = 0;
            string nombre = empleadoCLS.nombreEmpleado;
            string apellidoP = empleadoCLS.aPaterno;
            string apellidoM = empleadoCLS.aMaterno;
            using (var bd = new BDPasajeEntities())
            {
                nRegistrosEncontrados = bd.Empleado.Where(p => p.NOMBRE.Equals(nombre) && p.APPATERNO.Equals(apellidoP) && p.APMATERNO.Equals(apellidoM)).Count();
            }

            if (!ModelState.IsValid || nRegistrosEncontrados>=1)
            {
                if(nRegistrosEncontrados>=1)
                {
                    empleadoCLS.mensajeError = "Este empleado ya existe";
                }
                ListarCombos();
                return View(empleadoCLS);
            }

            else
            {
                using (var bd = new BDPasajeEntities())
                {
                    Empleado empleado = new Empleado();
                    empleado.NOMBRE = empleadoCLS.nombreEmpleado;
                    empleado.APPATERNO = empleadoCLS.aPaterno;
                    empleado.APMATERNO = empleadoCLS.aMaterno;
                    empleado.FECHACONTRATO = empleadoCLS.fechaContrato;
                    empleado.SUELDO = empleadoCLS.sueldo;
                    empleado.IIDTIPOUSUARIO = empleadoCLS.idTipoUsuario;
                    empleado.IIDTIPOCONTRATO = empleadoCLS.idTipoContrato;
                    empleado.IIDSEXO = empleadoCLS.idTipoSexo;
                    empleado.BHABILITADO = 1;

                    bd.Empleado.Add(empleado);
                    bd.SaveChanges();
                }

                return RedirectToAction("Index");
            }
        }

        public ActionResult Editar(int id)
        {
            ListarCombos();

            EmpleadoCLS empleadoCLS = new EmpleadoCLS();
            using (var bd = new BDPasajeEntities())
            {
                Empleado empleado = bd.Empleado.Where(p => p.IIDEMPLEADO.Equals(id)).First();
                empleadoCLS.idEmpleado = empleado.IIDEMPLEADO;
                empleadoCLS.nombreEmpleado = empleado.NOMBRE;
                empleadoCLS.aPaterno = empleado.APPATERNO;
                empleadoCLS.aMaterno = empleado.APMATERNO;
                empleadoCLS.fechaContrato = (DateTime)empleado.FECHACONTRATO;
                empleadoCLS.sueldo = (decimal) empleado.SUELDO;
                empleadoCLS.idTipoUsuario = (int) empleado.IIDTIPOUSUARIO;
                empleadoCLS.idTipoContrato = (int) empleado.IIDTIPOCONTRATO;
                empleadoCLS.idTipoSexo = (int) empleado.IIDSEXO;

            }

            return View(empleadoCLS);
        }

        [HttpPost]
        public ActionResult Editar(EmpleadoCLS empleadoCLS)
        {
            int idEmp = empleadoCLS.idEmpleado;
            int nRegistrosEncontrados = 0;
            string nombre = empleadoCLS.nombreEmpleado;
            string apellidoP = empleadoCLS.aPaterno;
            string apellidoM = empleadoCLS.aMaterno;
            using (var bd = new BDPasajeEntities())
            {
                nRegistrosEncontrados = bd.Empleado.Where(p => p.NOMBRE.Equals(nombre) && p.APPATERNO.Equals(apellidoP) && p.APMATERNO.Equals(apellidoM) && !p.IIDEMPLEADO.Equals(idEmp)).Count();
            }

            if (!ModelState.IsValid || nRegistrosEncontrados>=1)
            {
                if(nRegistrosEncontrados >=1)
                {
                    empleadoCLS.mensajeError = "Este empleado ya existe";
                }
                ListarCombos();
                return View(empleadoCLS);
            }

            else
            {
               
                using (var bd = new BDPasajeEntities())
                {
                    Empleado empleado = bd.Empleado.Where(p => p.IIDEMPLEADO.Equals(idEmp)).First();
                    empleado.NOMBRE = empleadoCLS.nombreEmpleado;
                    empleado.APPATERNO = empleadoCLS.aPaterno;
                    empleado.APMATERNO = empleadoCLS.aMaterno;
                    empleado.FECHACONTRATO = empleadoCLS.fechaContrato;
                    empleado.SUELDO = empleadoCLS.sueldo;
                    empleado.IIDTIPOUSUARIO = empleadoCLS.idTipoUsuario;
                    empleado.IIDTIPOCONTRATO = empleadoCLS.idTipoContrato;
                    empleado.IIDSEXO = empleadoCLS.idTipoSexo;

                    bd.SaveChanges();
                }

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Eliminar(int idEmpleado)
        {
            using (var bd = new BDPasajeEntities())
            {
                Empleado empleado = bd.Empleado.Where(p => p.IIDEMPLEADO.Equals(idEmpleado)).First();
                empleado.BHABILITADO = 0;
                bd.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}