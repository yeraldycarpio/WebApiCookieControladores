//importa el espacio de nombres necesarios 
//para la autorizacion 
using Microsoft.AspNetCore.Authorization;
//importa el espacio de nombres necesarios 
//para trabajar con controladores
using Microsoft.AspNetCore.Mvc;

namespace HYCM20240908.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class MatriculaController: ControllerBase
    {
        static List<object> data = new List<object>();

        // GET: api/<MatriculaController>
        [HttpGet]
        public IEnumerable<object> Get()
        {
            return data;
        }

        // GET api/<MatriculaController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var matricula = data.FirstOrDefault(m => m.GetType().GetProperty("id").GetValue(m).Equals(id));
            if (matricula == null)
            {
                return NotFound();
            }
            return Ok(matricula);
        }

        // POST api/<MatriculaController>
        [HttpPost]
        public IActionResult Post(int id, string nombre, string apellido, int edad, DateTime fecha)
        {
            var matricula = new
            {
                id,  // Usando el id proporcionado
                nombre,
                apellido,
                edad,
                fecha
            };
            data.Add(matricula);
            return Ok(matricula);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, string nombre, string apellido, int edad, DateTime fecha)
        {
            // Encuentra el índice del objeto en la lista que coincide con el id
            var index = data.FindIndex(m => m.GetType().GetProperty("id").GetValue(m).Equals(id));

            if (index == -1)
            {
                // Si no se encuentra el objeto, devuelve NotFound
                return NotFound();
            }

            // Crea un nuevo objeto con los datos actualizados
            var updatedMatricula = new
            {
                id,
                nombre,
                apellido,
                edad,
                fecha
            };

            // Reemplaza el objeto en la lista
            data[index] = updatedMatricula;

            return Ok(updatedMatricula);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Encuentra el índice del objeto en la lista que coincide con el id
            var index = data.FindIndex(m => m.GetType().GetProperty("id").GetValue(m).Equals(id));

            if (index == -1)
            {
                // Si no se encuentra el objeto, devuelve NotFound
                return NotFound();
            }

            // Elimina el objeto de la lista
            data.RemoveAt(index);

            return NoContent(); // Devuelve NoContent para indicar que la eliminación fue exitosa
        }
    }
}
