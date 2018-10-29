using DependencyInjectionSample.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionSample.Services
{
    public class MyDenpendency : IMyDenpendency
    {
        public readonly ILogger<MyDenpendency> _logger;
        public MyDenpendency(ILogger<MyDenpendency> logger)
        {
            this._logger = logger;
        }

        public Task WriteMessage(string message)
        {
            _logger.LogInformation(typeof(MyDenpendency).FullName+" called. Message {0}",message);

            return Task.FromResult(0);
        }
    }
}
