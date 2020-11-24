using IdentityAuthencation.Helpers;
using IdentityAuthencation.Logger;
using IdentityAuthencation.Repository;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace IdentityAuthencation.Service.RootService
{
    public abstract class BaseService : IBaseService
    {
        protected ILoggerManager _logger;
        protected IUnitOfWork _unitOfWork;
        protected BaseService(ILoggerManager logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        protected async Task ProcessRequest(Func<Task> func)
        {
            var methodInfo = func.GetMethodInfo().Name;
                _logger.LogInfo("Begin calling function " + methodInfo);
                await func();
                _logger.LogInfo("End calling function " + methodInfo); 
        }
        protected async Task<T> ProcessRequest<T>(Func<Task<T>> func)
        { 
                var methodInfo = func.GetMethodInfo().Name;
                _logger.LogInfo($"Begin calling function {methodInfo} with type {typeof(T)}");
                var result = await func();
                _logger.LogInfo($"End calling function {methodInfo} with type {typeof(T)}");
                return result;
        }
    }
}
