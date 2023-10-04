using Microsoft.AspNetCore.Mvc;
using PersonasAPI.Modelos;
using PersonasAPI.Modelos.DTOs;
using PersonasAPI.Repositorio;

namespace PersonasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly IPersonasRepositorio _repositorio;
        private ResponseDto _response;
        public PersonasController(IPersonasRepositorio repositorio)
        {
            _response = new ResponseDto();
            _repositorio = repositorio;
        }

        // GET: api/Personas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseDto>>> GetPersonas()
        {        
            _response.Result = await _repositorio.GetPersonas();
            return Ok(_response);
        }

        // GET: api/Personas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto>> GetPersonas(int id)
        {
            _response.Result = await _repositorio.GetPersonasById(id);
            return Ok(_response);
        }

        // PUT: api/Personas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto>> PutPersonas(int id, PersonasDto personas)
        {
            _response.Result = _repositorio.CrearOActualizar(personas, id);

            return Ok(_response);
        }

        // POST: api/Personas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> PostPersonas(PersonasDto personas)
        {
            _response.Result = await _repositorio.CrearOActualizar(personas);

            return Ok(_response);
        }

        // DELETE: api/Personas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto>> DeletePersonas(int id)
        {              
            _response.IsSuccess = await _repositorio.DeletePersonas(id);
            _response.DisplayMessage = $"Se elimino la persona con id {id}";
            return Ok(_response);
        }        
    }
}
