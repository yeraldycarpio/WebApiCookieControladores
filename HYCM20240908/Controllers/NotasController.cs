using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HYCM20240908.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotasController: ControllerBase
    {
        //crear una lista de objetos 'data' para almecenar informacion 
        static List<object> data = new List<object>();
        
        [HttpGet]
        public IEnumerable<object> Get()
        {
            //Devuelve los datos almacenados en la lista 'data' en 
            //respuesta a una solicitud GET
            return data;
        }


        // POST api/<NotasController>
        [HttpPost]
        public IActionResult Post(string nombre, int nota)
        {
            //agrega un nuevo objeto anonimo con 'name' y 'nota'
            //a la lista 'data' en respuesta a una solicitud POST 
            data.Add(new { nombre, nota });
            //Devuelve una respuesta HTTP exitosa 
            return Ok();
        }
    }
}
