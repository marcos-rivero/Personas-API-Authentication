using PersonasAPI.Modelos;
using PersonasAPI.Modelos.DTOs;

namespace PersonasAPI.Repositorio
{
    public interface IPersonasRepositorio
    {
        Task<List<PersonasDto>> GetPersonas();
        Task<PersonasDto> GetPersonasById(int id);
        Task<PersonasDto> CrearOActualizar(PersonasDto persona, int id = 0);
        Task<bool> DeletePersonas(int id);
    }
}
