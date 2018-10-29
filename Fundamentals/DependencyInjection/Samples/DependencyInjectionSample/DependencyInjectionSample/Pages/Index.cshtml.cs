using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependencyInjectionSample.Interfaces;
using DependencyInjectionSample.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DependencyInjectionSample.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMyDenpendency _myDenpendency;
        public readonly ILogger<MyDenpendency> _logger;
        public IndexModel(ILogger<MyDenpendency> logger,
            IMyDenpendency myDenpendency,
             OperationService operationService,
            IOperationTransient transientOperation,
            IOperationScoped scopedOperation,
            IOperationSingleton singletonOperation,
            IOperationSingletonInstance singletonInstanceOperation)
        {
            this._myDenpendency = myDenpendency;
            OperationService = operationService;
            TransientOperation = transientOperation;
            ScopedOperation = scopedOperation;
            SingletonOperation = singletonOperation;
            SingletonInstanceOperation = singletonInstanceOperation;
            this._logger = logger;
        }
        public OperationService OperationService { get; }
        public IOperationTransient TransientOperation { get; }
        public IOperationScoped ScopedOperation { get; }
        public IOperationSingleton SingletonOperation { get; }
        public IOperationSingletonInstance SingletonInstanceOperation { get; }
        public async Task OnGetAsync()
        {
            _logger.LogWarning("IndexModel.OnGetAsync[TransientOperation]==>" + TransientOperation.OperationId.ToString());
            _logger.LogWarning("IndexModel.OnGetAsync[ScopedOperation] ==>" + ScopedOperation.OperationId.ToString());
            _logger.LogWarning("IndexModel.OnGetAsync[SingletonOperation]==>" + SingletonOperation.OperationId.ToString());
            _logger.LogWarning("IndexModel.OnGetAsync[SingletonInstanceOperation]==>" + SingletonInstanceOperation.OperationId.ToString());
            OperationService.WriteMessage();

            await _myDenpendency.WriteMessage("IndexModel.OnGetAsync created this message.");



        }
    }
}
