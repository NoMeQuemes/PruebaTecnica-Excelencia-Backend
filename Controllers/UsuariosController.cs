﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;

using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

using Prueba_Tecnica_Api.Data;
using Prueba_Tecnica_Api.Models;
using Prueba_Tecnica_Api.Models.Dto.Usuarios;

namespace Satizen_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {

        private readonly ILogger<UsuariosController> _logger;
        private readonly ApplicationDbContext _db;
        protected ApiResponse _response;

        public UsuariosController(ILogger<UsuariosController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
            _response = new();
        }

        //--------------- EndPoint que trae la lista completa de usuarios -------------------
        [HttpGet]
        [Route("ListarUsuarios")]
        public async Task<ActionResult<ApiResponse>> GetUsuarios()
        {
            try
            {
                _logger.LogInformation("Obtener los usuarios"); // Esto solo muestra en consola que se ejecutó este endpoint

                _response.Resultado = await _db.Usuarios
                                              .Where(u => u.estadoUsuario == null)
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

        //------------- EndPoint que trae un usuario a través de la id --------------
        [HttpGet]
        [Route("ListarPorId/{id}")]

        public async Task<ActionResult<ApiResponse>> GetUsuario(int id)
        {
            try
            {
                var usuario = await _db.Usuarios.FirstOrDefaultAsync(e => e.idUsuario == id && e.estadoUsuario == null );

                if (id == 0)
                {
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                if (usuario == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Resultado = usuario;
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

        //--------------- EndPoint que desactiva un registro en la base de datos ------------
        [HttpPatch]
        [Route("EliminarUsuario/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DesactivarUsuario(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var usuario = await _db.Usuarios.FirstOrDefaultAsync(v => v.idUsuario == id);

            if (usuario == null)
            {
                return NotFound();
            }

            // Desactivar el usuario estableciendo la fecha actual en estadoUsuario
            usuario.estadoUsuario = DateTime.Now;

            _db.Usuarios.Update(usuario);
            await _db.SaveChangesAsync();

            _response.statusCode = HttpStatusCode.NoContent;
            return Ok(_response);
        }
    }
}
