using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Services;

namespace Api.Controllers
{
    [Route("[controller]")]
    public class CoordinatorControllers : ControllerBase
    {
        private readonly CoordinatorServices _coordinatorServices;

        public CoordinatorControllers (CoordinatorServices coordinatorServices)
        {
            _coordinatorServices = coordinatorServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JoinModels>>> GetAllTables()
        {
            var joinModels = await _coordinatorServices.GetAllTables();
            if (joinModels != null)
            {
                return Ok(joinModels);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JoinModels>> GetIdTable(int id)
        {
            var joinModels = await _coordinatorServices.GetIdTable(id);
            if (joinModels != null)
            {
                return Ok(joinModels);
            }
            return NotFound();
        }

        // en crear no necesitamos sus id de primary key
        [HttpPost]
        public async Task<ActionResult> CreateItems([FromBody] CreateUpdateRequest request) // Se define el modelo del cuerpo del json que se esta recibiendo
        {
            await _coordinatorServices.CreateItems(request.curso, request.estudiante);
            return Ok();
        }

        // En actualizar necesitamos sus id de primary key
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateItem([FromBody] CreateUpdateRequest request)
        {
            if (await _coordinatorServices.UpdateItem(request.curso, request.estudiante))
            {
                return Ok();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            if(await _coordinatorServices.DeleteItem(id))
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpGet("GeneratePDF/{id}")]
        public async Task<IActionResult> Generate(int id)
        {
            var pdfBytes = await _coordinatorServices.GenerarPDF(id);
            return File(pdfBytes, "application/pdf", "generated.pdf");
        }
    }
}
