using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RazorMVC.Models
{
    public class SucursalCLS
    {
        [Display(Name="ID Sucursal")]
        public int idSucursal { get; set; }

        [Display(Name = "Nombre Sucursal")]
        [Required]
        [StringLength(100, ErrorMessage ="La longitud máxima es 100")]
        public string nombreSucursal { get; set; }

        [Display(Name = "Dirección")]
        [Required]
        [StringLength(200, ErrorMessage = "La longitud máxima es 200")]
        public string direccionSucursal { get; set; }

        [Display(Name = "Teléfono")]
        [Required]
        [StringLength(8, ErrorMessage = "La longitud máxima es 8")]
        public string telefonoSucursal { get; set; }

        [Display(Name = "Email")]
        [Required]
        [StringLength(100, ErrorMessage = "La longitud máxima es 100")]
        [EmailAddress(ErrorMessage ="Ingrese un email válido")]
        public string emailSucursal { get; set; }

        [Display(Name = "Fecha de apertura")]
        [Required]
        [DataType(DataType.Date)]
        // si no se pone el DisplayFormat no se va a agregar la fecha
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechaApertura { get; set; }


        public int bHabilitado { get; set; }


        public string mensajeError { get; set; }
       
    }
}