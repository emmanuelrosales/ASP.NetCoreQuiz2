dotnet ef migrations remove -c ApplicationDbContext

dotnet ef migrations add IdentityNew -c ApplicationDbContext

// sincroniza las estructuras del codigo con la base de datos

dotnet ef database update -c ApplicationDbContext

dotnet tool install -g dotnet-aspnet-codegenerator

dotnet aspnet-codegenerator --help

dotnet tool update -g dotnet-aspnet-codegenerator

dotnet aspnet-codegenerator --project "D:\aspnet202208\proyecto\NorthwindStore\Northwind.Store.UI.Web.Intranet" controller --force --controllerName CategoryController --model Northwind.Store.Model.Category --dataContext Northwind.Store.Data.NWContext --relativeFolderPath Areas/Admin/Controllers --controllerNamespace Northwind.Store.UI.Web.Intranet.Areas.Admin.Controllers --referenceScriptLibraries --useDefaultLayout