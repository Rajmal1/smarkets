using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Interfaces;

namespace Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class ConsultaController : Controller {
        private readonly IConsultaService _consultaService;

        public ConsultaController (IConsultaService consultaService) {
            _consultaService = consultaService;
        }

        [HttpGet ()]
        public async Task<IActionResult> Get () {
            try {
                var consultasList = await _consultaService.GetAll ();

                if (consultasList == null)
                    return NoContent ();
                return Ok (consultasList);
            } catch (Exception ex) {
                return StatusCode (500, ex.Message);
            }
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> GetId (long id) {
            try {
                var consulta = await _consultaService.GetId (id);

                if (consulta == null)
                    return NoContent ();
                return Ok (consulta);
            } catch (Exception ex) {
                return StatusCode (500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post ([FromBody] ConsultaModel model) {
            try {
                if (!ModelState.IsValid)
                    return BadRequest (ModelState);

                if (model.DtInicioConsulta.CompareTo (model.DtFimConsulta) > 0)
                    return BadRequest ("Data início não pode ser maior que a data fim!");

                var newConsulta = await _consultaService.Post (model);

                if (newConsulta == null)
                    return NoContent ();
                return Ok (newConsulta);
            } catch (Exception ex) {
                return StatusCode (500, ex.Message);
            }
        }

        [HttpPut ("{id}")]
        public async Task<IActionResult> Put (ConsultaModel model, long id) {
            try {
                if (!ModelState.IsValid)
                    return BadRequest (ModelState);

                var a = model.DtInicioConsulta.CompareTo (model.DtFimConsulta);
                if (model.DtInicioConsulta.CompareTo (model.DtFimConsulta) > 0)
                    return BadRequest ("Data início não pode ser maior que a data fim!");

                var updatedModel = await _consultaService.Put (model, id);

                if (updatedModel == -1)
                    return BadRequest ("Já existe uma consulta para está data e hora!");

                if (updatedModel == -2)
                    return BadRequest ("A consulta que está sendo alterada não existe! Favor Verificar.");

                if (updatedModel == 0)
                    return NoContent ();
                return Ok (model);
            } catch (Exception ex) {
                return StatusCode (500, ex.Message);
            }
        }

        [HttpDelete ("{id}")]
        public async Task<IActionResult> Delete (long id) {
            try {
                var deletedRow = await _consultaService.Delete (id);

                if (deletedRow == -1)
                    return BadRequest ("A consulta que está sendo deletada não existe! Favor verificar");

                if (deletedRow == 0)
                    return NoContent ();
                return Ok ();
            } catch (Exception ex) {
                return StatusCode (500, ex.Message);
            }
        }
    }
}