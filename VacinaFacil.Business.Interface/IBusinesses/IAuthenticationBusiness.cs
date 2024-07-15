using ControleTarefas.Entidade.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacinaFacil.Entity.DTO;

namespace VacinaFacil.Business.Interface.IBusinesses
{
    public interface IAuthenticationBusiness
    {
        Task<TokenPatientDTO> Login(LoginDTO login);
        Task<TokenPatientDTO> RefreshToken();
    }
}
