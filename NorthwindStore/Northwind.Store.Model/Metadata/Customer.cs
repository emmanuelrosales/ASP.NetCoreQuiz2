using Microsoft.AspNetCore.Mvc;

namespace Northwind.Store.Model
{
    [ModelMetadataType(typeof(CustomerMetadata))]
    public partial class Customer : ModelBase
    {

        public class CustomerMetadata
        {

        }
    }
}
