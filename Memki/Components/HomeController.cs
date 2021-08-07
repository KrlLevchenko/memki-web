using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Memki.Components
{
    public class HomeController: Controller
    {
        [Route("")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return Ok(new {version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "version"});
        }
    }
}