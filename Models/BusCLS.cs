using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RazorMVC.Models
{
    public class BusCLS
    {
        [Display(Name = "ID Bus")]
        public int idBus { get; set; }

        [Display(Name = "Nombre sucursal")]
        [Required]
        public int idSucursal { get; set; }

        [Display(Name = "Tipo bus")]
        [Required]
        public int idTipoBus { get; set; }

        [Display(Name = "Placa del bus")]
        [Required]
        [StringLength(100, ErrorMessage = "La longitud máxima es 100")]
        public string placaBus { get; set; }

        [Display(Name = "Fecha de compra")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechaCompra { get; set; }


        [Display(Name = "Nombre modelo")]
        [Required]
        public int idModelo { get; set; }


        [Display(Name = "Número de filas")]
        [Required]
        public int numFilas { get; set; }


        [Display(Name = "Número de columnas")]
        [Required]
        public int numColumnas { get; set; }



        public int bHabilitado { get; set; }


        [Display(Name = "Descripción")]
        [Required]
        [StringLength(200, ErrorMessage = "La longitud máxima es 200")]
        public string descripcion { get; set; }


        [Display(Name = "Observación")]
        [StringLength(200, ErrorMessage = "La longitud máxima es 200")]
        public string observacion { get; set; }


        [Display(Name = "Nombre marca")]
        [Required]
        public int idMarca { get; set; }

        // Propiedades adicionales
        [Display(Name = "Nombre sucursal")]
        public string nombreSucursal { get; set; }

        [Display(Name = "Nombre tipo bus")]
        public string nombreTipoBus { get; set; }

        [Display(Name = "Nombre modelo")]
        public string nombreModelo { get; set; }

        public string mensajeError { get; set; }


    }
}