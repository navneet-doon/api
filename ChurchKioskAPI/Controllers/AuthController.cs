using ChurchKioskAPI.Models;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace ChurchKioskAPI.Controllers
{
    public class AuthController : ApiController
    {
        //*************************** config settings **********************************
        private string sec { get; } = System.Configuration.ConfigurationManager.AppSettings["AuthSecurityKey"];
        private string host { get; } = System.Configuration.ConfigurationManager.AppSettings["APIHost"];
        //*************************** //////////// *************************************

        /// <summary>
        /// To authenticate user credentials
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Authenticate([FromBody] LoginRequest login)
        {
            var loginResponse = new LoginResponse { };
            LoginRequest loginrequest = new LoginRequest { };
            loginrequest.Username = login.Username.ToLower();
            loginrequest.Password = login.Password;

            IHttpActionResult response;
            HttpResponseMessage responseMsg = new HttpResponseMessage();
            bool isUsernamePasswordValid = false;

            //************************ User credentials will be authenticated here from DB ********************************
            if (login != null)
                isUsernamePasswordValid = loginrequest.Password == "admin" ? true : false;

            // if credentials are valid
            if (isUsernamePasswordValid)
            {
                string token = createToken(loginrequest.Username);
                //return the token
                return Ok(token);
            }
            else
            {
                // if credentials are not valid send unauthorized status code in response
                loginResponse.responseMsg.StatusCode = HttpStatusCode.Unauthorized;
                response = ResponseMessage(loginResponse.responseMsg);
                return response;
            }
        }

        /// <summary>
        /// JWT is created
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private string createToken(string username)
        {
            //Set issued at date
            DateTime issuedAt = DateTime.UtcNow;
            //set the time when it expires
            DateTime expires = DateTime.UtcNow.AddDays(1);

            //http://stackoverflow.com/questions/18223868/how-to-encrypt-jwt-security-token
            var tokenHandler = new JwtSecurityTokenHandler();

            //create a identity and add claims to the user which we want to log in
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
            });

            var now = DateTime.UtcNow;
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(sec));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);

            //create the jwt
            var token = tokenHandler.CreateJwtSecurityToken(issuer: host, audience: host,
                     subject: claimsIdentity, notBefore: issuedAt, expires: expires, signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
