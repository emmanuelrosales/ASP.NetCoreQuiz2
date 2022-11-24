﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Northwind.Store.Model
{
    [ModelMetadataType(typeof(ProductMetadata))]
    public partial class Product : ModelBase
    {
        public class ProductMetadata
        {
            [Display(Name = "Nombre de Producto")]
            [Required(ErrorMessage = "El {0} es requerido.")]
            [StringLength(40, MinimumLength = 4, ErrorMessage = "Se requiere entre {2} y {1} caracteres.")]
            public string ProductName { get; set; } = null!;

            //[Display(Name = "Precio Unitario")]
            //[Required(ErrorMessage = "El {0} es requerido.")]
            public decimal? UnitPrice { get; set; }
        }
    }
}