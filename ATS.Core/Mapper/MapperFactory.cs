using AutoMapper;

namespace ATS.Core.Mapper
{
    public static class MapperFactory
    {
        public static IMapper InitMapper()
        {
            var mappingProfile = new MappingProfile();

            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(mappingProfile));

            return new AutoMapper.Mapper(mapperConfig);
        }
    }
}
