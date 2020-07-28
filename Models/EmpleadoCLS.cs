using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RazorMVC.Models
{
    public class EmpleadoCLS
    {
        [Display(Name = "ID Empleado")]
        public int idEmpleado { get; set; }

        [Display(Name = "Nombre")]
        [StringLength(100, ErrorMessage = "La longitud máxima es 100")]
        [Required]
        public string nombreEmpleado { get; set; }

        [Display(Name = "Apellido 1")]
        [StringLength(200, ErrorMessage = "La longitud máxima es 200")]
        [Required]
        public string aPaterno { get; set; }

        [Display(Name = "Apellido 2")]
        [StringLength(200, ErrorMessage = "La longitud máxima es 200")]
        [Required]
        public string aMaterno { get; set; }

        [Display(Name = "Fecha de contrato")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechaContrato { get; set; }

        [Display(Name="Sueldo")]
        [Required]
        [Range(0,100000, ErrorMessage = "Fuera de rango")]
        public decimal sueldo { get; set; }

        [Display(Name = "Tipo de usuario")]
        [Required]
        public int idTipoUsuario { get; set; }

        [Display(Name = "Tipo contrato")]
        [Required]
        public int idTipoContrato { get; set; }

        [Display(Name = "Sexo")]
        [Required]
        public int idTipoSexo { get; set; }


        public int bHabilitado { get; set; }

        // Propiedades adicionales
        [Display(Name = "Tipo contrato")]
        public string nombreTipoContrato { get; set; }

        [Display(Name = "Tipo usuario")]
        public string nombreTipoUsuario { get; set; }

        public string mensajeError { get; set; }
    }
}