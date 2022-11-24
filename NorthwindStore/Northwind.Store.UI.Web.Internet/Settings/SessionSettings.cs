using Microsoft.AspNetCore.Http;
using Northwind.Store.UI.Web.Internet.ViewModels;

namespace Northwind.Store.UI.Web.Internet
{
    public class SessionSettings
    {
        private readonly ISession _session;
        public SessionSettings(IHttpContextAccessor hca)
        {
            _session = hca?.HttpContext.Session;
        }

        public CartViewModel Cart
        {
            get
            {
                var cart = _session.GetObject<CartViewModel>(nameof(Cart));

                if (cart == null)
                {
                    cart = new CartViewModel();
                }

                return cart;
            }
            set
            {
                _session.SetObject(nameof(Cart), value);
            }
        }

        public string OrderID
        {
            get
            {
                return _session.GetString(nameof(OrderID));
            }
            set
            {
                _session.SetString(nameof(OrderID), value);
            }
        }
    }
}