using Microsoft.AspNetCore.Mvc;

namespace UdemusDateus.Controllers;

public class FallbackController : Controller
{
    /// <summary>
    /// Provides a fallback for the API to serve the index.html from Angular.
    /// </summary>
    /// <returns></returns>
    public ActionResult Index()
    {
        return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/HTML");
    }
}