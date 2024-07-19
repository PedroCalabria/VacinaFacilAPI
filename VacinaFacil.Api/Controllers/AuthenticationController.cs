using Microsoft.AspNetCore.Mvc;

using ControleTarefas.Entidade.DTO;

using Microsoft.AspNetCore.Authorization;
using VacinaFacil.Business.Interface.IBusinesses;
using VacinaFacil.Entity.DTO;

namespace ControleTarefas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationBusiness _authenticationBusiness;

        public AuthenticationController(IAuthenticationBusiness authenticationBusiness)
        {
            _authenticationBusiness = authenticationBusiness;
        }

        [HttpPost("login")]
        public async Task<TokenPatientDTO> Login(LoginDTO login)
        {
            return await _authenticationBusiness.Login(login);
        }

        [HttpGet("refreshToken")]
        [ProducesResponseType(typeof(TokenPatientDTO), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<TokenPatientDTO> RefreshToken()
        {
            return await _authenticationBusiness.RefreshToken();

        }
    }
}
