using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UdemusDateus.Data;
using UdemusDateus.Entities;

namespace UdemusDateus.Controllers;

public class ErrorController : BaseApiController
{
    private readonly DataContext _dataContext;

    public ErrorController(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    [Authorize]
    [HttpGet("auth")]
    public ActionResult<string> GetSecret()
    {
        return "Secret text!";
    }
    
    [HttpGet("not-found")]
    public ActionResult<AppUser> GetNotFound()
    {
        var test = _dataContext.Users.Find(-1);

        if (test == null) return NotFound();

        return Ok(test);
    }
    
    [HttpGet("server-error")]
    public ActionResult<string> GetServerError()
    {
        var test = _dataContext.Users.Find(-1);
        var testReturn = test.ToString();

        return testReturn;
    }
    
    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest()
    {
        return BadRequest("This is a bad request!");
    }
}