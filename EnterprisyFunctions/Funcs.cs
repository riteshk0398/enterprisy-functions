
using System;
using System.IO;
using AzureFunctions.Autofac;
using BusinessLogic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace EnterprisyFunctions
{
    [DependencyInjectionConfig(typeof(DiConfig))]
    public static class Funcs
    {
        [FunctionName("CrashAndLog")]
        public static IActionResult CrashAndLog([HttpTrigger(AuthorizationLevel.User, "get", "post", Route = null)]
            HttpRequest req,
            [Inject] IMediator mediator,
            ILogger logger
            )
        {
            var result = mediator.Send(new ServiceOne { Param1 = "Testing" }).GetAwaiter().GetResult();

            var rand = new Random();
            var number = rand.Next(5);
            if (number == 3)
            {
                logger
                    .LogError("Randomly crashing {@result}",result);
                throw new Exception();
            }
            return new OkObjectResult(result);
        }
    }
}
