using DependencyInjectionDemo.NetFrameworkAspNet.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DependencyInjectionDemo.NetFrameworkAspNet.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly IValuesService valuesService;

        public ValuesController(IValuesService valuesService)
        {
            this.valuesService = valuesService;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return valuesService.GetValues();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return valuesService.GetValueById(id);
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
            valuesService.AddValue(value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
            valuesService.UpdateValue(id, value);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            valuesService.DeleteValue(id);
        }
    }
}
