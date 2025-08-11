using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microservices.EK.Command.Api.Controllers
{
    public class BaseKontrollerOpen : ControllerBase
    {
        protected const string JSON_CONTENT_TYPE = "application/json";
        protected async Task<string> GetInputDataAsync()
        {
            Request.Body.Position = 0;

            using (var reader = new StreamReader(Request.Body))
            {
                string retValue = await reader.ReadToEndAsync();
                return retValue;
            }
        }
        protected async Task<dynamic> GetInputObject()
        {
            var objectString = await this.GetInputDataAsync();
            dynamic obj = string.IsNullOrEmpty(objectString) ? null : JObject.Parse(objectString);

            return obj;
        }
        protected dynamic GetInputObject(string objectString)
        {
            try
            {
                dynamic obj = JObject.Parse(objectString);
                return obj;
            }
            catch (JsonReaderException)
            {
                // JSON inválido
                return null;
            }
        }

    }
}
