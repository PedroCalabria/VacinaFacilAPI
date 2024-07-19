using ControleTarefas.Entidade.DTO;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using VacinaFacil.Business.Interface.IBusinesses;
using VacinaFacil.Entity.DTO;
using VacinaFacil.Entity.Entities;
using VacinaFacil.Repository.Interface.IRepositories;
using VacinaFacil.Utils.Configurations;
using VacinaFacil.Utils.Extensions;
using VacinaFacil.Utils.Messages;
using VacinaFacil.Utils.PatientContext;

namespace VacinaFacil.Business.Businesses
{
    public class AuthenticationBusiness : IAuthenticationBusiness
    {
        private readonly IPatientRepository _patientRepository;
        private readonly AuthenticationConfig _authenticationConfig;
        private readonly IPatientContext _patientContext;

        public AuthenticationBusiness(IPatientRepository patientRepository, IOptionsMonitor<AuthenticationConfig> authenticationConfig, IPatientContext patientContext)
        {
            _patientRepository = patientRepository;
            _authenticationConfig = authenticationConfig.CurrentValue;
            _patientContext = patientContext;
        }

        public async Task<TokenPatientDTO> Login(LoginDTO login)
        {
            var validPatient = await Authenticate(login);
            var patient = await _patientRepository.getPatient(login.Email);
            string token;
            string refreshToken;

            if (validPatient && patient != null)
            {
                token = GenerateToken(patient);
                refreshToken = GenerateRefreshToken(patient);
            }
            else
                throw new UnauthorizedAccessException(BusinessMessages.InvalidEmailPassword);

            return new TokenPatientDTO(token, refreshToken);
        }


        private async Task<bool> Authenticate(LoginDTO login)
        {
            var patient = await _patientRepository.getPatient(login.Email);

            if (patient == null)
                return false;

            using var hmac = new HMACSHA512(patient.PasswordSalt);

            return hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password))
            .SequenceEqual(patient.PasswordHash);
        }

        private string GenerateToken(Patient patient)
        {
            var expiration = DateTime.Now.AddMinutes(_authenticationConfig.AccessTokenExpiration);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Sid, patient.Id.ToString()),
                new(ClaimTypes.Name, patient.Name),
                new(ClaimTypes.Email, patient.Email),
                new("birthDate", patient.BirthDate.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationConfig.SecretKey));
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _authenticationConfig.Issuer,
                audience: _authenticationConfig.Audience,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private string GenerateRefreshToken(Patient patient)
        {
            var expiration = DateTime.Now.AddMinutes(_authenticationConfig.RefreshTokenExpiration);

            var claims = new List<Claim>
            {
                new(ClaimTypes.Sid, patient.Id.ToString()),
                new(ClaimTypes.Email, patient.Email),
                new("birthDate", patient.BirthDate.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationConfig.SecretKey));
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _authenticationConfig.Issuer,
                audience: _authenticationConfig.Audience,
                claims: claims,
                expires: expiration,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<TokenPatientDTO> RefreshToken()
        {
            var id = _patientContext.Id();
            var patient = await _patientRepository.getByID(id);
            string token;
            string refreshToken;

            if (patient != null)
            {
                token = GenerateToken(patient);
                refreshToken = GenerateRefreshToken(patient);
            }
            else
                throw new UnauthorizedAccessException();

            return new TokenPatientDTO(token, refreshToken);
        }
    }
}
