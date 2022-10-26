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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _usuarioService.GetUsuarios();
            return Ok(users);
        }
    }
}
