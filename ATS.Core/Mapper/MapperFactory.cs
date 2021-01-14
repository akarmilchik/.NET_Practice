using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ATS.Core.Mapper
{
    public static class MapperFactory
    {
        public static IMapper InitMapper()
        {
            var services = new ServiceCollection();

            var mappingProfile = new MappingProfile();

            services.AddAutoMapper(mapperConfig => mapperConfig.AddProfile(mappingProfile));

            var serviceProvider = services.BuildServiceProvider();

            var mockMapper = new MapperConfiguration(cfg => cfg.AddProfile(mappingProfile));

            return mockMapper.CreateMapper();

        }
    }
}
