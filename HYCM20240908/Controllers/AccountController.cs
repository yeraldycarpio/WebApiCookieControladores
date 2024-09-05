//Importa el espacio de nombres necesarios para la autenticacion por cookies
using Microsoft.AspNetCore.Authentication.Cookies;
//Importa el espacio de nombres necesarios para la autenticacion 
using Microsoft.AspNetCore.Authentication;
//Importa el espacio de nombres necesarios para trabajar con controladores
using Microsoft.AspNetCore.Mvc;
//Importa el espacio de nombres necesarios para trabajar con reclamaciones(claims)
using System.Security.Claims;

namespace HYCM20240908.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController:ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login(string login, string password)
        {
            //comprueba si las credenciales son validas
            if (login == "admin" && password == "admin")
            {
                //crea una lista de reclamaciones (claims)
                var claims = new List<Claim>
                {
                    //agrega una reclamacion de nombres con el valor de 'login'
                    new Claim(ClaimTypes.Name,login),
                };

                //crea una identidad de reclamaciones (claims identity)
                //con el esquema de autenticacion por cookies
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                //crea propiedades de autentificacion adicionales 
                //(puedes configurar mas aqui si es necesario)
                var authProperties = new AuthenticationProperties
                {
                    //puedes configurar propiedades adicionales aqui 
                };
                // Inicia sesion del usuario
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                 //Devuelve una respuesta exitosa
                return Ok("Inicio sesion correctamente");
            }
            else
            {
                //Devuelve una respuesta no autorizada si las credenciales son incorrectas 
                return Unauthorized("Credenciales Incorrectas");
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            //Cierra la sesion del usuario
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //Devuelve una respuesta exitosa
            return Ok("Cerro sesion correctamente");
        }
    }
}
