using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Shared.Database;

namespace HttpFunction
{
    public  class HttpFunctionTest
    {
        private readonly IConfiguration _configuration;
        private readonly TestContext _dbContext;

        public HttpFunctionTest(IConfiguration configuration , TestContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        [FunctionName("Function")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            var value = $"Fun:Value :{_configuration["Fun:Value"]}";

            string responseMessage = string.IsNullOrEmpty(name)
                ? $"{value}  | This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"{value} | Hello, {name}. This HTTP triggered function executed successfully.";


            await _dbContext.Tests.AddAsync(new Test() { LastUpdate = DateTime.Now, Name = name });
            await _dbContext.SaveChangesAsync();
            return new OkObjectResult(responseMessage);
        }
    }
}
