using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DependencyInjectionDemo.NetFrameworkAspNet.Services
{
    public class ValuesService : IValuesService
    {
        private readonly List<string> values = new List<string> { "value1", "value2" };

        public IEnumerable<string> GetValues()
        {
            return values;
        }

        public string GetValueById(int id)
        {
            return values.ElementAtOrDefault(id);
        }

        public void AddValue([FromBody] string value)
        {
            values.Add(value);
        }

        public void UpdateValue(int id, [FromBody] string value)
        {
            if (id >= 0 && id < values.Count)
            {
                values[id] = value;
            }
        }

        public void DeleteValue(int id)
        {
            if (id >= 0 && id < values.Count)
            {
                values.RemoveAt(id);
            }
        }
    }
}