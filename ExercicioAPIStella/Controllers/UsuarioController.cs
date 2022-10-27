using ExercicioAPIStella.Domain.Contracts.Usuario;
using ExercicioAPIStella.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExercicioAPIStella.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UsuarioRequest usuarioRequest)
        {
            try
            {
                var user = await _usuarioService.CadastrarUsuario(usuarioRequest);
                return Created("Post", user);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        // GAMBIARRA PARA POPULAR BANCO DE DADOS
        // [HttpPost]
        // [Route("BULK")]
        // public async Task<IActionResult> Post([FromBody] List<UsuarioRequest> usuarioRequests)
        // {
        //     try
        //     {
        //         await _usuarioService.CadastrarUsuario(usuarioRequests);
        //         return Created("Post", "Usuarios cadastrados.");
        //     }
        //     catch (ArgumentException e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _usuarioService.GetUsuarios();
            return Ok(users);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            try
            {
                var user = await _usuarioService.GetUsuarioPorId(id);
                return Ok(user);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpGet("name")]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            try
            {
                var user = await _usuarioService.GetUsuario(name);
                return Ok(user);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("id")]
        public async Task<IActionResult> Put(
            [FromQuery] int id,
            [FromBody] UsuarioRequest usuarioRequest
        )
        {
            try
            {
                var user = await _usuarioService.EditarUsuario(id, usuarioRequest);
                return Ok(user);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            try
            {
                await _usuarioService.ExcluirUsuarioPorId(id);
                return Ok("Usuario Deletado com Sucesso.");
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
