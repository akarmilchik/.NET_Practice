using ATS.Mapper;
using AutoMapper;

namespace ATS.Helpers
{
    public static class MapperFactory
    {
        public static AutoMapper.Mapper InitMapper()
        {
            var config = new MappingProfile();

            var mapper = new AutoMapper.Mapper((IConfigurationProvider)config);

            return mapper;
        }
    }
}
