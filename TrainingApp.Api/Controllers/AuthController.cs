using Microsoft.AspNetCore.Mvc;

namespace TrainingApp.Controllers;

[ApiController]
[Route("/auth")]
public sealed class AuthController : ControllerBase
{
	[HttpGet("me")]
	public IActionResult Get()
	{
		return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
	}
}