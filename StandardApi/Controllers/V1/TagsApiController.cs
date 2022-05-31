using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StandardApi.Contracts;
using StandardApi.Contracts.V1.Requests;
using StandardApi.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StandardApi.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TagsApiController : Controller
    {
        // To create data for this service later

        [HttpGet(ApiRoutes.Tag.GetAll)]
        [Authorize(Policy = "TagViewer")]
        public IActionResult GetAll()
        {
            var tags = new List<Tag>
            {
                new Tag { Description = "Random tag description" }
            };
            return Ok(tags);
        }

        [HttpPost(ApiRoutes.Tag.Create)]
        public IActionResult Create([FromBody] CreateTagRequest request)
        {
            return Ok(new Tag { Description = "Random Tag" });
        }
    }
}
