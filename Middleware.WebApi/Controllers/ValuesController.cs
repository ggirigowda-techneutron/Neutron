using System.Collections.Generic;
using System.Web.Http;
using Microsoft.Web.Http;

namespace Middleware.WebApi.Controllers
{
    /// <summary>
    ///     Values controller
    /// </summary>
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/values")]
    public class ValuesController : ApiController
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] {"Version 1.0 value1", "Version 1.0 value2"};
        }

        // GET api/values
        [HttpGet, MapToApiVersion("2.0")]
        public IEnumerable<string> GetV2()
        {
            return new[] { "Version 2.0 value1", "Version 2.0 value2" };
        }

        // GET api/values/5
        [HttpGet]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}