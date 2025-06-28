using aluguel_de_imoveis.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace aluguel_de_imoveis.Security
{
    public class JwtTokenGenerator
    {

        private readonly string _jwtKey;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            _jwtKey = configuration["Jwt:Key"];
        }

        public string GerarToken(Usuario usuario)
        {
            var claims = new List<Claim>()
            {
                new(JwtRegisteredClaimNames.Sub, usuario.Id.ToString())
            };

            var chaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(120),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(chaveSimetrica, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }
    }
}
