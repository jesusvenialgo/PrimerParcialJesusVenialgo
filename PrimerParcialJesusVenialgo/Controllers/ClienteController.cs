using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Logica;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Repository.Data;

namespace PrimerParcialJesusVenialgo.Controllers
{
    public class ClienteController : Controller 
    {
        private ClienteService clienteService;
        private ClienteRepository clienteRepository;
        public ClienteController(IConfiguration configuracion)

        {
            clienteRepository = new ClienteRepository(configuracion.GetConnectionString("postgres"));
            clienteService = new ClienteService(configuracion.GetConnectionString("postgres"));
        }
        [HttpPost("Add")]
        public ActionResult Add([FromBody]Repository.Data.ClienteModel cliente)
        {
            clienteService.add(cliente);
            return Ok(new { message = "Los datos fueron agregado correctamente" });
        }

        [HttpPost("Update")]
        public ActionResult Update([FromBody] Repository.Data.ClienteModel cliente)
        {
            // Validar datos del cliente antes de actualizar (opcional)

            clienteService.update(cliente);
            return Ok(new { message = "Se han actualizado los datos correctamente" });
        }

        [HttpDelete("Delete")]
        public ActionResult Delete([FromBody]int id)
        {
            clienteRepository.delete(id);
            return Ok(new { message = "Los datos se eliminaron correctamente" });
        }

        [HttpGet("Get")]
        public ActionResult<ClienteModel> get(int id)
        {
            var cliente = clienteRepository.get(id);
            if (cliente != null)
            {
                return Ok(cliente);
            }
            else
            {
                return NotFound(new { message = "No se encontró la persona con el ID ingresado" });
            }
        }

        [HttpGet("List")]
        public ActionResult<List<ClienteModel>> List()
        {
            var clientes = clienteService.listarclientes();
            return Ok(clientes);
        }


    }
}
