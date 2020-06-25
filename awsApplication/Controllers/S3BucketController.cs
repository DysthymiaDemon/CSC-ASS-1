using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using awsApplication.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace awsApplication.Controllers
{
    [Produces("application/json")]
    [Route("api/S3Bucket")]
    public class S3BucketController : Controller
    {
        private readonly IS3Service _service;

        public S3BucketController(IS3Service service)
        {
            _service = service;
        }

        [HttpPost("{bucketname}")]
        public async Task<IActionResult> CreateBucket([FromRoute] string bucketname)
        {
            var response = await _service.CreateBucketAsync(bucketname);

            return Ok();
        }
    }
}
