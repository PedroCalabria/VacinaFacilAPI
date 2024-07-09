using log4net;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using VacinaFacil.Repository.Interface;
using VacinaFacil.Utils.Attributes;
using VacinaFacil.Utils.Responses;

namespace VacinaFacil.Api.Middleware
{
    public class ApiMiddleware : IMiddleware
    {
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

            messages.Add("Ocooreu um erro, contate o administrador.");

            await response.WriteAsync(JsonConvert.SerializeObject(new DefaultResponse(HttpStatusCode.InternalServerError, messages)));
        }
    }
}
