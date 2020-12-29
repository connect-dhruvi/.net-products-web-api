using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_api.Controllers
{
    public class ProductController : Controller
    {
        [Route("[controller]")]
        [HttpGet]
        public string GetProdcuts() =>
            "Hello world";
    }
}
