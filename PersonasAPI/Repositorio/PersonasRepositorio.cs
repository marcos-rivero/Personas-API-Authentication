using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonasAPI.Data;
using PersonasAPI.Modelos;
using PersonasAPI.Modelos.DTOs;

namespace PersonasAPI.Repositorio
{
    public class PersonasRepositorio : IPersonasRepositorio
    {
        private readonly DataContext _context;
        private IMapper _mapper;
        public PersonasRepositorio(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PersonasDto> CrearOActualizar(PersonasDto personaDto, int id = 0)
        {
            var persona = _mapper.Map<PersonasDto, Personas>(personaDto);
            if (id == 0) { 
            
                await _context.Personas.AddAsync(persona);
            }
            else{
                persona.Id = id;
            }           
            
            await _context.SaveChangesAsync();
            return _mapper.Map<Personas, PersonasDto>(persona);
        }

        public async Task<bool> DeletePersonas(int id)
        {
            try
            {
                var persona = await _context.Personas.FindAsync(id);
                if (persona != null)
                {
                    _context.Personas.Remove(persona);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch(Exception ex){
                throw;
            }
        }

        public async Task<List<PersonasDto>> GetPersonas()
        {
            List<Personas> personas = await _context.Personas.ToListAsync();
            //List<PersonasDto> personasDto = new List<PersonasDto>();
            //foreach(var persona in personas)
            //{
            //    var personaDto = new PersonasDto();
            //    personaDto.Nombre = persona.Nombre;
            //    personaDto.Apellido = persona.Apellido;
            //    personasDto.Add(personaDto);

            //}
            return _mapper.Map<List<Personas>, List<PersonasDto>>(personas); 
        }

        public async Task<PersonasDto> GetPersonasById(int id)
        {
            var persona = await _context.Personas.FindAsync(id);            
            return _mapper.Map<Personas, PersonasDto>(persona); ;            
        }

    }
}
