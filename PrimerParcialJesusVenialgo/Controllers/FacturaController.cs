using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Logica;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Repository.Data;


namespace PrimerParcialJesusVenialgo.Controllers
{
    public class FacturaController : Controller
    {
        private FacturaService facturaService;
        private FacturaRepository facturaRepository;
        public FacturaController(IConfiguration configuracion)

        {
            facturaRepository = new FacturaRepository(configuracion.GetConnectionString("postgres"));
            facturaService = new FacturaService(configuracion.GetConnectionString("postgres"));
        }
        [HttpPost("Agregar")]
        public ActionResult Add([FromBody] Repository.Data.FacturaModel factura)
        {
            var result = facturaService.add(factura);
            return Ok(result);
        }

        [HttpPost("Actualizar")]
        public ActionResult Update([FromBody] Repository.Data.FacturaModel factura)
        {
            // Validar datos del cliente antes de actualizar (opcional)

            var result = facturaService.update(factura);
            return Ok(result);
        }

        [HttpDelete("Borrar")]
        public ActionResult Delete([FromBody] int id)
        {
            var result = facturaRepository.delete(id);
            return Ok(result);
        }

        [HttpGet("Obtener")]
        public ActionResult<FacturaModel> get(int id)
        {
            var factura = facturaRepository.get(id);
            if (factura != null)
            {
                return Ok(factura);
            }
            else
            {
                return NotFound(new { message = "No se encontró la persona con el ID ingresado" });
            }
        }

        [HttpGet("Listar")]
        public ActionResult<List<ClienteModel>> List()
        {
            var clientes = facturaService.listarfacturas();
            return Ok(clientes);
        }


    }
}
