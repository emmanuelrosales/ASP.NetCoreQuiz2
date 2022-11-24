using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Northwind.Store.Data;
using Northwind.Store.Model;

namespace Northwind.Store.UI.Web.Internet.Controllers
{
    public class OrderDetailController : Controller
    {
        private readonly NWContext _context;
        private readonly SessionSettings _ss;

        public OrderDetailController(NWContext context, SessionSettings ss)
        {
            _context = context;
            _ss = ss;
        }

        public async Task<IActionResult> Create()
        {
            if (ModelState.IsValid)
            {
                foreach (var order in _ss.Cart.Items)
                {
                    OrderDetail orderDetail = new OrderDetail();

                    orderDetail.OrderId = Int32.Parse(_ss.OrderID);
                    orderDetail.ProductId = order.ProductId;
                    orderDetail.UnitPrice = (decimal)order.UnitPrice;
                    orderDetail.Quantity = (short)order.UnitsOnOrder;

                    _context.Add(orderDetail);
                    await _context.SaveChangesAsync();
                }
                HttpContext.Session.Remove("Cart");
                ViewBag.cartItems = 0;
                
                return RedirectToAction("OrderCompleted");

            }

            return View();
        }

        public IActionResult OrderCompleted()
        {
            return View();
        }

        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetails.Any(e => e.OrderId == id);
        }
    }
}
