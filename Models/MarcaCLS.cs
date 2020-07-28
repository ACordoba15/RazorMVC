using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RazorMVC.Models
{
    // Se define un modelo. 
    public class MarcaCLS
    {
        [Display(Name = "ID Marca")]
        public int idMarca { get; set; }

        [Display(Name = "Nombre Marca")]
        [Required] // Es requerido
        [StringLength(100, ErrorMessage ="La longitud máxima es 100")]
        public string nombreMarca { get; set; }

        [Display(Name = "Descripcion Marca")]
        [Required]
        [StringLength(200, ErrorMessage = "La longitud máxima es 200")]
        public string descripcionMarca { get; set; }
        
        public int bHabilitado { get; set; }

        // Añado una propiedad (errores de validación)


        public string mensajeError { get; set; }
    }
}