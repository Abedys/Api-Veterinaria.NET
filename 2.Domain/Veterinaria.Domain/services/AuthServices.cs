using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using NETCore.Encrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.Common.Exceptions;
using Veterinaria.Core.UnitWork.interfacce;
using Veterinaria.Domain.services.Interface;
using Veterinaria.Entity.DTO.Tokens;
using Veterinaria.Entity.Models.Secure;
using static Veterinaria.Common.Constant.Const;

namespace Veterinaria.Domain.services
{
    public class AuthServices : IAuthServices
    {
        #region Atrinutes
        private readonly IUserServices _userServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        #endregion


        #region Builder

        public AuthServices(IUserServices userServices, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _userServices = userServices;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        #endregion

        #region Methods
        public async Task<TokenDto> Login (LoginDto login)
        {
            UserEntity user = await _userServices.GetUserByEmail(login.Email);
            if (user == null || user.Password != EncryptProvider.Sha256(login.Password))
            {
                throw new BusinessException("Credenciales incorrectas");
            }
            return GenerateTokenJWT(user);
        }
        #endregion



        #region Privated
        private TokenDto GenerateTokenJWT(UserEntity userEntity)
        {
            IConfigurationSection tokenAppSetting = _configuration.GetSection("Tokens");
            //Es la clave secreta que se utiliza para firmar y verificar el token JWT. Debe ser una cadena larga y segura.
            string? key = tokenAppSetting.GetSection("Key").Value;
            //Es el emisor del token, que indica quién generó el token. Puede ser el nombre de tu aplicación o dominio.
            string? issuer = tokenAppSetting.GetSection("Issuer").Value;
            //Es el público del token, que indica quién puede usar el token. Puede ser el nombre de tu aplicación o dominio.
            string?  audience = tokenAppSetting.GetSection("Audience").Value;
            //Es el tiempo de expiración del token en minutos. Debe ser un número entero positivo.
            string? expirationMinutesValue = tokenAppSetting.GetSection("ExpireMinutes").Value;

            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(issuer) || string.IsNullOrWhiteSpace(audience))
                throw new InvalidOperationException("La configuración Tokens debe incluir Key, Issuer y Audience.");

            if (!int.TryParse(expirationMinutesValue, out int expirationMinutes) || expirationMinutes <= 0)
                throw new InvalidOperationException("La configuración Tokens:ExpireMinutes debe ser un número mayor que cero.");


            // La misma clave y el mismo algoritmo (HMAC-SHA256) deben usarse al validar la firma.
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            // Se utiliza para firmar el token y garantizar su integridad. Se crea a partir de la clave secreta y el algoritmo de firma.
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            // Datos que viajan dentro del token. No poner la contraseña ni otros secretos.
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(TypeClaims.IdUser, userEntity.IdUser.ToString()),
                new Claim(TypeClaims.FullName, userEntity.FullName),
                new Claim(TypeClaims.Email, userEntity.Email),
            };

            // nbf y exp van en UTC. JWT guarda un instante, no la hora local del servidor.
            DateTime now = DateTime.UtcNow;
            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Subject = new ClaimsIdentity(claims),
                NotBefore = now,
                Expires = now.AddMinutes(expirationMinutes),
                SigningCredentials = signingCredentials
            };

            TokenDto token = new TokenDto
            {
                Token = new JsonWebTokenHandler().CreateToken(descriptor),
                // Expires es UTC. En el JSON sale con Z; no es la hora de Colombia.
                DateExpiration = descriptor.Expires!.Value
            };

            return token;



        }

        #endregion
    }
}
