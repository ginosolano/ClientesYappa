using AutoMapper;
using BackEndApi.DTOs;
using BackEndApi.Models;
using System.Globalization;

namespace BackEndApi.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Cliente, ClienteDTO>().
                ForMember(destino => destino.FechaDeNacimiento,
                opt => opt.MapFrom(origen => origen.FechaDeNacimiento.Value.ToString("dd-MM-yyyy"))
                );

            CreateMap<ClienteDTO, Cliente>().
                ForMember(destino => destino.FechaDeNacimiento,
                opt => opt.MapFrom(origen => DateTime.ParseExact(origen.FechaDeNacimiento, "dd-MM-yyyy", CultureInfo.InvariantCulture))
                );
        }
    }
}
