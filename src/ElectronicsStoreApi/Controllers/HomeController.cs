using System.Collections;
using System.Collections.Generic;
using ElectronicsStoreApi.Hateoas;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsStoreApi.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("api")]
        public IActionResult Get()
        {
            var wrapper = new EmptyLinkWrapper();
            wrapper.Links.Add(new LinkValue("orders", Url.Action("GetAllOrders", "Orders", null, Request.Scheme)));
            wrapper.Links.Add(new LinkValue("products", Url.Action("GetAllProducts", "Products", null, Request.Scheme)));
            return Ok(wrapper);
        }
    }
}