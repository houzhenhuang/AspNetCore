using DependencyInjectionSample.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionSample.Services
{
    public class OperationService
    {
        public readonly ILogger<MyDenpendency> _logger;
        public OperationService(
            ILogger<MyDenpendency> logger,
           IOperationTransient transientOperation,
           IOperationScoped scopedOperation,
           IOperationSingleton singletonOperation,
           IOperationSingletonInstance instanceOperation)
        {
            TransientOperation = transientOperation;
            ScopedOperation = scopedOperation;
            SingletonOperation = singletonOperation;
            SingletonInstanceOperation = instanceOperation;
            this._logger = logger;
        }

        public IOperationTransient TransientOperation { get; }
        public IOperationScoped ScopedOperation { get; }
        public IOperationSingleton SingletonOperation { get; }
        public IOperationSingletonInstance SingletonInstanceOperation { get; }

        public void WriteMessage()
        {
            _logger.LogWarning("OperationService.WriteMessage[TransientOperation]==>" + TransientOperation.OperationId.ToString());
            _logger.LogWarning("OperationService.WriteMessage[ScopedOperation] ==>" + ScopedOperation.OperationId.ToString());
            _logger.LogWarning("OperationService.WriteMessage[SingletonOperation]==>" + SingletonOperation.OperationId.ToString());
            _logger.LogWarning("OperationService.WriteMessage[SingletonInstanceOperation]==>" + SingletonInstanceOperation.OperationId.ToString());
        }
    }
}
