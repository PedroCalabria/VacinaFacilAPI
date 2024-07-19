using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleTarefas.Entidade.DTO
{
    public class TokenPatientDTO
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public TokenPatientDTO(string token, string refreshToken)
        {
            Token = token;
            RefreshToken = refreshToken;
        }
    }
}
