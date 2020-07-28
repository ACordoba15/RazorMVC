using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RazorMVC.Models
{
    public class ViajeCLS
    {
        [Display(Name = "ID Viaje")]
        
        public int idViaje { get; set; }

        [Display(Name = "Lugar origen")]
        [Required]
        public int idLugarOrigen { get; set; }

        [Display(Name = "Lugar destino")]
        [Required]
        public int idLugarDestino { get; set; }

        [Display(Name = "Precio")]
        [Required]
        [Range(0,100000, ErrorMessage = "Rango incorrecto")]
        public double precio { get; set; }

        [Display(Name = "Fecha viaje")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechaViaje { get; set; }

        [Display(Name = "Bus")]
        [Required]
        public int idBus { get; set; }

        [Display(Name = "Cant. asientos disponibles")]
        [Required]
        public int cantAsientosDisponibles { get; set; }


        // Propiedades adicionales

        [Display(Name = "Lugar origen")]
        public string nombreLugarO { get; set; }

        [Display(Name = "Lugar destino")]
        public string nombreLugarD { get; set; }

        [Display(Name = "Nombre bus")]
        public string nombreBus { get; set; }

    }
}