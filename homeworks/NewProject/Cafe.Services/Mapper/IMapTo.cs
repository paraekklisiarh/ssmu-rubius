using AutoMapper;

namespace Cafe.Services.Mapper;

public interface IMapTo<T>
{
    void MappingTo(Profile profile)
    {
        profile.CreateMap(GetType(), typeof(T));
    }
}