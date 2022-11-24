using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Store.Model
{
    [ModelMetadataType(typeof(CategoryMetadata))]
    public partial class Category : ModelBase
    {
        [NotMapped]
        public string PictureBase64
        {
            get
            {
                var result = "";
                if (Picture != null)
                {
                    var base64 = Convert.ToBase64String(Picture);
                    result = $"data:image/jpg;base64,{base64}";
                }
                return result;
            }
        }
        public class CategoryMetadata
        {
            [Display(Name = "Category Name")]
            [StringLength(15, MinimumLength = 4, ErrorMessage = "Se requiere entre {2} y {1} caracteres.")]
            public string CategoryName { get; set; } = null!;

            [Display(Name = "Description Abuelito dime tu")]
            [Required(ErrorMessage = "The {0} is required")]
            //[System.ComponentModel.DataAnnotations.Phone]
            [StringLength(32, MinimumLength = 4, ErrorMessage = "Se requiere entre {2} y {1} caracteres.")]
            public string? Description { get; set; }
        }
    }
}
