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
        public ActionResult Add(Repository.Data.FacturaModel factura)
        {
            facturaService.add(factura);
            return Ok(new { message = "Los datos fueron agregado correctamente" });
        }

        [HttpPost("Actualizar")]
        public ActionResult Update(Repository.Data.FacturaModel factura)
        {
            // Validar datos del cliente antes de actualizar (opcional)

            facturaService.update(factura);
            return Ok(new { message = "Se han actualizado los datos correctamente" });
        }

        [HttpDelete("Borrar")]
        public ActionResult Delete(int id)
        {
            facturaRepository.delete(id);
            return Ok(new { message = "Los datos se eliminaron correctamente" });
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
