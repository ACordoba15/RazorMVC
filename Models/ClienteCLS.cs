using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RazorMVC.Models
{
    public class ClienteCLS
    {
        [Display(Name="ID Cliente")]
        public int idCliente { get; set; }
        
        [Display(Name="Nombre Cliente")]
        [Required]
        [StringLength(100, ErrorMessage = "La longitud máxima es 100")]
        public string nombreCliente { get; set; }

        [Display(Name = "Apellido 1")]
        [StringLength(150, ErrorMessage = "La longitud máxima es 150")]
        [Required]
        public string APaterno { get; set; }

        [Display(Name = "Apellido 2")]
        [StringLength(150, ErrorMessage = "La longitud máxima es 150")]
        [Required]
        public string AMaterno { get; set; }

        [Display(Name = "Email")]
        [Required]
        [StringLength(200, ErrorMessage = "La longitud máxima es 200")]
        [EmailAddress(ErrorMessage = "Ingrese un email válido")]
        public string Email { get; set; }

        [Display(Name = "Dirección")]
        [Required]
        [StringLength(200, ErrorMessage = "La longitud máxima es 200")]
        [DataType(DataType.MultilineText)]
        public string Direccion { get; set; }

        [Display(Name = "Sexo")]
        [Required]
        public int IdSexo { get; set; }

        [Display(Name = "Teléfono fijo")]
        [Required]
        [StringLength(8, ErrorMessage = "La longitud máxima es 8")]
        public string telefonoFijo { get; set; }

        [Display(Name = "Teléfono celular")]
        [Required]
        [StringLength(8, ErrorMessage = "La longitud máxima es 8")]
        public string telefonoCelular { get; set; }

        public int bHabilitado { get; set; }

        // Propiedad adicional
        public string mensajeError { get; set; }

    }
}