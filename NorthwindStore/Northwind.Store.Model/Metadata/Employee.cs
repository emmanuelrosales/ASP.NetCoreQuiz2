using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Store.Model
{
    [ModelMetadataType(typeof(EmployeeMetadata))]
    public partial class Employee : ModelBase
    {
        [NotMapped]
        public string PictureBase64
        {
            get
            {
                var result = "";
                if (Photo != null)
                {
                    var base64 = Convert.ToBase64String(Photo);
                    result = $"data:image/jpg;base64,{base64}";
                }
                return result;
            }
        }

        public class EmployeeMetadata
        {

        }
    }
}
