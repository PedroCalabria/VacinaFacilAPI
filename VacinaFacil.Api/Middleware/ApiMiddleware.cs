using log4net;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using VacinaFacil.Repository.Interface;
using VacinaFacil.Utils.Attributes;
using VacinaFacil.Utils.Exceptions;
using VacinaFacil.Utils.Messages;
using VacinaFacil.Utils.Responses;

namespace VacinaFacil.Api.Middleware
{
    public class ApiMiddleware : IMiddleware
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(ApiMiddleware));
        private readonly ITransactionManager _transactionManager;

        public ApiMiddleware(ITransactionManager transactionManager)
        {
            _transactionManager = transactionManager;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            var mandatoryTransaction = context.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata.GetMetadata<MandatoryTransactionAttribute>();

            try
            {

                if (mandatoryTransaction != null)
                {
                    await _transactionManager.BeginTransactionAsync(System.Data.IsolationLevel.ReadCommitted);

                    await next.Invoke(context);

                    await _transactionManager.CommitTransactionAsync();
                }
                else
                {
                    await next.Invoke(context);
                }


                stopwatch.Stop();
                _log.InfoFormat(string.Format(InfraMessages.ExecutionCompleted, context.Request.Method, context.Request.Path, stopwatch.ElapsedMilliseconds));
            }
            catch (Exception ex)
            {
                if (mandatoryTransaction != null)
                {
                    await _transactionManager.RollbackTransactionsAsync();
                }

                stopwatch.Stop();
                await HandleException(context, ex);
            }
        }

        private static async Task HandleException(HttpContext context, Exception ex)
        {
            var response = context.Response;

            response.ContentType = "application/json";

            var messages = new List<string>();

            switch (ex)
            {
                case BusinessException:
                    messages.Add(ex.Message);
                    break;
                default:
                    messages.Add(InfraMessages.UnexpectedError);
                    break;
            }

            await response.WriteAsync(JsonConvert.SerializeObject(new DefaultResponse(HttpStatusCode.InternalServerError, messages)));
        }
    }
}
