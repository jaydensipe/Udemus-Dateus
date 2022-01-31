using Microsoft.AspNetCore.Mvc;
using UdemusDateus.Helpers;

namespace UdemusDateus.Controllers;

[ServiceFilter(typeof(LogUserActivity))]
[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
}