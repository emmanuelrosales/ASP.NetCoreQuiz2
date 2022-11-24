using Microsoft.AspNetCore.Mvc;

namespace Northwind.Store.Model
{
    [ModelMetadataType(typeof(RegionMetadata))]
    public partial class Region : ModelBase
    {

        public class RegionMetadata
        {

        }
    }
}
