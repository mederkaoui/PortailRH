using PortailRH.BLL.Dtos.Authentification;
using PortailRH.BLL.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortailRH.BLL.Services.AuthentificationService
{
    /// <summary>
    /// IAuthentificationService
    /// </summary>
    public interface IAuthentificationService
    {
        public Task<UserDto> Login(LoginDto loginDto);
    }
}
