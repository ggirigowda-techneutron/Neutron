using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Middleware.Core.WebApi.Controllers
{
    /// <summary>
    ///     Values controller
    /// </summary>
    //[ApiVersion("1.0")]
    //[ApiVersion("2.0")]
    [Route("api/[controller]")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        ///     Get all
        /// </summary>
        /// <returns></returns>
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Version 1.0 value1", "Version 1.0 value2" };
        }

        // GET api/values
        //[HttpGet, MapToApiVersion("2.0")]
        //public IEnumerable<string> GetV2()
        //{
        //    return new[] { "Version 2.0 value1", "Version 2.0 value2" };
        //}

            /// <summary>
            /// Get by Id
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        /// <summary>
        ///     Post value
        /// </summary>
        /// <param name="value"></param>
        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        /// <summary>
        ///     Put value
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        ///     Delete value
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
