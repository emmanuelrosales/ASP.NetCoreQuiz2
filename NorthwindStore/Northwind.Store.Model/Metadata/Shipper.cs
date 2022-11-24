using Microsoft.AspNetCore.Mvc;

namespace Northwind.Store.Model
{
    [ModelMetadataType(typeof(ShipperMetadata))]
    public partial class Shipper : ModelBase
    {

        public class ShipperMetadata
        {

        }
    }
}
