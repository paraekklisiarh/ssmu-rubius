using AutoMapper;

namespace Cafe.Services.Mapper;

public interface IMapFrom<T>
{
    void MappingFrom(Profile profile)
    {
        profile.CreateMap(typeof(T), GetType());
    }
}