using System.Collections.Generic;
using System.Web.Http;

namespace DependencyInjectionDemo.NetFrameworkAspNet.Services
{
    public interface IValuesService
    {
        void AddValue([FromBody] string value);
        void DeleteValue(int id);
        string GetValueById(int id);
        IEnumerable<string> GetValues();
        void UpdateValue(int id, [FromBody] string value);
    }
}