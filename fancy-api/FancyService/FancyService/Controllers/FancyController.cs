using System;
using Fancy.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FancyService.Controllers
{
    public class FancyController : Controller
    {
        private IFancyService _fancyService;
        private ILogger _logger;

        public FancyController(IFancyService fancyService)
        {
            _fancyService = fancyService;
            _logger = new LoggerFactory().CreateLogger<FancyController>();
        }

        [Route("api/fibonacci")]
        [HttpGet]
        public IActionResult Fibonacci([FromQuery] long n)
        {
            if (Math.Abs(n) > 92)
                return Ok();

            if (n < 0)
                return Ok(_fancyService.GetFibonacciNumberForNegativeIndex(n));

            return Ok(_fancyService.GetFibonacciNumberForPositiveIndex(n));
        }

        [Route("api/ReverseWords")]
        [HttpGet]
        public IActionResult ReverseWords([FromQuery] string sentence)
        {
            return new JsonResult(_fancyService.GetReverseWords(sentence));
        }

        [Route("api/Token")]
        [HttpGet]
        public IActionResult Token()
        {
            _logger.LogError("test");
            return Ok(_fancyService.GetToken());
        }


        [Route("api/TriangleType")]
        [HttpGet]
        public IActionResult TriangleType([FromQuery] int? a, [FromQuery]  int? b, [FromQuery]  int? c)
        {
            if (HasNumberValue(a) && HasNumberValue(b) && HasNumberValue(c))
                return new JsonResult(_fancyService.GetTriangleType(a, b, c));

            return BadRequest(new { message = "The request is invalid." });
        }

        private bool HasNumberValue(int? number)
        {
            return number.HasValue;
        }
    }
}
