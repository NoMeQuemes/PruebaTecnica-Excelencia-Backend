using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

using System.Net;

using Prueba_Tecnica_Api.Data;
using Prueba_Tecnica_Api.Models;
using Prueba_Tecnica_Api.Models.Dto;
using System.Diagnostics;
using System.Threading;
using Prueba_Tecnica_Api.Models.Dto.Doctores;
using Microsoft.AspNetCore.Authorization;


namespace Prueba_Tecnica_Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize] // Esta sentencia determina que a esta api solo pueden entrar usuarios autorizados
    [ApiController]
    public class DoctoresController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        protected ApiResponse _response;

        public DoctoresController(ApplicationDbContext db)
        {
            _db = db;
            _response = new();
        }

        //--------------- EndPoint que trae la lista completa de doctores -------------------
        [Authorize(Policy = "Admin")]
        [HttpGet]
        [Route("ListarDoctores")]
        public async Task<ActionResult<ApiResponse>> GetDoctores()
        {
            try
            {

                _response.Resultado = await _db.Doctores
                                                .Where(d => d.fechaEliminacion == null)
                                                .Include(d => d.Especialidades)
                                                .Select(d => new
                                                {
                                                    d.idDoctor,
                                                    d.nombre,
                                                    d.apellido,
                                                    d.email,
                                                    d.numCelular,
                                                    Especialidad = d.Especialidades.nombre,
                                                    d.fechaCreacion,
                                                    d.fechaActualizacion,
                                                    d.fechaEliminacion
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

        //------------- EndPoint que trae un doctor a través de la id --------------
        [HttpGet]
        [Route("ListarPorId/{id}")]

        public async Task<ActionResult<ApiResponse>> GetDoctor(int id)
        {
            try
            {
                var doctor = await _db.Doctores.FirstOrDefaultAsync(e => e.idDoctor == id && e.fechaEliminacion == null);

                if (id == 0)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                if (doctor == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Resultado = doctor;
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


        //------------------------ EndPoint que crea nuevos doctores -------------------------
        [HttpPost]
        [Route("CrearDoctor")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CrearDoctor([FromBody] DoctorCreateDto doctorDto)
        {
            try
            {
                if (doctorDto == null)
                {
                    return BadRequest(doctorDto);
                }

                Doctor modelo = new()
                {
                    nombre = doctorDto.nombre,
                    apellido = doctorDto.apellido,
                    email = doctorDto.email,
                    numCelular = doctorDto.numCelular,
                    idEspecialidad = doctorDto.idEspecialidad,
                    fechaCreacion = DateTime.Now
                };

                await _db.Doctores.AddAsync(modelo);
                await _db.SaveChangesAsync();
                _response.Resultado = modelo;
                _response.statusCode = HttpStatusCode.Created;

                return (_response);
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
        [Route("ActualizarDoctor/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<ApiResponse>> ActualizaDoctor(int id, [FromBody] DoctorUpdateDto doctorDto)
        {
            try
            {
                if (doctorDto == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var doctorExistente = await _db.Doctores.FirstOrDefaultAsync(e => e.idDoctor == id);

                if (doctorExistente == null)
                {
                    _response.IsExitoso = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages = new List<string> { "El doctor no existe" };
                    return _response;
                }

                doctorExistente.nombre = doctorDto.nombre;
                doctorExistente.apellido = doctorDto.apellido;
                doctorExistente.email = doctorDto.email;
                doctorExistente.numCelular = doctorDto.numCelular;
                doctorExistente.fechaActualizacion = DateTime.Now;

                _db.Doctores.Update(doctorExistente);
                await _db.SaveChangesAsync();

                _response.statusCode = HttpStatusCode.NoContent;
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;

        }

        //--------------- EndPoint que elimina (desactiva) un doctor en la base de datos ------------
        [HttpPatch]
        [Route("EliminarDoctor/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> DesactivarDoctor(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                var doctor = await _db.Doctores.FirstOrDefaultAsync(v => v.idDoctor == id);

                if (doctor == null)
                {
                    return NotFound();
                }

                // Desactivar el usuario estableciendo la fecha actual en fechaEliminacion
                doctor.fechaEliminacion = DateTime.Now;

                _db.Doctores.Update(doctor);
                await _db.SaveChangesAsync();

                _response.statusCode = HttpStatusCode.NoContent;

                return (_response);
            }
            catch ( Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;
        }

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


    }
}
