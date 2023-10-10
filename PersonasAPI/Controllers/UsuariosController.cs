using Azure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonasAPI.Modelos;
using PersonasAPI.Modelos.DTOs;
using PersonasAPI.Repositorio;

namespace PersonasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        protected ResponseDto _response;
        public UsuariosController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _response = new ResponseDto();
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(UsuariosDto usuariosDto)
        {
            var resultado = await _usuarioRepositorio.Register(
                new Usuarios { UserName = usuariosDto.UserName }, usuariosDto.Password);
            if(resultado == -1)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Usuario ya existente";
                return BadRequest(_response);
            }
            if(resultado == -500)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Error al crear el usuario";
                return BadRequest(_response);
            }
            _response.DisplayMessage = "Usuario creado con exito";
            _response.Result = resultado;
            return Ok(_response);
        }
        [HttpPost("Login")] 
        public async Task<ActionResult> Login(UsuariosDto user)
        {
            var resultado = await _usuarioRepositorio.Login(user.UserName, user.Password);
            if(resultado == "nouser")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Usuario No Existe";
                return BadRequest(_response);
            }
            if(resultado == "wrongpassword")
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "Contraseña incorrecta";
                return BadRequest(_response);
            }
            _response.DisplayMessage = "Usuario Logueado con exito";
            _response.Result = resultado;
            return Ok(_response);
        }
    }
}
