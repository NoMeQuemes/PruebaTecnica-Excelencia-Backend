using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Net;

using Prueba_Tecnica_Api.Data;
using Prueba_Tecnica_Api.Models;
using Prueba_Tecnica_Api.Models.Dto;
using System.Diagnostics;
using System.Threading;
using Prueba_Tecnica_Api.Models.Dto.Pacientes;
using Microsoft.AspNetCore.Authorization;

namespace Prueba_Tecnica_Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        protected ApiResponse _response;

        public PacientesController(ApplicationDbContext db)
        {
            _db = db;
            _response = new();
        }

        //--------------- EndPoint que trae la lista completa de pacientes -------------------
        [HttpGet]
        [Route("ListarPacientes")]
        public async Task<ActionResult<ApiResponse>> GetPacientes()
        {
            try
            {
                _response.Resultado = await _db.Pacientes
                                              .Where(u => u.fechaEliminacion == null)
                                              .Include(g => g.Generos)
                                              .Select(g => new
                                              {
                                                  g.idPaciente,
                                                  g.nombre,
                                                  g.apellido,
                                                  g.fechaNacimiento,
                                                  Genero = g.Generos.nombre,
                                                  g.email,
                                                  g.numCelular,
                                                  g.fechaCreacion,
                                                  g.fechaActualizacion,
                                                  g.fechaEliminacion
                                              })
                                              .ToListAsync();
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        //------------- EndPoint que trae un paciente a través de la id --------------
        [HttpGet]
        [Route("ListarPorId/{id}")]

        public async Task<ActionResult<ApiResponse>> GetPaciente(int id)
        {
            try
            {
                var paciente = await _db.Pacientes.FirstOrDefaultAsync(e => e.idPaciente == id && e.fechaEliminacion == null);

                if (id == 0)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                if (paciente == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Resultado = paciente;
                _response.statusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return BadRequest(_response);
        }


        //------------------------ EndPoint que crea nuevos pacientes -------------------------
        [HttpPost]
        [Route("CrearPaciente")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CrearPaciente([FromBody] PacienteCreateDto pacienteDto)
        {
            try
            {
                if (pacienteDto == null)
                {
                    return BadRequest(pacienteDto);
                }

                Paciente modelo = new()
                {
                    nombre = pacienteDto.nombre,
                    apellido = pacienteDto.apellido,
                    fechaNacimiento = pacienteDto.fechaNacimiento,
                    idGenero = pacienteDto.idGenero,
                    email = pacienteDto.email,
                    numCelular = pacienteDto.numCelular,
                    fechaCreacion = DateTime.Now
                };

                await _db.Pacientes.AddAsync(modelo);
                await _db.SaveChangesAsync();
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }

        //--------------- EndPoint que actualiza un registro en la base de datos ------------
        [HttpPut]
        [Route("ActualizarPaciente/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<ApiResponse>> ActualizaPaciente(int id, [FromBody] PacienteUpdateDto pacienteDto)
        {
            try
            {
                if (pacienteDto == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var pacienteExistente = await _db.Pacientes.FirstOrDefaultAsync(e => e.idPaciente == id);

                if (pacienteExistente == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages = new List<string> { "El paciente no existe" };
                    return _response;
                }

                pacienteExistente.nombre = pacienteDto.nombre;
                pacienteExistente.apellido = pacienteDto.apellido;
                pacienteExistente.fechaNacimiento = pacienteDto.fechaNacimiento;
                pacienteExistente.idGenero = pacienteDto.idGenero;
                pacienteExistente.email = pacienteDto.email;
                pacienteExistente.numCelular = pacienteDto.numCelular;
                pacienteExistente.fechaActualizacion = DateTime.Now;

                _db.Pacientes.Update(pacienteExistente);
                await _db.SaveChangesAsync();

                _response.statusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;

        }

        //--------------- EndPoint que elimina (desactiva) un paciente en la base de datos ------------
        [HttpPatch]
        [Route("EliminarPaciente/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> DesactivarPaciente(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                var paciente = await _db.Pacientes.FirstOrDefaultAsync(v => v.idPaciente == id);

                if (paciente == null)
                {
                    return NotFound();
                }

                // Desactivar el paciente estableciendo la fecha actual en fechaEliminacion
                paciente.fechaEliminacion = DateTime.Now;

                _db.Pacientes.Update(paciente);
                await _db.SaveChangesAsync();

                _response.statusCode = HttpStatusCode.NoContent;

                return (_response);
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }

        //End point que lista los generos
        [HttpGet]
        [Route("ListarGeneros")]
        public async Task<ActionResult<ApiResponse>> ListarGeneros()
        {
            var generos = await _db.Generos.ToListAsync();
            return Ok(new ApiResponse
            {
                Resultado = generos,
                statusCode = HttpStatusCode.OK,
                IsExitoso = true
            });
        }
    }
}
