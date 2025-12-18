using AutoMapper;
using Project.DTO;

namespace Project.ConsoleApp;

public static class AutoMapperConfiguration
{
    public static IMapper Configure()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        return config.CreateMapper();
    }
}

