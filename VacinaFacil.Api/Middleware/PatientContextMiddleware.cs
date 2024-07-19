using VacinaFacil.Utils.Extensions;
using VacinaFacil.Utils.PatientContext;
using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ControleTarefas.WebApi.Middleware
{
    public class PatientContextMiddleware : IMiddleware
    {
        private readonly IPatientContext _patientContext;

        public PatientContextMiddleware(IPatientContext patientContext)
        {
            _patientContext = patientContext;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (SetUser(context))
                await next.Invoke(context);
        }

        private bool SetUser(HttpContext context)
        {
            if (IsAuthenticated(context))
            {
                var securityToken = GetSecurityToken(context);

                SetUserContext(context, securityToken);

                return true;
            }
            else
            {
                throw new UnauthorizedAccessException("Usuário não autorizado");
            }
        }

        private static JwtSecurityToken GetSecurityToken(HttpContext context)
        {
            var authToken = context.Request.Headers["Authorization"].ToString();

            if (authToken != null && authToken.Trim().Length > 0)
            {
                var token = authToken.Replace("Bearer", string.Empty).Trim();
                return new JwtSecurityTokenHandler().ReadJwtToken(token);
            }

            return null;
        }

        private static bool IsAuthenticated(HttpContext context)
        {
            var authToken = context.Request.Headers["Authorization"].String();

            if (!string.IsNullOrEmpty(authToken))
                return (context.User?.Identity?.IsAuthenticated ?? false) || !string.IsNullOrEmpty(authToken);
            else
                return true;
        }

        private void SetUserContext(HttpContext context, JwtSecurityToken securityToken)
        {
            _patientContext.RequestId = Guid.NewGuid();
            _patientContext.StartDateTime = DateTime.UtcNow;
            _patientContext.SourceInfo = new SourceInfo
            {
                IP = context?.Connection?.RemoteIpAddress,
                Data = GetAllHeaders(context)
            };

            if (securityToken != null && securityToken.Claims.Any())
            {
                var id = securityToken.Claims.GetClaimValue(ClaimTypes.Sid);
                var name = securityToken.Claims.GetClaimValue(ClaimTypes.Name);
                var email = securityToken.Claims.GetClaimValue(ClaimTypes.Email);
                var birthDate = securityToken.Claims.GetClaimValue("birthDate");

                _patientContext.AddData("id", id);
                _patientContext.AddData("name", name);
                _patientContext.AddData("email", email);
                _patientContext.AddData("birthDate", birthDate);
            }
        }

        private static Hashtable GetAllHeaders(HttpContext context)
        {
            var hashtable = new Hashtable();
            var requestHeaders = context?.Request?.Headers;

            if (requestHeaders == null)
                return hashtable;

            foreach (var header in requestHeaders)
                hashtable.Add(header.Key, header.Value);

            return hashtable;
        }
    }
}