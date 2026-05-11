using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Globalization;

namespace Stoca.Function
{
    public static class HttpExample
    {
        [FunctionName("HttpExample")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // Read possible values from query string
            string nameQuery = req.Query["name"];
            string emailQuery = req.Query["email"];
            string ageQuery = req.Query["age"];

            // Read and parse JSON body (if any)
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = null;
            if (!string.IsNullOrWhiteSpace(requestBody))
            {
                try
                {
                    data = JsonConvert.DeserializeObject(requestBody);
                }
                catch
                {
                    data = null;
                }
            }

            // Prefer query values; fallback to JSON body; then sensible defaults
            string nameRaw = !string.IsNullOrWhiteSpace(nameQuery) ? nameQuery : (data?.name != null ? (string)data.name : null);
            string emailRaw = !string.IsNullOrWhiteSpace(emailQuery) ? emailQuery : (data?.email != null ? (string)data.email : null);
            string ageRaw = !string.IsNullOrWhiteSpace(ageQuery) ? ageQuery : (data?.age != null ? data.age.ToString() : null);

            string defaultName = "Anonymous";
            string defaultEmail = "unknown";
            object ageValue = "not provided";

            string finalName = string.IsNullOrWhiteSpace(nameRaw) ? defaultName : ToTitleCase(nameRaw.Trim());
            string finalEmail = string.IsNullOrWhiteSpace(emailRaw) ? defaultEmail : emailRaw.Trim().ToLowerInvariant();

            if (!string.IsNullOrWhiteSpace(ageRaw))
            {
                if (int.TryParse(ageRaw.Trim(), out int parsedAge))
                {
                    ageValue = parsedAge;
                }
            }

            return new OkObjectResult(new { name = finalName, email = finalEmail, age = ageValue });

            static string ToTitleCase(string input)
            {
                if (string.IsNullOrWhiteSpace(input)) return input;
                var ti = CultureInfo.CurrentCulture.TextInfo;
                return ti.ToTitleCase(input.ToLowerInvariant());
            }
        }
    }
}
