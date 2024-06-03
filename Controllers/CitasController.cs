using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Net;
using System.Diagnostics;
using System.Threading;

using Prueba_Tecnica_Api.Data;
using Prueba_Tecnica_Api.Models;
using Prueba_Tecnica_Api.Models.Dto;
using Prueba_Tecnica_Api.Models.Dto.Citas;
using Prueba_Tecnica_Api.Models.Dto.Pacientes;



namespace Prueba_Tecnica_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitasController : ControllerBase
    {

        private readonly ApplicationDbContext _db;
        protected ApiResponse _response;

        public CitasController(ApplicationDbContext db)
        {
            _db = db;
            _response = new();
        }

        //--------------- EndPoint que trae la lista completa de citas -------------------
        [HttpGet]
        [Route("ListarCitas")]
        public async Task<ActionResult<ApiResponse>> GetCitas()
        {
            try
            {
                _response.Resultado = await _db.Citas
                                              .Where(u => u.fechaEliminacion == null)
                                              .Include(g => g.Pacientes)
                                              .Include(d => d.Doctores)
                                              .Include(e => e.Estados)
                                              .Select(g => new
                                              {
                                                  g.idCita,
                                                  Paciente = g.Pacientes.nombre,
                                                  Doctor = g.Doctores.nombre,
                                                  g.fechaCita,
                                                  g.motivo,
                                                  Estado = g.Estados.nombre,
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

        //------------- EndPoint que trae un citas a través de la id --------------
        [HttpGet]
        [Route("ListarPorId/{id}")]

        public async Task<ActionResult<ApiResponse>> GetCitas(int id)
        {
            try
            {
                var cita = await _db.Citas.FirstOrDefaultAsync(e => e.idCita == id && e.fechaEliminacion == null);

                if (id == 0)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                if (cita == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Resultado = cita;
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

        //------------------------ EndPoint que crea nuevas citas -------------------------
        [HttpPost]
        [Route("CrearCita")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CrearPaciente([FromBody] CitasCreateDto citasDto)
        {
            try
            {
                if (citasDto == null)
                {
                    return BadRequest(citasDto);
                }

                Cita modelo = new()
                {
                    idPaciente = citasDto.idPaciente,
                    idDoctor = citasDto.idDoctor,
                    fechaCita = citasDto.fechaCita,
                    motivo = citasDto.motivo,
                    idEstado = 2,
                    fechaCreacion = DateTime.Now
                };

                await _db.Citas.AddAsync(modelo);
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
        [Route("ActualizarCita/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<ApiResponse>> ActualizaCita(int id, [FromBody] CitasUpdateDto citaDto)
        {
            try
            {
                if (citaDto == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var citaExistente = await _db.Citas.FirstOrDefaultAsync(e => e.idCita == id);

                if (citaExistente == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages = new List<string> { "La cita no existe" };
                    return _response;
                }

                citaExistente.idPaciente = citaDto.idPaciente;
                citaExistente.idDoctor = citaDto.idDoctor;
                citaExistente.fechaCita = citaDto.fechaCita;
                citaExistente.motivo = citaDto.motivo;
                citaExistente.idEstado = citaDto.idEstado;
                citaExistente.fechaActualizacion = DateTime.Now;

                _db.Citas.Update(citaExistente);
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

        //--------------- EndPoint que elimina (desactiva) un cita en la base de datos ------------
        [HttpPatch]
        [Route("EliminarCita/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> DesactivarCita(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                var cita = await _db.Citas.FirstOrDefaultAsync(v => v.idCita == id);

                if (cita == null)
                {
                    return NotFound();
                }

                // Desactivar el paciente estableciendo la fecha actual en fechaEliminacion
                cita.fechaEliminacion = DateTime.Now;

                _db.Citas.Update(cita);
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

        //Endpoint que lista los doctores
        [HttpGet]
        [Route("ListarDoctores")]
        public async Task<ActionResult<ApiResponse>> ListarDoctores()
        {
            var doctores = await _db.Doctores
                                    .Where(u => u.fechaEliminacion == null)
                                    .ToListAsync();
            return Ok(new ApiResponse
            {
                Resultado = doctores,
                statusCode = HttpStatusCode.OK,
                IsExitoso = true
            });
        }

        //Endpoint que lista las especialidades
        [HttpGet]
        [Route("ListarEspecialidades")]
        public async Task<ActionResult<ApiResponse>> ListarEspecialidades()
        {
            var especialidades = await _db.Especialidades.ToListAsync();
            return Ok(new ApiResponse
            {
                Resultado = especialidades,
                statusCode = HttpStatusCode.OK,
                IsExitoso = true
            });
        }

        //Endpoint que lista los pacientes
        [HttpGet]
        [Route("ListarPacientes")]
        public async Task<ActionResult<ApiResponse>> ListarPacientes()
        {
            var pacientes = await _db.Pacientes
                                     .Where(u => u.fechaEliminacion == null)
                                     .ToListAsync();
            return Ok(new ApiResponse
            {
                Resultado = pacientes,
                statusCode = HttpStatusCode.OK,
                IsExitoso = true
            });
        }

        //Endpoint que lista los pacientes
        [HttpGet]
        [Route("ListarEstados")]
        public async Task<ActionResult<ApiResponse>> ListarEstados()
        {
            var estados = await _db.Estados.ToListAsync();

            return Ok(new ApiResponse
            {
                Resultado = estados,
                statusCode = HttpStatusCode.OK,
                IsExitoso = true
            });
        }

    }
}
