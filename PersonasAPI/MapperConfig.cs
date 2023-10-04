using AutoMapper;
using PersonasAPI.Modelos;
using PersonasAPI.Modelos.DTOs;

namespace PersonasAPI
{
    public class MapperConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<PersonasDto, Personas>();
                config.CreateMap<Personas, PersonasDto>();
            });
            return mappingConfig;
        }
    }
}
